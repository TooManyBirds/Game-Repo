using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    //public CharacterController cont;
    public CharacterController cont;
    public Transform playerBody;
    [Space]

    public float mass = 5.0f;
    public float gravityMod = 1.0f;
    //Camera
    public GameObject playerCamera;
    public GameObject ADSCamera;
    [Space]

    // Movement Variables
    public float playerSpeed = 6f;
    public Vector3 moveDir;
    public float jumpForce = 20f;
    public float jumpCount = 0f;
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;
    public bool canDash = true;
    bool isJumping = false;
    [Range(0, 1)] public float lerpFactor;
    [Space]

    //Mouse input stuff
    public bool ADS;
    public float turnVelocity;   
    public float turnSmoothing = 0.1f;
    float mouseX;
    [Space]

    //Gravity and stuff
    public float gravity = 20f;
    bool grounded;
    protected Vector3 velocity;
    [Space]

    //Ground Checking stuff
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    [Space]

    //Animation Stuff
    public Animator playerAnimator;

    public Gun gun;
    


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float totalForce = -(mass * 9.8f);
        playerAnimator.SetBool("ReJump", false);
        float initialVelocity = velocity.y;

        // Gravity and Jumping


        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetButtonUp("Jump")) isJumping = false;
        if (velocity.y < 0 && Input.GetButton("Jump") && isJumping == true) isJumping = false;

        if (grounded && velocity.y < 0)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2) { initialVelocity = jumpForce; isJumping = true; } //Will not add additional force if the max number of jumps has been reached.
            if (jumpCount <= 2) jumpCount++; //jumpCount will be allowed to go over the max jump count. This is useful for the conditionals that follow. However, the <= limits it just allows the variable to go over the max by 1, to prevent overflow.
        }

        if (grounded && Input.GetButtonDown("Jump") && jumpCount <= 2) 
        {
            //The first jump.
            playerAnimator.SetBool("Jump", true);
            Debug.Log("Jumping true");
        }
        else if (!grounded && Input.GetButtonDown("Jump") && jumpCount <= 2) //&& jumpCount < 2)
        {
            //Every jump beyond the first jump.
            playerAnimator.SetBool("ReJump", true);
            Debug.Log("JUMMMMMMMMMMMMMMMMMMMMMMP");
        }
        //Keep separate.
        if (grounded && !Input.GetButtonDown("Jump") && !isJumping)
        {
            playerAnimator.SetBool("Jump", false);
        }



        //Changing player velocity
        cont.Move(velocity * Time.deltaTime);

        //Horizontal player movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //moving player body with mouse
 



        if (direction.magnitude >= 0.1f)
        {
            // Playerboy point in movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothing);
            
            //Responsible for moving character in movement direction
            Vector3 tempMoveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            moveDir = tempMoveDir.normalized;
            cont.Move(tempMoveDir.normalized * playerSpeed * Time.deltaTime);
            playerAnimator.SetBool("Walking", true);
            Debug.Log("WALK");


            if (tempMoveDir != Vector3.zero)
            {
                transform.forward = tempMoveDir * Time.deltaTime;
                //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);

            }
        }
        //if (direction.magnitude <= 0.01f)
        else
        {
            playerAnimator.SetBool("Walking", false);
           // Debug.Log("NO WALK");
        }

        /*
         * 
         * Shooting Functionality
         * 
         */

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }

        /*
         * 
         * ADS Funcitonality
         * 
         */
        if (Input.GetButtonDown("Fire2"))
        {
            //ADS Enable
            ADSCamera.SetActive(true);
            ADS = true;
            playerAnimator.SetBool("aiming", ADS);
        } else if (Input.GetButtonUp("Fire2")) {
            //ADS Disable
            ADSCamera.SetActive(false);
            ADS = false;
            playerAnimator.SetBool("aiming", ADS);
            playerBody.transform.rotation = new Quaternion(0,0,0,0);
        }
        if (ADS)
        {
            Quaternion camRot = playerCamera.transform.rotation;
            Quaternion rotTo = Quaternion.RotateTowards(playerBody.rotation, camRot, 360f);
            Quaternion camQuat = new Quaternion(0, rotTo.y, 0, rotTo.w);
            playerBody.transform.rotation = camQuat;
        }

        /*
         * 
         * Dash Functionaliy
         * 
         */
        if (velocity.x > 5f)
        {
            velocity.x = 5f;
        }
        if (velocity.z > 5f)
        {
            velocity.z = 5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());

        }
        else
            playerAnimator.SetBool("dashing", false);
        //if (applyJump) StartCoroutine(ApplyJumpForce(velocity.y, totalForce, jumpForce, 0.2f));
        velocity.y = initialVelocity + ((-9.8f * gravityMod) * Time.deltaTime);

        //if (!isJumping)
        if (grounded && !isJumping) { velocity.y = 0.0f; }
        //else { Debug.Log("6776867897689768976897869"); }



    }
    // WARNING DANKLE CODE DANKLE CODE DANKLE CODE TOO DANK FEELS DANKLE MAN
    IEnumerator Dash()
    {
        float startTime = Time.time;

        canDash = false;

        playerAnimator.SetBool("dashing", false);

        while (Time.time < startTime + dashTime)
        {
            cont.Move(moveDir.normalized * dashSpeed * playerSpeed * Time.deltaTime);

            playerAnimator.SetBool("dashing", true);
            yield return null;
        }
        yield return new WaitForSeconds(dashCoolDown);

        Debug.Log("Dash Cool Down Over");

        canDash = true;

        playerAnimator.SetBool("dashing", false);
    }

    public void Shoot()
    {
        gun.Shoot();
    }

    public void Reload()
    {
        gun.Reload();
    }



}

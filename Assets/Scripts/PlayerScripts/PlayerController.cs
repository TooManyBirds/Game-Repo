using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    public CharacterController cont;
    public Transform playerCamera;
    public Transform playerBody;
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
    [Range(0, 1)] public float lerpFactor;
    [Space]

    //Mouse input stuff
    public bool ADS;
    public float turnVelocity;   
    public float turnSmoothing = 0.1f;
    float lerpTime = 0.0f;
    Quaternion playerRot;
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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Gravity and Jumping
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && velocity.y < 0)
        {
            velocity.y = 0;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            velocity.y = jumpForce;
            jumpCount++;
        }
        // Gravity
        velocity.y += gravity * Time.deltaTime;

        if (velocity.y < -20f)
        {
            velocity.y = -20f;
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
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothing);
            
            //Responsible for moving character in movement direction
            Vector3 tempMoveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            moveDir = tempMoveDir.normalized;
            cont.Move(tempMoveDir.normalized * playerSpeed * Time.deltaTime);
        
            if (tempMoveDir != Vector3.zero)
            {
                transform.forward = tempMoveDir * Time.deltaTime;
                //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);

            }
        }
        /*
         * 
         * ADS Funcitonality
         * 
         */
        if (!ADS && Input.GetButtonDown("Fire2"))
        {
            //ADS Enable
            ADS = true;
            playerRot = playerBody.transform.rotation;
        } else if ( ADS && Input.GetButtonUp("Fire2")) {
            //ADS Disable
            ADS = false;
            lerpTime = 0.0f;
        }
        if (ADS)
        {
            Quaternion camQuat;
            Quaternion camRot = playerCamera.rotation;
            if (lerpTime < 1.0)
            {
                float lerpAngle = Mathf.LerpAngle(playerRot.y, camRot.y * 180, lerpTime);
                camQuat = Quaternion.Euler(0, lerpAngle, 0);
            }
            else
            {
                camQuat = camQuat = Quaternion.Euler(0, camRot.y * 180, 0);
            }

            playerBody.transform.rotation = camQuat;
            lerpTime += Time.deltaTime / 0.1f;

        }

        /*
         * 
         * Dash Functionaliy
         * 
         */

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        canDash = false;
        
        while(Time.time < startTime + dashTime)
        {
            cont.Move(moveDir.normalized * dashSpeed * playerSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(dashCoolDown);

        Debug.Log("Dash Cool Down Over");

        canDash = true;
    }
}

//        //Animation Updater
//        horizontalMove = Input.GetAxisRaw("Horizontal");
//        if(horizontalMove != 0)
//        { 
//            //Set walk/run animation
//        }
//        else
//        {
//            //Undo animation
//        }
//        if(horizontalMove > 0)
//        {
//            direction = 1;
//        }
//        if(horizontalMove < 0)
//        {
//            direction = -1;
//        }

//    }
//}

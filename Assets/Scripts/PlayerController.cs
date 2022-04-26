using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    public CharacterController cont;
    public Transform playerCamera;
    public Transform playerBody;
    // Movement Variables
   
    public float playerSpeed = 6f;
    public float jumpForce = 20f;
    public float jumpCount = 0f;
    [Range(0, 1)] public float lerpFactor;


    //Mouse input stuff
    public float turnVelocity;   
    public float turnSmoothing = 0.1f;
    float mouseX;
    public float mouseSensativity;

    //Gravity and stuff
    public float gravity = 20f;
    bool grounded;
    Vector3 velocity;

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
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            cont.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        
            if (moveDir != Vector3.zero)
            {
                transform.forward = moveDir * Time.deltaTime;
                //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);

            }
        }
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

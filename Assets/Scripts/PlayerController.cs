using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    public CharacterController cont;

    // Movement Variables
    [SerializeField]
    private Vector3 playerVel;
    private bool groundedPlayer;
    private float playerSpd;
    private float jumpHeight;
    private float gravity = -9.81f;

    [Space]
    private float horizontalMove;
    private float direction = 1;

    void Start()
    {
        cont = GetComponent<CharacterController>();
    }


    void Update()
    {
        // Movement
        groundedPlayer = cont.isGrounded;
        if(groundedPlayer && playerVel.y < 0)
        {
            playerVel.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cont.Move(move * Time.deltaTime * playerSpd);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if(Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVel.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        // Contstantly pushed the player down
        playerVel.y += gravity * Time.deltaTime;
        cont.Move(playerVel * Time.deltaTime);



        //Animation Updater
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if(horizontalMove != 0)
        { 
            //Set walk/run animation
        }
        else
        {
            //Undo animation
        }
        if(horizontalMove > 0)
        {
            direction = 1;
        }
        if(horizontalMove < 0)
        {
            direction = -1;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;

    public Transform playerBod;

    public Transform playerHead;

    public Vector2 turn;

    public float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        turn.y = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= turn.y;            // Subtracting is regular, Adding is inverted
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerBod.localRotation = Quaternion.Euler(xRotation, turn.x, 0f); ;
    }
}

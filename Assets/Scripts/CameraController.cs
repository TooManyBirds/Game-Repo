using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 turn;
    public float sensativity = 5f;

    void Update()
    {
        //rotating the Camera Target based off of Mouse input
        turn.x += Input.GetAxis("Mouse X") * sensativity;
        turn.y += Input.GetAxis("Mouse Y") * sensativity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        turn.y = Mathf.Clamp(turn.y, -50f, 50f);
    }
}

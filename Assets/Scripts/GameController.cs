using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public MouseLook mCont;

    private void Update()
    {
        //Getting input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mCont.mouseSens = 800;
            print("esc pressed");
        }
    }
}

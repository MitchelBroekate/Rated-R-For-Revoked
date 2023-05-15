using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float rotateCamY;
    public float rotateCamX;
    void Update()
    {
        float mouseY = -Input.GetAxis("Mouse Y") * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime; ;

        rotateCamX = Mathf.Clamp(mouseX, -30f, 30f);
        rotateCamY = mouseY;


    }
}
https://www.youtube.com/watch?v=f473C43s8nE
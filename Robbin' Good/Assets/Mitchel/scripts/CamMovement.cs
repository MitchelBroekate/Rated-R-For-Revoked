using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float sensY;
    Vector3 rotateCamX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {

        

        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        rotateCamX.x -= mouseY;

        transform.Rotate(rotateCamX);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public float rotateCamY;
    public float rotateCamX;
    public Transform orientation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

        rotateCamY += mouseX;
        rotateCamX -= mouseY;

        rotateCamX = Mathf.Clamp(rotateCamX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotateCamX, rotateCamY, 0);
        orientation.rotation = Quaternion.Euler(0, rotateCamY, 0);
    }
}

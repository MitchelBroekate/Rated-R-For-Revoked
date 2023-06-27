using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float sensY;
    public float sensX;
    float rotateCam;
    public GameObject player;
    public Camera cam;
    private float targetFOV;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        
        float speed = 10;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV,Time.deltaTime * speed);

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        rotateCam = Mathf.Clamp(rotateCam, -90f, 90f);
        rotateCam -= mouseY;

        player.transform.Rotate(Vector3.up * mouseX);

        transform.localRotation = Quaternion.Euler(rotateCam, 0, 0);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            targetFOV = 75;
        } 
        
        else
        {
            targetFOV = 90;
        }
    }
}

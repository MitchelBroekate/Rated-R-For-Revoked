using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float sensY;
    public float sensX;
    float rotateCam;
    public GameObject player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        rotateCam = Mathf.Clamp(rotateCam, -90f, 90f);
        rotateCam -= mouseY;

        player.transform.Rotate(Vector3.up * mouseX);

        transform.localRotation = Quaternion.Euler(rotateCam, 0, 0);
    }
}

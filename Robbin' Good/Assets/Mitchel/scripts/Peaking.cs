using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaking : MonoBehaviour
{
    public float lerpTime = 0.15f;

    public Transform peakRight;
    public Transform peakLeft;
    public Transform peakStart;

    [Header("Keybindes")]
    public KeyCode keyPeakLeft = KeyCode.E;
    public KeyCode keyPeakRight = KeyCode.Q;

    void Update()
    {



        if (Input.GetKey(keyPeakRight))
        {
            transform.position = Vector3.Lerp(transform.position, peakRight.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakRight.rotation, lerpTime);
        } 
        else if(Input.GetKey(keyPeakLeft))
        {
            transform.position = Vector3.Lerp(transform.position, peakLeft.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakLeft.rotation, lerpTime);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, peakStart.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakStart.rotation, lerpTime);
        }
    }
}

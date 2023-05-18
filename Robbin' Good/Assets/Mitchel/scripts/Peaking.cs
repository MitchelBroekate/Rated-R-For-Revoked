using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaking : MonoBehaviour
{
    public Transform peakIdle;
    public Transform peakLeft;
    public Transform peakRight;

    public float lerpTime = 0.15f;

    [Header("Keybindes")]
    public KeyCode keyPeakLeft = KeyCode.E;
    public KeyCode keyPeakRight = KeyCode.Q;

    void Update()
    {
        if(Input.GetKey(keyPeakRight))
        {
            transform.position = Vector3.Lerp(transform.position, peakRight.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakRight.rotation, lerpTime);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, peakIdle.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakIdle.rotation, lerpTime);
        }
        if(Input.GetKey(keyPeakLeft))
        {
            transform.position = Vector3.Lerp(transform.position, peakLeft.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakLeft.rotation, lerpTime);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, peakIdle.position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, peakIdle.rotation, lerpTime);
        }
    }
}

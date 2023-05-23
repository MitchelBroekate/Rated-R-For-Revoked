using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaking : MonoBehaviour
{
    Vector3 posLeft;
    Vector3 posRight;
    Vector3 posCurrent;
 public void Start()
    {
        posLeft = transform.position;
        posRight = transform.position;
        posCurrent = transform.position;

        posLeft.x = 0.658f;
        posRight.x = -0.658f;
    }
    public float lerpTime = 0.15f;

    [Header("Keybindes")]
    public KeyCode keyPeakLeft = KeyCode.E;
    public KeyCode keyPeakRight = KeyCode.Q;

    void Update()
    {

        if (Input.GetKey(keyPeakRight))
        {
            transform.position = Vector3.Lerp(transform.position, posRight, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, posCurrent, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        }
        if(Input.GetKey(keyPeakLeft))
        {
            transform.position = Vector3.Lerp(transform.position, posLeft, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, posCurrent, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        }
    }
}

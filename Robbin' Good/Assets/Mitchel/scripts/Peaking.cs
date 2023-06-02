using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaking : MonoBehaviour
{
    public float lerpTime = 0.15f;
    Vector3 startPos;
    Vector3 posRight;
    Vector3 posLeft;
    Vector3 posCurrent;
    Transform playerBody;

    [Header("Keybindes")]
    public KeyCode keyPeakLeft = KeyCode.E;
    public KeyCode keyPeakRight = KeyCode.Q;

    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {

        if (Input.GetKey(keyPeakRight))
        {
            transform.position = Vector3.Lerp(transform.position, playerBody.position + posRight, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        } 
        else if(Input.GetKey(keyPeakLeft))
        {
            transform.position = Vector3.Lerp(transform.position, posLeft, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation, lerpTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float vertMove;
    public float horizMove;
    public float moveSpeed;
    public Vector3 moveDirection;

    void Update()
    {
        horizMove = Input.GetAxis("Horizontal");
        vertMove = Input.GetAxis("Vertical");

        moveDirection.x = horizMove;
        moveDirection.z = vertMove;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}

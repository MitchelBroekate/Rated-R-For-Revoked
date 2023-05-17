using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public Transform orientation;

    [Header("Ground check")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool grounded;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float hor;
    public float vert;

    [Header("keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, WhatIsGround);

        if(grounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }

        MyInput();
        SpeedControl();
        StateHandeler();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
    }

    private void StateHandeler()
    {
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vert + orientation.right * hor;
        rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KeyMove : MonoBehaviour
{
    public Transform orientation;
    private float moveSpeed;
    Vector3 moveDirection;
    Rigidbody rb;

    [Header("Move Controls")]
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float hor;
    public float vert;

    [Header("Ground check")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool grounded;

    [Header("Crouch")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("SFX")]
    public AudioSource walkSFX;
    public AudioSource crouchSFX;
    public AudioSource sprintSFX;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = transform.localScale.y;
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

        if (state == MovementState.walking)
        {
            if (hor != 0 || vert != 0)
            {
                if (!walkSFX.isPlaying)
                {
                    walkSFX.Play();
                }
            }
            else
            {
                walkSFX.Stop();
            }
        }

        if (state == MovementState.crouching)
        {
            if (hor != 0 || vert != 0)
            {
                if (!crouchSFX.isPlaying)
                {
                    crouchSFX.Play();
                }
            }
        }
        else
        {
            crouchSFX.Stop();
        }

        if (state == MovementState.sprinting)
        {
            if (hor != 0 || vert != 0)
            {
                if (!sprintSFX.isPlaying)
                {
                    sprintSFX.Play();
                }
            }
        }
        else
        {
            sprintSFX.Stop();
        }
    }
    private void FixedUpdate()
    {


        MovePlayer();
    }

    private void MyInput()
    {

        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 2.5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandeler()
    {

        if(Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
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

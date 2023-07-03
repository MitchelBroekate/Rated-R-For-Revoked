using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KeyMove : MonoBehaviour
{
    private float moveSpeed;
    Rigidbody rb;
    public Animator animator;

    [Header("Move Controls")]
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float horMove;
    public float vertMove;
    public float sensX;

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
    public GameObject pause;
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
        if(pause.activeInHierarchy == true)
        {
            walkSFX.Stop();
            crouchSFX.Stop();
            sprintSFX.Stop();
        }
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, WhatIsGround);

        if(grounded)
        {
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        }

        MyInput();
        SpeedControl();
        StateHandeler();
        MovePlayer();

        if (state == MovementState.walking)
        {

            if (horMove != 0 || vertMove != 0)
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
            walkSFX.Stop();

            if (horMove != 0 || vertMove != 0)
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
            walkSFX.Stop();

            if (horMove != 0 || vertMove != 0)
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

    private void MyInput()
    {

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

        if(Input.GetKeyDown(crouchKey))
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
    if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveSpeed);
        } 

    if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed);
        }

    if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * moveSpeed);
        }

    if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * moveSpeed);
        }

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (state == MovementState.sprinting)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Walking", true);
            }
        }
        else
        {
            if (state == MovementState.sprinting)
            {
                animator.SetBool("Running", false);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }

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

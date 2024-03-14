using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement components

    private CharacterController controller;
    private Animator animator;

    private float moveSpeed = 4.0f;

    [Header("Movement system")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;


    void Start()
    {
        // get movement components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        // Runs the functions that handle all movements
        Move();
    }
    public void Move()
    {
        //Gets the horizontal and vertical inputs as a number
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        //Directions in a normalised vector
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        //Is the sprint key pressesd
        if(Input.GetButton("Sprint"))
        {
            //Set the animation to run and increase the speed
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }


        // Check if there is any movement
        if (dir.magnitude >= 0.1f)
        {
            //Look towards that directions
            transform.rotation = Quaternion.LookRotation(dir);

            //Move
            controller.Move(velocity);

        }

        //Animation speed parameter
        animator.SetFloat("Speed", velocity.magnitude);
    }
}
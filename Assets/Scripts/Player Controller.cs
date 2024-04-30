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

    //Interaction Components
    Playerinteraction playerinteraction;


    void Start()
    {
        // get movement components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerinteraction = GetComponentInChildren<Playerinteraction>();
    }


    void Update()
    {
        // Runs the functions that handle all movements
        Move();

        //Runs the function that hand;es all interaction
        Interact();

        //Debugging purposes only
        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }

    }

    public void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Interact
            playerinteraction.Interact();
        }

        if(Input.GetButtonDown("Fire2"))
        {
            playerinteraction.ItemInteract();
        }

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
        if (Input.GetButton("Sprint"))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform cameraRig; // Reference to the CameraRig
    public Transform body; // Reference to the Body (capsule) GameObject

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        // Ensure we have a reference to the Rigidbody on the Body
        rb = body.GetComponent<Rigidbody>();
        // Reference to the Animator component
        animator = body.GetComponent<Animator>();
    }

    void Update()
    {
        // Get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the direction based on the camera's forward and right directions
        Vector3 forward = cameraRig.forward;
        Vector3 right = cameraRig.right;

        // We only want to move along the X and Z axes, so zero out the Y component
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate the desired movement direction
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized * speed;

        // Apply movement to the Rigidbody
        Vector3 newVelocity = movement;
        newVelocity.y = rb.velocity.y; // Preserve the existing Y velocity (for jumping)
        rb.velocity = newVelocity;

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
        }

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Check if the player is grounded using raycasting
        RaycastHit hit;
        if (Physics.Raycast(body.position, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
                Debug.Log("Grounded");
            }
        }
        else
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
        }

        // Attack input
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Attack button pressed");
            animator.SetTrigger("Attack");
        }
    }
    
}



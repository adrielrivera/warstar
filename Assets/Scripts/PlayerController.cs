using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform cameraRig; // reference to the CameraRig
    public Transform body; // reference to the Body (capsule) GameObject

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // ensure we have a reference to the Rigidbody on the Body
        rb = body.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // calculate the direction based on the camera's forward and right directions
        Vector3 forward = cameraRig.forward;
        Vector3 right = cameraRig.right;

        // zero out the Y component
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // calculate the desired movement direction
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized * speed;

        // apply movement to the Rigidbody
        Vector3 newVelocity = movement;
        newVelocity.y = rb.velocity.y; // preserve the existing Y velocity (for jumping)
        rb.velocity = newVelocity;


        // jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // check if the player is grounded
        RaycastHit hit;
        if (Physics.Raycast(body.position, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }

        // attack input
        if (Input.GetButtonDown("Fire1"))
        {
            Animator animator = body.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
    
}



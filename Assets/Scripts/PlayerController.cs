using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public float jumpForce = 5; // reference to CameraRig gameobj
    public Transform cameraRig; // reference to the body gameobj
    public Transform body;

    private Rigidbody rb;
    private bool isGrounded;

    void Start() {
        rb = body.GetComponent<Rigidbody>();
    }

    void Update() {
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
        
        // apply movement to rigidbody
        Vector3 newVelocity = movement;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;

        // check for jump input
        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Jump button pressed");
        }

        // jumping
        if (isGrounded && Input.GetButtonDown("Jump")) {
            Debug.Log("Jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // check if the player is grounded using raycasting

    RaycastHit hit;
    if (Physics.Raycast(body.position, Vector3.down, out hit, 1.1f)) {
        if (hit.collider.CompareTag("Ground")) {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }
    else
    {
        isGrounded = false;
        Debug.Log("Not Grounded");
    }
    
}



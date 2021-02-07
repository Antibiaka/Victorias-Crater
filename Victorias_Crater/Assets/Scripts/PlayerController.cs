using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    public float gravity = -3.71f;
    Vector3 velocity;
    
    private bool isGrounded;
    public Transform groundCheck;
    public  float groundDist = 0.4f;
    public  float jumpHigh = 3f;
    public LayerMask groundLayer; //choosing a layer to check on collision 

 

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundLayer);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime; //increase velocity by time in fall
        controller.Move(velocity * Time.deltaTime);
        BasicMovement();
        Jump();
        Sprint();
    }
    void BasicMovement() {
        float horizontal = Input.GetAxisRaw("Horizontal"); // raw - no smooth movement axis
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.right*horizontal+transform.forward*vertical;

        controller.Move(direction * speed * Time.deltaTime);

    }
    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHigh * -2f * gravity); //velocity that an object with stored gravitational potential energy 
        }
    }
    void Sprint() {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) {
            speed = 7f; //no air calculation yet
        }
        else {
            speed = 5f;
        }
    }

}

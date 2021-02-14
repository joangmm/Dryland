using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_character : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    private float gravity = 9.81f;
    public GameObject character;
    private Vector3 moveDirection = Vector3.zero;
    public Animator animator;
    public float degreesPerSecond;
    public float jumpHeight = 10.0f;
    Quaternion targetRotation;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal"));
            moveDirection *= speed;
            LookAtDirection(moveDirection);
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    void LookAtDirection(Vector3 moveDirection)
    {
        Vector3 xzDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (xzDirection.magnitude > 0)
            targetRotation = Quaternion.LookRotation(xzDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            targetRotation, degreesPerSecond * Time.deltaTime);
    }
}

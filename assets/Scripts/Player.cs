using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Security.Cryptography;
//using System.Threading;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    private Vector3 up = Vector3.zero;
    private Vector3 right = new Vector3(0, 90, 0);
    private Vector3 down = new Vector3(0, 180, 0);
    private Vector3 left = new Vector3(0, 270, 0);
    private Vector3 currentDirection = Vector3.zero;
    private Animator animator;
    
    private Vector3 nextPos, destination, direction;

    private float speed = 3f;
    private float rayLength = 1f;

    private bool canMove;
    public bool isAlive = true;
    //health --> they are used in the script fillStatusBar
    public float current_health = 10;
    public float max_health = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    
    void Update()
    {

       Move();
        if (transform.position == destination)
        {
            animator.SetTrigger("isIdle");
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                nextPos = Vector3.left;
                currentDirection = left;
                canMove = true;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                nextPos = Vector3.right;
                currentDirection = right;
                canMove = true;

            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextPos = Vector3.forward;
                currentDirection = up;
                canMove = true;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nextPos = Vector3.back;
                currentDirection = down;
                canMove = true;
            }

            if (Vector3.Distance(destination, transform.position) <= 0.00001f)
            {
                transform.localEulerAngles = currentDirection;
                if (canMove)
                {
                    if (Valid())
                    {
                        destination = transform.position + nextPos;
                        direction = nextPos;
                        canMove = false;
                    }

                }

            }
        }
    }

    bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;
        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);
 
        if (Physics.Raycast(myRay, out hit, rayLength))
        {
            if (hit.collider.tag == "limit" || hit.collider.tag == "cactus")
            {
                animator.SetTrigger("isIdle");
                canMove = false;
                return false;
            }
        }
        animator.SetTrigger("isRunning");
        return true;
    }
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "lion" || collision.gameObject.tag == "cocodrile")
        {
            animator.SetBool("dead", true);
            isAlive = false;
        }
    }
}

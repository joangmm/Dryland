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
    public bool isAlive;

    public float periot = 0.5f;
    private float interval = 0.0f;


    
    //health --> they are used in the script fillStatusBar
    public float current_health = 10;
    public float max_health = 10;

    void Start()
    {
        isAlive = true;
        animator = GetComponent<Animator>();
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    
    void Update()
    {
        
        
        if (transform.position == destination)
        {
            animator.Play("idle");
        }
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
          
        if (isAlive )
        {
            
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Time.time > interval)
            {
                interval = periot + Time.time;
                nextPos = Vector3.left;
                currentDirection = left;
                animator.Play("move");
                canMove = true;
            }
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && Time.time > interval)
            {
                interval = periot + Time.time;
                nextPos = Vector3.right;
                currentDirection = right;
                animator.Play("move");
                canMove = true;

            }
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && Time.time > interval)
            {
                interval = periot + Time.time;
                nextPos = Vector3.forward;
                currentDirection = up;
                animator.Play("move");
                canMove = true;
            }
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && Time.time > interval)
            {
                interval = periot + Time.time;
                nextPos = Vector3.back;
                currentDirection = down;
                animator.Play("move");
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
                animator.Play("idle");
                canMove = false;
                return false;
            }
        }
        //animator.SetTrigger("move");
        return true;
    }
    
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "enemigo")
        {
            //animator.SetBool("dead", true);
            isAlive = false;
        }
    }


    
}

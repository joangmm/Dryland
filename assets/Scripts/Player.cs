using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    private bool isAlive = true;
    private bool isBited = false;
    private int bitedTimes = 0;
    public float period = 0.5f;
    private float interval = 0.0f;
    private float poisonTick=0.0f;
    private GameObject gameOverMenu;
    private bool tpose = false;
    private int tpose_counter = 0;
    private bool godMode = false;
    private AudioSource ouch;
    private BoxCollider collider;

    //health --> they are used in the script fillStatusBar
    public float current_health = 10;
    public float max_health = 10;

    public GameObject girlAnimated;
    public GameObject girlTpose;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
        girlTpose.SetActive(false);
        tpose = false;
        tpose_counter = 0;
        ouch = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider>();
    }

    
    void Update()
    {
        if (isBited)
        {
            poisonTick = poisoned(poisonTick, bitedTimes);
        }
        Move();

        if (Input.GetKeyDown(KeyCode.T))
        {
            tpose_counter++;
            if (tpose_counter >= 5)
            {
                applyTpose();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode ^= true;
            collider.enabled ^= true;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (isAlive || godMode )
        {
            
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Time.time > interval)
            {
                interval = period + Time.time;
                nextPos = Vector3.left;
                currentDirection = left;
                animator.Play("move");
                canMove = true;
            }
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && Time.time > interval)
            {
                interval = period + Time.time;
                nextPos = Vector3.right;
                currentDirection = right;
                animator.Play("move");
                canMove = true;

            }
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && Time.time > interval)
            {
                interval = period + Time.time;
                nextPos = Vector3.forward;
                currentDirection = up;
                animator.Play("move");
                canMove = true;
            }
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && Time.time > interval)
            {
                interval = period + Time.time;
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
        else
        {
            gameOver();
        }
    }

    bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;
        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);
 
        if (Physics.Raycast(myRay, out hit, rayLength))
        {
            if (hit.collider.tag == "limit")
            {
                canMove = false;
                return false;
            }
            else if(hit.collider.tag == "cactus")
            {
                current_health -= 2;
                canMove = false;
                if(current_health == 0)
                {
                    isAlive = false;
                }
                else
                    playOuch();
                return false;
            }
            else if (hit.collider.tag == "fridge")
            {
                AudioSource audio = hit.collider.GetComponent<AudioSource>();
                audio.Play();
                hit.collider.transform.localPosition = new Vector3(0.1f, 0f, 0.91f);
                Vector3 v = transform.localEulerAngles;
                v.y = 270f;
                hit.collider.transform.localEulerAngles = v;
                AudioSource drinking = GetComponent<AudioSource>();
                StartCoroutine(waiter(drinking));
                canMove = false;
                return false;
            }
            else if(hit.collider.tag == "Finish")
            {
                if(isBited)
                    SceneManager.LoadScene("GameWinL1Poisoned");
                else
                {
                    SceneManager.LoadScene("GameWinL1");
                }
            }
        }
        //animator.SetTrigger("move");
        return true;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "lion" || collision.gameObject.tag == "cocodrile")
        {
            //animator.SetBool("dead", true);
            current_health = 0;
            isAlive = false;
        }
        else if(collision.gameObject.tag == "snake")
        {
            if (isBited == false)
                poisonTick = Time.time;
            isBited = true;
            playOuch();
            bitedTimes += 1;
        }

    }
    IEnumerator waiter(AudioSource audio)
    {
        yield return new WaitForSeconds(1);
        audio.Play();
        yield return new WaitForSeconds(3);
        audio.Stop();
        GameObject image = GameObject.Find("Canvas/Image");
        GameObject text = GameObject.Find("Canvas/Text");
        GameObject music = GameObject.FindGameObjectsWithTag("MenuMusic")[0];
        Animator image_animator = image.GetComponent<Animator>();
        Animator text_animator = text.GetComponent<Animator>();
        AudioSource music_audiosource = music.GetComponent<AudioSource>();
        image_animator.SetTrigger("isEndgame");
        text_animator.SetTrigger("isEndgame");
        yield return new WaitForSeconds(2);
        float startVolume = music_audiosource.volume;
        while (music_audiosource.volume > 0)
        {
            music_audiosource.volume -= startVolume * Time.deltaTime / 2;

            yield return null;
        }
        yield return new WaitForSeconds(3);

        Destroy(music);
        SceneManager.LoadScene("MainMenu");

    }
    public float poisoned(float curr_time, int times)
    {
        if(Time.time > curr_time)
        {
            current_health -=(0.1f)*times;
            if (current_health < 0)
                current_health = 0;
            if(current_health == 0)
            {
                isAlive = false;
            }
            return curr_time + 1;
        }
        return curr_time;
    }

    public void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    void playOuch()
    {
        if (ouch != null)
        {
            ouch.Play();
        }
    }
    void applyTpose()
    {
        tpose = !tpose;

        //animator.enabled = tpose;

        if (tpose)
        {
            girlAnimated.SetActive(false);
            girlTpose.SetActive(true);
            tpose_counter = 0;
        } else
        {
            girlTpose.SetActive(false);
            girlAnimated.SetActive(true);
            tpose_counter = 0;
        }
        
    }
}

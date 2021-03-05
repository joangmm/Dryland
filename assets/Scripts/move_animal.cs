using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_animal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector3 rotate = new Vector3(0, 180, 0);
    private bool backwards = false;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    public Vector3 jump = new Vector3(0, 0, 0);
    private Vector3 aux;
    private bool obsInFront = false;
    private Camera raycast;

    private Animation anim;

    bool isMoving = false;

    void Start()
    {
        raycast = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 2) && obsInFront == false)
        {
            obsInFront = true;
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            transform.rotation = Quaternion.Euler(rot);
            backwards ^= true;
            //raycast.transform.rotation = Quaternion.Euler(rot);
        }
        else
        {
            obsInFront = false;
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            if (obsInFront == false)
            {
                isMoving = true;
                transform.position += Vector3.forward * speed + jump;
                aux = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), Mathf.Floor(transform.position.z));
                transform.position = aux;
            }
            else
            {
                isMoving = true;
                transform.position -= Vector3.forward * speed - jump;
                aux = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y), Mathf.Floor(transform.position.z));
                transform.position = aux;
            }

        }

    }

    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Limit")
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            transform.rotation = Quaternion.Euler(rot);
            backwards ^= true;
        }
    }


}

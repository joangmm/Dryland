using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_animal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector3 rotate = new Vector3(0, 180, 0);
    private bool backwards = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(backwards!=true)
            transform.position += Vector3.forward * Time.deltaTime * speed;
        else
            transform.position -= Vector3.forward * Time.deltaTime * speed;
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

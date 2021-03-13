using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFridge : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "player")
        {
            transform.position = new Vector3(-0.23f, 0f, 1.04f);
            transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
            Debug.Log("test");
        }
    }
}

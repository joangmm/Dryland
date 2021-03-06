using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_animal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    public Vector3 jump = new Vector3(0, 0, 0);
    private Camera raycast;
    private Vector3 initial;

    private Animation anim;

    void Start()
    {
        raycast = GetComponent<Camera>();
        initial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        float maxDistance = 2;

        RaycastHit hit;

        if (Physics.Raycast( origin, direction, out hit, maxDistance ) && (hit.transform.tag == "limit" || hit.transform.tag == "cactus"))
        {
            Debug.DrawRay( origin, direction * hit.distance, Color.yellow);
            transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            Debug.Log(transform.tag);
            //change speed to go backwards
            speed = speed * ( -1 ) ;
        }
        else
        {
            Debug.DrawRay(origin, direction * 100, Color.white);
            //Debug.Log("Did Hit");
        }

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            Vector3 target = transform.position;

            target += (Vector3.forward * speed + jump);

            transform.position = new Vector3(Mathf.Floor(initial.x), Mathf.Floor(target.y), target.z );
        }

    }
}

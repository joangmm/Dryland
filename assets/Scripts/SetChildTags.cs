using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildTags : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.tag = transform.tag;
        }
    }


}

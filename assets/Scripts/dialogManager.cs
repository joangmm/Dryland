using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogManager : MonoBehaviour
{
    public GameObject fatherImage;
    public GameObject whiteBox;
    public GameObject blackBox;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        fatherImage.SetActive(true);
        whiteBox.SetActive(true);
        blackBox.SetActive(true);
        text1.SetActive(true);
        text2.SetActive(false);
        text3.SetActive(false);
        counter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (counter == 0)
            {
                text1.SetActive(false);
                text2.SetActive(true);
                counter = 1;
            }
            else if (counter == 1)
            {
                text2.SetActive(false);
                text3.SetActive(true);
                counter = 2;
            }else if(counter == 2)
            {
                fatherImage.SetActive(false);
                whiteBox.SetActive(false);
                blackBox.SetActive(false);
                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(false);
            }
        }

    }
}

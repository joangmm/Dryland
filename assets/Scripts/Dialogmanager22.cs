using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogmanager22 : MonoBehaviour
{
    public GameObject jordiImage;
    public GameObject madreImage;
    public GameObject whiteBox;
    public GameObject blackBox;
    public TextMeshProUGUI text1;
    public GameObject player;
    private Player player_script;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        jordiImage.SetActive(true);
        madreImage.SetActive(false);
        whiteBox.SetActive(true);
        blackBox.SetActive(true);
        counter = 0;
        player_script = player.GetComponent<Player>();
        player_script.isDialog = true;
        text1.SetText("Mamá, ¿me traes un vaso de agua?");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (counter == 0)
            {
                text1.SetText("…");
                counter += 1;
            }
            else if (counter == 1)
            {
                text1.SetText("¿Mamá…?");
                counter += 1;
            }
            else if (counter == 2)
            {
                madreImage.SetActive(true);
                jordiImage.SetActive(false);
                text1.SetText("¿Hasta para eso me necesitas? Vé tú, encima que estoy ordenando tu habitación...");
                counter += 1;
            }
            else if (counter == 3)
            {
                madreImage.SetActive(false);
                jordiImage.SetActive(true);
                text1.SetText("Madre mía, vale, vale, ¡¡ya voy!!");
                counter += 1;
            }
            else if (counter == 4)
            {
                text1.SetText("...");
                counter += 1;
            }
            else if (counter == 5)
            {
                text1.SetText("Qué pereza...");
                counter += 1;
            }
            else if (counter == 6)
            {
                text1.SetText("");
                jordiImage.SetActive(false);
                whiteBox.SetActive(false);
                blackBox.SetActive(false);
                counter += 1;
                player_script.isDialog = false;
            }
        }

    }
}

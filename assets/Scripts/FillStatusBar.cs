using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Player player;
    public Image fillImage;
    private Slider slider;
    private Color c = new Color(228f/255f, 60f/255f, 68f/255f);

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( slider.value <= slider.minValue )
        {
            fillImage.enabled = false;
        }

        if ( slider.value > slider.minValue && !fillImage.enabled )
        {
            fillImage.enabled = true;
        }

        float fillValue = (float) ( player.current_health / player.max_health );
        if (fillValue >= 0)
        {
            if (fillValue <= slider.maxValue / 3)
            {
                fillImage.color = c;
            }
            else
            {
                fillImage.color = Color.green;
            }

            slider.value = fillValue;
        }
    }
}

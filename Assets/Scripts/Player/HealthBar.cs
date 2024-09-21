using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class HealthBar : MonoBehaviour
{
    PlayerStats player;
    Image[] healthBars;
    int count, gap, num;
    float colourPercent;

    public Color Red;

    private void Start()
    {
        player = Player.instance.GetComponent<PlayerStats>();
        healthBars = new Image[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            healthBars[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
            healthBars[i].gameObject.SetActive(true);
        }

        num = healthBars.Length;

        gap = (int)player.MaxHealth / num;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            count = num - (int)System.Math.Ceiling((double)player.Health / gap);

            for (int i = 0; i < count; i++)
            {
                if(i < num) healthBars[i].gameObject.SetActive(false);
            }

            for (int i = num - 1; i >= count; i--)
            {
                if(i < num && i > -1) healthBars[i].gameObject.SetActive(true);
            }
        }

        // for (int i = 0; i < healthBars.Length; i++)
        // {
        //     colourPercent = (5 - count) / num;
        //     healthBars[i].color = Color.Lerp(Red, Color.white, colourPercent);
        // }
    }
}

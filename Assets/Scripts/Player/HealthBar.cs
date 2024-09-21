using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class HealthBar : MonoBehaviour
{
    PlayerStats player;
    GameObject[] healthBars;
    int count, gap, num;
    float colourPercent;

    public Color Red;

    private void Start()
    {
        player = Player.instance.GetComponent<PlayerStats>();
        healthBars = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            healthBars[i] = transform.GetChild(i).gameObject;
            healthBars[i].SetActive(true);
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
                if(i < num) healthBars[i].SetActive(false);
            }

            for (int i = num - 1; i >= count; i--)
            {
                if(i < num && i > -1) healthBars[i].SetActive(true);
            }
        }

        for (int i = 0; i < healthBars.Length; i++)
        {
            Renderer render = healthBars[i].GetComponent<Renderer>();
            Material obstacleMaterial = new Material(render.sharedMaterial);
            colourPercent = (5 - count) / num;
            obstacleMaterial.color = Color.Lerp(Red, Color.white, colourPercent);
            render.sharedMaterial = obstacleMaterial;
        }
    }
}

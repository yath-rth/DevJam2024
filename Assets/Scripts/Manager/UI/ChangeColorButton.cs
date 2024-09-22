using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorButton : MonoBehaviour
{
    changeTshirt changeShirt;
    [SerializeField] int color_index, cost;
    public bool upgraded;

    private void Awake()
    {
        changeShirt = Player.instance.GetComponent<changeTshirt>();

        if (PlayerPrefs.GetInt("unlocked").ToString()[color_index] == '1') upgraded = true;
    }

    public void change()
    {
        if (stepCounter.instance.steps >= cost && upgraded == false)
        {
            changeShirt.change(color_index);
            stepCounter.instance.setSteps(cost);
            upgraded = true;
            PlayerPrefs.SetInt("unlocked", PlayerPrefs.GetInt("unlocked") + (int)Math.Pow(10, color_index));
            Debug.Log(PlayerPrefs.GetInt("unlocked"));
        }

        if (upgraded) changeShirt.change(color_index);
    }
}

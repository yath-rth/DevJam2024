using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFirerateButton : MonoBehaviour
{
    public int cost;
    public bool upgraded;

    public void Increase()
    {
        if (stepCounter.instance.steps >= cost && Player.instance.GetComponent<dumbellGun>().timeBtwShot > 0.2f)
        {
            Player.instance.GetComponent<dumbellGun>().timeBtwShot -= 0.1f;
            Player.instance.GetComponent<dumbellGun>().timeBtwShot = Math.Clamp(Player.instance.GetComponent<dumbellGun>().timeBtwShot, 0.2f, 2f);
            PlayerPrefs.SetFloat("firerate", Player.instance.GetComponent<dumbellGun>().timeBtwShot);
            stepCounter.instance.setSteps(cost);
            upgraded = true;
        }
    }
}

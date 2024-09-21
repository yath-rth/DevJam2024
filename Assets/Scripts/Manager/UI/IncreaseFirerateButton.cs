using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFirerateButton : MonoBehaviour
{
    public int cost;

    public void Increase()
    {
        if (stepCounter.instance.steps >= cost)
        {
            Player.instance.GetComponent<dumbellGun>().timeBtwShot -= 0.2f;
            stepCounter.instance.temp_steps += cost;
        }
    }
}

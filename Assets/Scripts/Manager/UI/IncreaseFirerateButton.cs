using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFirerateButton : MonoBehaviour
{
    public int cost;

    public void Increase(){
        Player.instance.GetComponent<dumbellGun>().timeBtwShot -= 0.2f;
        if(stepCounter.instance.steps >= cost) stepCounter.instance.steps -= cost;
    }
}

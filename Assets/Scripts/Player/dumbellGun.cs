using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dumbellGun : MonoBehaviour
{
    public float timeBtwShot;
    float timeRn;

    private void Update() {
        if(timeRn < Time.time){
            timeRn = Time.time + timeBtwShot;
            Debug.Log("Shot");
        }
    }
}

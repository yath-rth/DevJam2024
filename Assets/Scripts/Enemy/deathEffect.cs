using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathEffect : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(disable());
    }

    IEnumerator disable(){
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

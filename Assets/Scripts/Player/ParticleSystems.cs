using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystems : MonoBehaviour
{
    public ParticleSystem[] particles;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Math.Abs(playerMovement.movementWSAD.magnitude) > 0.1)
        {
            particles[0].gameObject.SetActive(true);
            particles[0].transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
        }
        else
        {
            particles[0].gameObject.SetActive(false);
        }
    }
}

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
        Debug.Log(playerMovement.movementWSAD.magnitude);
        if (Math.Abs(playerMovement.movementWSAD.magnitude) > 0.1)
        {
            particles[0].transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
        }
    }
}

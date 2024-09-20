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
        if (playerMovement.movementWSAD.magnitude > 0.1)
        {
            particles[0].transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        }
    }
}

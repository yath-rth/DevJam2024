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
            particles[2].transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        }
    }

    public void ParticleSetActive(int index, bool val)
    {
        particles[index].gameObject.SetActive(val);
    }
}

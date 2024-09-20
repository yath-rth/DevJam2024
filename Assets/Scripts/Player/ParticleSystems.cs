using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystems : MonoBehaviour
{
    public ParticleSystem[] particles;
    PlayerDash _dash;
    PlayerMovement playerMovement;

    private void Start()
    {
        _dash = GetComponent<PlayerDash>();
        playerMovement = GetComponent<PlayerMovement>();

        var velocityoverlifetime = particles[0].velocityOverLifetime;

        velocityoverlifetime.enabled = true;
        velocityoverlifetime.space = ParticleSystemSimulationSpace.Local;
        if (_dash != null)
        {
            velocityoverlifetime.speedModifierMultiplier = _dash.anti;
        }
    }

    private void Update()
    {
        if (_dash != null)
        {
            if (_dash.anti > 0.1f)
            {
                ParticleSetActive(0, true);
            }
            else
            {
                ParticleSetActive(0, false);
            }

            var velocityoverlifetime = particles[0].velocityOverLifetime;
            velocityoverlifetime.speedModifierMultiplier = _dash.anti + .2f;

            if (_dash.timer < 1)
            {
                ParticleSetActive(1, true);
            }
            else
            {
                ParticleSetActive(1, false);
            }
        }

        if (playerMovement.movementWSAD.magnitude > 0)
        {
            ParticleSetActive(2, true);
        }
        else
        {
            ParticleSetActive(2, false);
        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollToggle : MonoBehaviour
{
    NavMeshAgent pathfinder;
    CapsuleCollider MainCollider;
    Animator am;
    EnemyMovement _enemyScript;
    EnemyStats _enemyStats;

    public List<CapsuleCollider> RagDollColliders;
    public List<Rigidbody> RagDollRigidBodies;

    private void Awake()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        MainCollider = GetComponent<CapsuleCollider>();
        am = GetComponent<Animator>();
        _enemyScript = GetComponent<EnemyMovement>();
        _enemyStats = GetComponent<EnemyStats>();

        ToggleRagDoll(false);
    }

    public void ToggleRagDoll(bool active)
    {
        foreach (CapsuleCollider collider in RagDollColliders)
        {
            collider.enabled = active;
        }

        foreach (Rigidbody rb in RagDollRigidBodies)
        {
            rb.detectCollisions = active;
            rb.isKinematic = !active;
        }

        pathfinder.enabled = !active;
        MainCollider.enabled = !active;
        am.enabled = !active;
        _enemyScript.enabled = !active;
        _enemyStats.enabled = !active;
    }
}

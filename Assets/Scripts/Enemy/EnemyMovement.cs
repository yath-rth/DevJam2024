using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class EnemyMovement : MonoBehaviour
{
    public enum state { idle, chasing, attack }
    public state State;
    [HideInInspector] public Transform player;
    [HideInInspector] public IStats playerStats;
    [HideInInspector] public float myCollisonRadius, playerCollisionRad;
    public float AttackDistance;
    [HideInInspector] public NavMeshAgent pathFinder;
    [HideInInspector] public Animator am;

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        am = GetComponent<Animator>();

        if (Player.instance != null)
        {
            player = Player.instance.transform;
            playerStats = player.GetComponent<IStats>();

            State = state.chasing;

            myCollisonRadius = GetComponent<CapsuleCollider>().radius;
            playerCollisionRad = player.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
        else
        {
            State = state.idle;
        }
    }

    IEnumerator UpdatePath()
    {
        while (player.gameObject != null)
        {
            if (playerStats.IsDead == false)
            {
                if (State == state.chasing)
                {
                    am.SetBool("walking", true);
                    Vector3 dest = (player.position - transform.position).normalized;
                    Vector3 destination = player.position - dest * (myCollisonRadius + playerCollisionRad + AttackDistance / 2);
                    pathFinder.SetDestination(destination);
                }
                else
                {
                    am.SetBool("walking", false);
                }
            }
            yield return new WaitForSeconds(.25f);
        }
    }

    private void Update()
    {
        transform.LookAt(player);

        if (am != null)
        {
            am.SetInteger("State", CurrentState());
        }
    }

    int CurrentState()
    {
        switch (State)
        {
            case state.idle:
                return 0;
            case state.chasing:
                return 1;
            case state.attack:
                return 2;
            default:
                return 1;
        }
    }
}

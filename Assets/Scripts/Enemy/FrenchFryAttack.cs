using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class FrenchFryAttack : MonoBehaviour
{
    EnemyMovement _enemy;
    public AnimationCurve attackCurve;
    public float AttackSpeed = 2, Damage = 2, timeBetweenAttacks = 2;
    float nextAttackTime;

    private void Awake()
    {
        _enemy = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (_enemy.player.position - transform.position).sqrMagnitude;
            if (sqrDstToTarget < Mathf.Pow(_enemy.AttackDistance + _enemy.myCollisonRadius + _enemy.playerCollisionRad, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        _enemy.State = EnemyMovement.state.attack;
        _enemy.pathFinder.enabled = false;

        float percent = 0;
        bool HasAttacked = false;
        Vector3 originalPos = transform.position;
        Vector3 dirTotarget = (_enemy.player.position - transform.position).normalized;
        Vector3 attackPos = _enemy.player.position - dirTotarget * _enemy.myCollisonRadius;

        while (percent <= 1)
        {
            percent += Time.deltaTime * AttackSpeed;
            _enemy.State = EnemyMovement.state.attack;
            if (_enemy.am != null)
            {
                _enemy.am.SetFloat("Dash Percent", percent);
            }

            float val = attackCurve.Evaluate(percent);
            transform.position = Vector3.Lerp(originalPos, attackPos, val);

            if (percent >= .05f && !HasAttacked)
            {
                _enemy.playerStats.TakeDamage(Damage);
                HasAttacked = true;
            }

            yield return null;
        }

        //_enemy.am.SetFloat("Dash Percent", 0);
        _enemy.pathFinder.enabled = true;
        _enemy.State = EnemyMovement.state.chasing;
    }
}

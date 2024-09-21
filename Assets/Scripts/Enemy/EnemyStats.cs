using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour, IStats
{
    public float Health { get; set; }
    public float MaxHealth;
    public bool IsDead { get; set; }

    EnemyMovement _enemy;
    Animator am;
    RagdollToggle Ragdolltoggle;
    public Vector3 direction;
    public GameObject DeathEffect;
    public int AddScoreAmt;

    public delegate void onEnemyDeath(int addScoreamt);
    public static event onEnemyDeath OnEnemyDeath;

    void Start()
    {
        Health = MaxHealth;
        _enemy = GetComponent<EnemyMovement>();
        am = GetComponent<Animator>();
        Ragdolltoggle = GetComponent<RagdollToggle>();
    }

    void Update()
    {
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if (Health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        Debug.Log(this.name + " has been damaged");
    }

    public void Die()
    {
        if (OnEnemyDeath != null) OnEnemyDeath(AddScoreAmt);

        Debug.Log("Enemy has died name: " + gameObject.name);
        _enemy.State = EnemyMovement.state.idle;
        IsDead = true;
        if (Ragdolltoggle != null) Ragdolltoggle.ToggleRagDoll(true);
        if (DeathEffect != null) DeathEffect.SetActive(true);
        GameObject particle = Instantiate(DeathEffect);
        particle.transform.position = transform.position;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            TakeDamage(10f);
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour, IStats
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }

    float damagedhealth;

    void Start()
    {
        MaxHealth = 100f;
        Health = MaxHealth;
        damagedhealth = MaxHealth;
        IsDead = false;
    }

    public void CheckHealth()
    {
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if(Health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float Damage)
    {
        damagedhealth = Health - Damage;
    }

    public void Die()
    {
        IsDead = true;
    }

    void Update()
    {
        CheckHealth();
        Health = Mathf.Lerp(Health, damagedhealth, 2 * Time.deltaTime);
        //StartCoroutine(ShowHealth());
    }

    IEnumerator ShowHealth()
    {
        Debug.Log(Health);
        yield return new WaitForSeconds(.25f);
        Debug.Log(Health);
    }
}

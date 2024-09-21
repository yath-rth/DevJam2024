using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG;
using DG.Tweening;

public class PlayerStats : MonoBehaviour, IStats
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }

    float damagedhealth;
    [SerializeField] Transform damageUI;

    void Start()
    {
        MaxHealth = 100f;
        Health = MaxHealth;
        damagedhealth = MaxHealth;
        IsDead = false;
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
        Debug.Log("Damage Taken");
        damagedhealth = Health - Damage;
        if(damageUI!=null) damageUI.DOPunchScale(Vector3.one, 0.5f, 0, 1);
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

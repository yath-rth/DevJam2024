using System.Collections;

public interface IStats
{
    float Health { get; set; }
    bool IsDead { get; set; }

    void TakeDamage(float Damage);

    void CheckHealth();

    void Die();
}

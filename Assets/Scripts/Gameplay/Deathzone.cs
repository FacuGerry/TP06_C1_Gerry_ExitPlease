using UnityEngine;

public class Deathzone : MonoBehaviour
{
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthSystem healthSystem))
        {
            damage = healthSystem.maxLife;
            healthSystem.DoDamage(damage);
        }
    }
}

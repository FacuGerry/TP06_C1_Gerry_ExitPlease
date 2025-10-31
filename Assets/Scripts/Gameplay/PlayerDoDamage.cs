using System;
using UnityEngine;

public class PlayerDoDamage : MonoBehaviour
{
    public static event Action<PlayerDoDamage> onPlayerDoDamage;

    public int damage = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.DoDamage(damage);
        }
    }
}

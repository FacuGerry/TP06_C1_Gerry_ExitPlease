using System;
using UnityEngine;
public class PickablesController : MonoBehaviour
{
    public static event Action<PickablesController> onCoinsPicked;
    public static event Action<PickablesController, bool, bool> onPickablesMakeSound;

    [Header("Type of pickable")]
    [SerializeField] private bool isLife = false;
    [SerializeField] private bool isCoin = false;

    [Header("Stats")]
    [SerializeField] private int lifeHeal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LifePicked(collision);
        CoinPicked();
        PickablesSound();
        gameObject.SetActive(false);
    }

    public void LifePicked(Collider2D collision)
    {
        if (isLife)
        {
            if (collision.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Heal(lifeHeal);
            }
        }
    }

    public void CoinPicked()
    {
        if (isCoin)
        {
            onCoinsPicked?.Invoke(this);
        }
    }

    public void PickablesSound()
    {
        onPickablesMakeSound?.Invoke(this, isLife, isCoin);
    }
}

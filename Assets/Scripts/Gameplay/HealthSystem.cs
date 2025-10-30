using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action<int, int, int> onLifeUpdated;
    public event Action onDie;

    public int life;
    public int actualMaxLife;

    public int initialLife = 50;
    public int maxLife = 100;

    private void Start()
    {
        life = initialLife;
        actualMaxLife = initialLife;
        onLifeUpdated?.Invoke(initialLife, actualMaxLife, maxLife);
    }

    public void DoDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.Log("Se cura en la funcion de daño");
            return;
        }

        life -= damage;

        if (life <= 0)
        {
            life = 0;
            onDie?.Invoke();
        }
        else
        {
            onLifeUpdated?.Invoke(life, actualMaxLife, maxLife);
        }

        Debug.Log("DoDamage", gameObject);
    }

    public void Heal(int plus)
    {
        if (plus < 0)
        {
            Debug.Log("Se daña en la funcion de cura");
            return;
        }

        life += plus;

        if (life > actualMaxLife)
            life = actualMaxLife;

        Debug.Log("Heal");
        onLifeUpdated?.Invoke(life, actualMaxLife, maxLife);
    }

    public void AddLife(int plus)
    {
        actualMaxLife += plus;
        if (actualMaxLife >= maxLife)
        {
            actualMaxLife = maxLife;
        }
        onLifeUpdated?.Invoke(life, actualMaxLife, maxLife);
    }
}
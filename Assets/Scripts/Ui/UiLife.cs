using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image barLife;

    private void OnEnable()
    {
        healthSystem.onLifeUpdated += HealthSystem_onLifeUpdated;
        healthSystem.onDie += HealthSystem_onDie;
    }

    private void OnDisable()
    {
        healthSystem.onLifeUpdated -= HealthSystem_onLifeUpdated;
        healthSystem.onDie -= HealthSystem_onDie;
    }

    public void HealthSystem_onLifeUpdated(int current, int max)
    {
        float lerp = current / (float)max;
        barLife.fillAmount = lerp;
    }

    private void HealthSystem_onDie()
    {
        barLife.fillAmount = 0;
    }
}
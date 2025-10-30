using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image barLifeBackg;
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

    public void HealthSystem_onLifeUpdated(int current, int actualMax, int max)
    {
        float lerp = current / (float)actualMax;
        barLife.fillAmount = lerp;
        barLifeBackg.fillAmount = max;
    }

    private void HealthSystem_onDie()
    {
        barLife.fillAmount = 0;
        barLifeBackg.fillAmount = 0;
    }
}
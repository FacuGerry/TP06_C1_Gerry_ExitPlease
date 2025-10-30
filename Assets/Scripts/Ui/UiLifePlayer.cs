using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiLifePlayer : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image barLifeBackg;
    [SerializeField] private Image barLife;
    [SerializeField] private TextMeshProUGUI textLife;
    [SerializeField] private TextMeshProUGUI textMaxLife;

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
        textLife.text = current.ToString("0");
        textMaxLife.text = actualMax.ToString("0");
    }

    private void HealthSystem_onDie()
    {
        barLife.fillAmount = 0;
        barLifeBackg.fillAmount = 0;
    }
}
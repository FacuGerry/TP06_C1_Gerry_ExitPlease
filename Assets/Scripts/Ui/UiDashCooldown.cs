using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiDashCooldown : MonoBehaviour
{
    [SerializeField] private PlayerDataSo data;
    [SerializeField] private Image barLifeBackg;
    [SerializeField] private Image barLife;

    private bool isReseting = false;

    private void Start()
    {
        isReseting = false;
    }

    private void OnEnable()
    {
        PlayerController.onPlayerDash += OnPlayerDash_StartCooldown;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDash -= OnPlayerDash_StartCooldown;
        StopAllCoroutines();
    }

    public void OnPlayerDash_StartCooldown(PlayerController playerController)
    {
        barLife.fillAmount = 0;
        if (!isReseting)
            StartCoroutine(nameof(ResetingDash));
    }

    public IEnumerator ResetingDash()
    {
        isReseting = true;
        float wait = 0.01f;
        float clock = 0;
        while (clock < data.dashCooldown)
        {
            yield return new WaitForSeconds(wait);
            clock += wait;
            float lerp = clock / data.dashCooldown;
            barLife.fillAmount = lerp;
        }
        isReseting = false;
        yield return null;
    }
}

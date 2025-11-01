using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiDashCooldown : MonoBehaviour
{
    [SerializeField] private PlayerDataSo data;
    [SerializeField] private Image barCooldownBackg;
    [SerializeField] private Image barCooldown;

    private IEnumerator ResetingDashUi;

    private void OnEnable()
    {
        PlayerController.onPlayerDash += OnPlayerDash_StartCooldown;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDash -= OnPlayerDash_StartCooldown;
        StopAllCoroutines();
        ResetingDashUi = null;
    }

    public void OnPlayerDash_StartCooldown(PlayerController playerController)
    {
        barCooldown.fillAmount = 0;

        if (ResetingDashUi != null)
            StopCoroutine(ResetingDashUi);

        ResetingDashUi = ResetingDash();
        StartCoroutine(ResetingDashUi);
    }

    public IEnumerator ResetingDash()
    {
        float clock = 0;
        while (clock < data.dashCooldown)
        {
            clock += Time.deltaTime;
            float lerp = clock / data.dashCooldown;
            barCooldown.fillAmount = lerp;
            yield return null;
        }
        ResetingDashUi = null;
    }
}

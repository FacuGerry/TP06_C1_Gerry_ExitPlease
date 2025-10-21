using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20;
    public int speed = 20;


    private void OnEnable ()
    {
        PlayerController.onPlayerAttack += OnPlayerAttack_ActivateDamageZone;
    }

    private void OnDisable ()
    {
        PlayerController.onPlayerAttack -= OnPlayerAttack_ActivateDamageZone;
    }

    private void OnPlayerAttack_ActivateDamageZone (PlayerController playerController)
    {
        if (playerController == null)
        StartCoroutine(nameof(WaitingToDeactivate));
    }

    private IEnumerator WaitingToDeactivate()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
        yield return null;
    }
}
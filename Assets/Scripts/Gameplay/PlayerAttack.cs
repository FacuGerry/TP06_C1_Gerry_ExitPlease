using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject swordColliderSide;
    [SerializeField] private GameObject swordColliderUp;

    private bool isAttacking = false;

    private void OnEnable()
    {
        PlayerController.onPlayerAttack += OnPlayerAttack_ActivateDamageZone;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerAttack -= OnPlayerAttack_ActivateDamageZone;
    }

    private void OnPlayerAttack_ActivateDamageZone(PlayerController playerController)
    {
        if (!isAttacking)
            StartCoroutine(nameof(WaitingToDeactivate));
    }

    private IEnumerator WaitingToDeactivate()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.2525f);
        swordColliderSide.SetActive(true);
        swordColliderUp.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        swordColliderSide.SetActive(false);
        swordColliderUp.SetActive(false);
        isAttacking = false;
        yield return null;
    }
}
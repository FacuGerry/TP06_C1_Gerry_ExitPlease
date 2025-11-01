using UnityEngine;

public class SFXController : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource master;
    [SerializeField] private AudioSource sfx;

    [Header("Pickables")]
    [SerializeField] private AudioClip healClip;
    [SerializeField] private AudioClip extraLifeClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip orbClip;
    [SerializeField] private AudioClip extraDamageClip;

    [Header("Player")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Enemy")]
    [SerializeField] private AudioClip enemyHurtSound;

    private void OnEnable()
    {
        PickablesController.onPickablesMakeSound += OnCoinsPicked_EmmitSound;

        PlayerController.onPlayerAttack += OnPlayerAttack_AttackSound;
        PlayerController.onPlayerJump += OnPlayerJump_JumpSound;
        PlayerController.onPlayerDash += OnPlayerDash_DashSound;
        PlayerController.onPlayerHurt += OnPlayerHurt_HurtSound;
        PlayerController.onPlayerDie += OnPlayerDie_GameOverSound;

        PlayerDoDamage.onPlayerDoDamage += OnPlayerDoDamage_EnemyHurtSound;
    }

    private void OnDisable()
    {
        PickablesController.onPickablesMakeSound -= OnCoinsPicked_EmmitSound;

        PlayerController.onPlayerAttack -= OnPlayerAttack_AttackSound;
        PlayerController.onPlayerJump -= OnPlayerJump_JumpSound;
        PlayerController.onPlayerDash -= OnPlayerDash_DashSound;
        PlayerController.onPlayerHurt -= OnPlayerHurt_HurtSound;
        PlayerController.onPlayerDie -= OnPlayerDie_GameOverSound;

        PlayerDoDamage.onPlayerDoDamage -= OnPlayerDoDamage_EnemyHurtSound;
    }

    public void OnCoinsPicked_EmmitSound(PickablesController pickablesController, bool isLife, bool isExtraLife, bool isCoin)
    { 
        if (isLife)
        {
            sfx.PlayOneShot(healClip);
        }
        if (isExtraLife)
        {
            sfx.PlayOneShot(extraLifeClip);
        }
        if (isCoin)
        {
            sfx.PlayOneShot(coinClip);
        }
    }

    public void OnPlayerAttack_AttackSound(PlayerController playerController)
    {
        sfx.PlayOneShot(attackSound);
        Debug.Log("OnPlayerAttack_AttackSound");
    }

    public void OnPlayerJump_JumpSound(PlayerController playerController)
    {
        sfx.PlayOneShot(jumpSound);
        Debug.Log("OnPlayerJump_JumpSound");
    }

    public void OnPlayerDash_DashSound(PlayerController playerController)
    {
        sfx.PlayOneShot(dashSound);
        Debug.Log("OnPlayerDash_DashSound");
    }

    public void OnPlayerHurt_HurtSound(PlayerController playerController)
    {
        sfx.PlayOneShot(hurtSound);
        Debug.Log("OnPlayerHurt_HurtSound");
    }

    public void OnPlayerDie_GameOverSound(PlayerController playerController)
    {
        sfx.PlayOneShot(deathSound);
        Debug.Log("OnPlayerDie_GameOverSound");
    }

    public void OnPlayerDoDamage_EnemyHurtSound(PlayerDoDamage playerDoDamage)
    {
        sfx.PlayOneShot(enemyHurtSound);
        Debug.Log("OnPlayerDoDamage_EnemyHurtSound");
    }
}

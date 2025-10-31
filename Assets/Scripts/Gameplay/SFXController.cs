using UnityEngine;

public class SFXController : MonoBehaviour
{
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

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PickablesController.onPickablesMakeSound += OnCoinsPicked_EmmitSound;

        PlayerController.onPlayerAttack += OnPlayerAttack_AttackSound;
        PlayerController.onPlayerJump += OnPlayerJump_JumpSound;
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
            source.PlayOneShot(healClip);
        }
        if (isExtraLife)
        {
            source.PlayOneShot(extraLifeClip);
        }
        if (isCoin)
        {
            source.PlayOneShot(coinClip);
        }
    }

    public void OnPlayerAttack_AttackSound(PlayerController playerController)
    {
        source.PlayOneShot(attackSound);
    }

    public void OnPlayerJump_JumpSound(PlayerController playerController)
    {
        source.PlayOneShot(jumpSound);
    }

    public void OnPlayerDash_DashSound(PlayerController playerController)
    {
        source.PlayOneShot(dashSound);
    }

    public void OnPlayerHurt_HurtSound(PlayerController playerController)
    {
        source.PlayOneShot(hurtSound);
    }

    public void OnPlayerDie_GameOverSound(PlayerController playerController)
    {
        source.PlayOneShot(deathSound);
    }

    public void OnPlayerDoDamage_EnemyHurtSound(PlayerDoDamage playerDoDamage)
    {
        source.PlayOneShot(enemyHurtSound);
    }
}

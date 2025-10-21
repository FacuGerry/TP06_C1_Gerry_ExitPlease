using UnityEngine;

public class SFXController : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip lifeClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip jumpSound;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PickablesController.onPickablesMakeSound += OnCoinsPicked_EmmitSound;
        PlayerController.onPlayerDie += OnPlayerDie_GameOverSound;
        PlayerController.onPlayerJump += OnPlayerJump_JumpSound;
    }

    private void OnDisable()
    {
        PickablesController.onPickablesMakeSound -= OnCoinsPicked_EmmitSound;
        PlayerController.onPlayerDie -= OnPlayerDie_GameOverSound;
        PlayerController.onPlayerJump -= OnPlayerJump_JumpSound;
    }

    public void OnCoinsPicked_EmmitSound(PickablesController pickablesController, bool isLife, bool isCoin)
    { 
        if (isLife)
        {
            source.PlayOneShot(lifeClip);
        }
        if (isCoin)
        {
            source.PlayOneShot(coinClip);
        }
    }

    public void OnPlayerDie_GameOverSound(PlayerController playerController)
    {
        source.PlayOneShot(deathSound);
    }

    public void OnPlayerJump_JumpSound(PlayerController playerController)
    {
        source.PlayOneShot(jumpSound);
    }
}

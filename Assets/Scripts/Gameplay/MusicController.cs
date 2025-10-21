using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerController.onPlayerDie += OnPlayerDie_StopMusic;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDie -= OnPlayerDie_StopMusic;
    }

    public void OnPlayerDie_StopMusic(PlayerController playerController)
    {
        source.Stop();
    }
}

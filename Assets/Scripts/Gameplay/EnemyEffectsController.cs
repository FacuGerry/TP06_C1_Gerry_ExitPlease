using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip deathSound;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        enemyController.onEnemyDeath += OnEnemyDeath_CheckForParticleEmission;
        enemyController.onEnemyDeath += OnEnemyDeath_EmmitSound;
    }

    private void OnDisable()
    {
        enemyController.onEnemyDeath -= OnEnemyDeath_CheckForParticleEmission;
        enemyController.onEnemyDeath -= OnEnemyDeath_EmmitSound;
    }

    public void OnEnemyDeath_CheckForParticleEmission()
    {
        particles.transform.position = enemy.transform.position;
        particles.SetActive(true);
    }

    public void OnEnemyDeath_EmmitSound()
    {
        source.PlayOneShot(deathSound);
    }
}

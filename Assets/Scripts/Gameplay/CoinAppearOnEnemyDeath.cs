using UnityEngine;

public class CoinAppearOnEnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject coin;
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = enemy.GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
        enemyController.onEnemyDeath += OnEnemyDeath_CoinAppear;
    }
    
    private void OnDisable()
    {
        enemyController.onEnemyDeath -= OnEnemyDeath_CoinAppear;
    }

    public void OnEnemyDeath_CoinAppear()
    {
        coin.transform.position = enemy.transform.position;
        coin.SetActive(true);
    }
}

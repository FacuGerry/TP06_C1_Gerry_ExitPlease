using UnityEngine;

public class CoinAppearOnEnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject coin;
    private SlimeController enemyController;

    private void Awake()
    {
        enemyController = enemy.GetComponent<SlimeController>();
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

using UnityEngine;

public class EnemyLifeBarFollowEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    void Update()
    {
        transform.position = new Vector2(enemy.transform.position.x, transform.position.y);
    }
}

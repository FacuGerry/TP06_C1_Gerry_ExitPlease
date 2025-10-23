using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public event Action onEnemyDeath;

    private Rigidbody2D enemyRigidbody;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private EnemyDataSo enemyData;
    [SerializeField] private float limitMovementRight = 1f;
    [SerializeField] private float limitMovementLeft = 1f;
    private float limitRight;
    private float limitLeft;
    private float enemySpeed;
    private float damage;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        healthSystem.onDie += HealthSystem_onDie;
    }

    private void Start()
    {
        enemySpeed = enemyData.speed;
        damage = enemyData.damage;
        limitLeft = transform.position.x - limitMovementLeft;
        limitRight = transform.position.x + limitMovementRight;
        enemyRigidbody.velocity = Vector2.right * enemySpeed;
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.DoDamage((int)damage);
        }
    }

    private void OnDisable()
    {
        healthSystem.onDie -= HealthSystem_onDie;
    }

    public void Move()
    {
        if (transform.position.x <= limitLeft) //Si toca el borde izquierdo
        {
            enemyRigidbody.velocityX = enemySpeed;
            spriteRenderer.flipX = true;
        }
        if (transform.position.x >= limitRight) //Si toca el borde derecho
        {
            enemyRigidbody.velocityX = -enemySpeed;
            spriteRenderer.flipX = false;
        }

        if (enemyRigidbody.velocityX < enemySpeed && enemyRigidbody.velocityX >= 0)
        {
            enemyRigidbody.velocityX = enemySpeed;
            spriteRenderer.flipX = true;
        }
        if (enemyRigidbody.velocityX > -enemySpeed && enemyRigidbody.velocityX < 0)
        {
            enemyRigidbody.velocityX = -enemySpeed;
            spriteRenderer.flipX = false;
        }
    }

    private void HealthSystem_onDie()
    {
        onEnemyDeath?.Invoke();
        gameObject.SetActive(false);
        Debug.Log("Muri� el enemigo");
    }
}
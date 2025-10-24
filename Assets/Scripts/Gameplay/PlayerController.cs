using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static event Action<PlayerController> onPlayerAttack;
    public static event Action<PlayerController> onPlayerDie;
    public static event Action<PlayerController> onPlayerJump;
    public static event Action<PlayerController> onPause;
    public static event Action<PlayerController> onResume;
    public static event Action<PlayerController, int> onAnimating;

    [SerializeField] private PlayerDataSo data;

    [SerializeField] private HealthSystem healthSystem;

    private Rigidbody2D playerRigidbody;

    [NonSerialized] public bool isJumping = false;
    [NonSerialized] public bool isWalking = false;
    [NonSerialized] public bool isAttacking = false;
    [NonSerialized] public bool isDashing = false;

    private bool isPause = false;

    private enum AnimationStates
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3,
        Hurt = 4,
        Death = 5,
        Dash = 6
    };

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isPause = false;
    }

    private void OnEnable()
    {
        healthSystem.onDie += HealthSystem_onDie;
    }

    private void Update()
    {
        if (!isPause)
        {
            Move();
            Animate();
        }
        if (Input.GetKeyDown(data.pauseGame))
        {
            switch (isPause)
            {
                case true:
                    Time.timeScale = 1f;
                    isPause = false;
                    onResume?.Invoke(this);
                    break;
                case false:
                    Time.timeScale = 0f;
                    isPause = true;
                    onPause?.Invoke(this);
                    break;
            }
        }
    }

    private void OnDisable()
    {
        healthSystem.onDie -= HealthSystem_onDie;
    }

    public void Move()
    {
        if (!isAttacking)
        {
            if (Input.GetKey(data.goLeft))
            {
                playerRigidbody.AddForce(Vector2.left * data.speed * Time.deltaTime, ForceMode2D.Force);
                transform.rotation = new Quaternion(0, 180, 0, 0);
                isWalking = true;
            }

            if (Input.GetKey(data.goRight))
            {
                playerRigidbody.AddForce(Vector2.right * data.speed * Time.deltaTime, ForceMode2D.Force);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                isWalking = true;
            }

            if (Input.GetKey(data.goUp))
            {
                if (!isJumping)
                {
                    playerRigidbody.velocityY = 0f;
                    playerRigidbody.AddForce(Vector2.up * data.jumpForce, ForceMode2D.Impulse);
                    onPlayerJump?.Invoke(this);
                }
            }

            if (Input.GetKey(data.attack))
            {
                isAttacking = true;
                StartCoroutine(nameof(WaitingForNewAttack));
                onPlayerAttack?.Invoke(this);
            }

            if (Input.GetKey(data.dash) && !isDashing)
            {
                if (transform.rotation.y == 0)
                {
                    playerRigidbody.AddForce(Vector2.right * data.dashForce, ForceMode2D.Impulse);
                }
                else
                {
                    playerRigidbody.AddForce(Vector2.left * data.dashForce, ForceMode2D.Impulse);
                }
                isDashing = true;
                onAnimating?.Invoke(this, (int)AnimationStates.Dash);
            }
        }
    }

    public void Animate()
    {
        if (!isJumping)
        {
            if (isWalking)
            {
                onAnimating?.Invoke(this, (int)AnimationStates.Walk);
                isWalking = false;
            }
            else if (!isWalking && isAttacking)
            {
                onAnimating?.Invoke(this, (int)AnimationStates.Attack);
            }
            else
            {
                onAnimating?.Invoke(this, (int)AnimationStates.Idle);
            }
        }
        else if (isJumping)
        {
            onAnimating?.Invoke(this, (int)AnimationStates.Jump);
        }
    }

    public void HealthSystem_onDie()
    {
        onPlayerDie?.Invoke(this);
        gameObject.SetActive(false);
    }

    private IEnumerator WaitingForNewAttack()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
        yield return null;
    }
}

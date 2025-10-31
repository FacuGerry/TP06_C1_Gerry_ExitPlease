using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static event Action<PlayerController> onPlayerAttack;
    public static event Action<PlayerController> onPlayerJump;
    public static event Action<PlayerController> onPlayerDash;
    public static event Action<PlayerController> onPlayerHurt;
    public static event Action<PlayerController> onPlayerDie;
    public static event Action<PlayerController> onPause;
    public static event Action<PlayerController> onResume;
    public static event Action<PlayerController, int> onAnimating;

    [SerializeField] private PlayerDataSo data;

    [SerializeField] private HealthSystem healthSystem;

    private Rigidbody2D playerRigidbody;

    [NonSerialized] public bool isJumping = false;
    [NonSerialized] public bool isWalking = false;
    [NonSerialized] public bool isAttacking = false;
    [NonSerialized] public bool isAnimatingDash = false;
    [NonSerialized] public bool isAnimatingHurt = false;

    private bool isPause = false;
    private bool isResetingDash = false;

    private bool playerCanMove = true;
    private bool isAlive = true;

    private enum AnimationStates
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3,
        Hurt = 4,
        Death = 5,
        Dash = 6,
        StayDead = 7
    };

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        isPause = false;
        playerCanMove = true;
        isAlive = true;
    }

    private void OnEnable()
    {
        healthSystem.onDie += HealthSystem_onDie;
        SlimeController.onPlayerRecieveDamage += OnPlayerRecieveDamage_AnimateDamage;
    }

    private void Update()
    {
        if (playerCanMove)
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
    }

    private void OnDisable()
    {
        healthSystem.onDie -= HealthSystem_onDie;
        SlimeController.onPlayerRecieveDamage -= OnPlayerRecieveDamage_AnimateDamage;
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

            if (Input.GetKey(data.goUp) && !isJumping)
            {
                isJumping = true;
                playerRigidbody.velocityY = 0f;
                playerRigidbody.AddForce(Vector2.up * data.jumpForce, ForceMode2D.Impulse);
            }

            if (Input.GetKey(data.attack))
            {
                isAttacking = true;
                StartCoroutine(nameof(WaitingForNewAttack));
                onPlayerAttack?.Invoke(this);
            }

            if (Input.GetKey(data.dash) && !isResetingDash)
            {
                if (transform.rotation.y == 0)
                {
                    playerRigidbody.AddForce(Vector2.right * data.dashForce, ForceMode2D.Impulse);
                }
                else
                {
                    playerRigidbody.AddForce(Vector2.left * data.dashForce, ForceMode2D.Impulse);
                }
                StartCoroutine(nameof(DashReseting));
                onPlayerDash?.Invoke(this);
                isAnimatingDash = true;
            }
        }
    }

    public void Animate()
    {
        if (isAlive)
        {
            if (!isJumping && !isAnimatingDash && !isAnimatingHurt)
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
            else if (isJumping && !isAnimatingDash && !isAnimatingHurt)
            {
                onAnimating?.Invoke(this, (int)AnimationStates.Jump);
            }
            else if (isAnimatingDash)
            {
                onAnimating?.Invoke(this, (int)AnimationStates.Dash);
                StartCoroutine(nameof(DashAnimationReseting));
            }
            else if (isAnimatingHurt)
            {
                onAnimating?.Invoke(this, (int)AnimationStates.Hurt);
                StartCoroutine(nameof(HurtAnimationReseting));
            }
        }
    }

    public void OnPlayerRecieveDamage_AnimateDamage(SlimeController enemyController)
    {
        onPlayerHurt?.Invoke(this);
        isAnimatingHurt = true;
        if (transform.rotation.y == 0)
        {
            playerRigidbody.AddForce(Vector2.left * data.onHurtForce, ForceMode2D.Impulse);
        }
        else
        {
            playerRigidbody.AddForce(Vector2.right * data.onHurtForce, ForceMode2D.Impulse);
        }
    }

    public void HealthSystem_onDie()
    {
        onPlayerDie?.Invoke(this);
        playerCanMove = false;
        isAlive = false;
        onAnimating?.Invoke(this, (int)AnimationStates.Death);
        StartCoroutine(nameof(StayingDead));
    }

    private IEnumerator WaitingForNewAttack()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
        yield return null;
    }

    public IEnumerator DashReseting()
    {
        isResetingDash = true;
        yield return new WaitForSeconds(1.3f);
        isResetingDash = false;
        yield return null;
    }

    public IEnumerator DashAnimationReseting()
    {
        yield return new WaitForSeconds(0.55f);
        isAnimatingDash = false;
        yield return null;
    }
    public IEnumerator HurtAnimationReseting()
    {
        playerCanMove = false;
        yield return new WaitForSeconds(0.3f);
        playerCanMove = true;
        isAnimatingHurt = false;
        yield return null;
    }

    public IEnumerator StayingDead()
    {
        yield return new WaitForSeconds(.5f);
        onAnimating?.Invoke(this, (int)AnimationStates.StayDead);
        yield return null;
    }
}

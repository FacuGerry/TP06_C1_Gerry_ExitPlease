using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static event Action<PlayerController> onPlayerAttack;
    public static event Action<PlayerController> onPlayerDie;
    public static event Action<PlayerController> onPlayerJump;
    public static event Action<PlayerController> onPlayerDash;
    public static event Action<PlayerController> onPause;
    public static event Action<PlayerController> onResume;
    public static event Action<PlayerController, int> onAnimating;

    [SerializeField] private PlayerDataSo data;

    [SerializeField] private HealthSystem healthSystem;

    private Rigidbody2D playerRigidbody;

    [NonSerialized] public bool isJumping = false;
    [NonSerialized] public bool isWalking = false;
    [NonSerialized] public bool isAttacking = false;
    private bool isPause = false;

    private enum AnimationStates
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3,
        Hurt = 4,
        Death = 5
    };

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

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
        Animate();
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

            if (Input.GetKey(data.goUp) && !isJumping)
            {
                playerRigidbody.velocityY = 0f;
                playerRigidbody.AddForce(Vector2.up * data.jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                onPlayerJump?.Invoke(this);
            }

            if (Input.GetKey(data.attack))
            {
                isAttacking = true;
                StartCoroutine(nameof(WaitingForNewAttack));
                onPlayerAttack?.Invoke(this);
            }
        }
    }

    public void Animate()
    {
        if (isWalking && !isJumping)
        {
            onAnimating?.Invoke(this, (int)AnimationStates.Walk);
            isWalking = false;
        }
        else if (isJumping)
        {
            onAnimating?.Invoke(this, (int)AnimationStates.Jump);
        }
        else if (isAttacking && !isWalking && !isJumping)
        {
            onAnimating?.Invoke(this, (int)AnimationStates.Attack);
        }
        else
        {
            onAnimating?.Invoke(this, (int)AnimationStates.Idle);
        }
    }

    public void HealthSystem_onDie()
    {
        onPlayerDie?.Invoke(this);
        gameObject.SetActive(false);
    }

    private IEnumerator WaitingForNewAttack()
    {
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
        yield return null;
    }
}

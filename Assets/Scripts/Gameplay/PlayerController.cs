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
    [NonSerialized] public bool isPause = false;

    private bool isResetingDash = false;
    private bool isDashing = false;
    private bool isAlive = true;
    private bool enableLogs = true;

    private IEnumerator CoroutineAttack;
    private IEnumerator CoroutineDash;

    public enum AnimationStates
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3,
        Hurt = 4,
        Dash = 5,
        Death = 6
    };

    public AnimationStates currentState = AnimationStates.Idle;
    public AnimationStates previousState = AnimationStates.Death;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        isPause = false;
        isAlive = true;
    }

    private void OnEnable()
    {
        healthSystem.onDie += HealthSystem_onDie;
        SlimeController.onPlayerRecieveDamage += OnPlayerRecieveDamage_AnimateDamage;
    }

    private void Update()
    {
        if (!isPause && isAlive)
        {
            switch (currentState)
            {
                case AnimationStates.Idle:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                    }
                    //update
                    if (Input.GetKey(data.goUp) && !isJumping)
                    {
                        currentState = AnimationStates.Jump;
                    }
                    else if (Input.GetKey(data.goLeft) || Input.GetKey(data.goRight))
                    {
                        currentState = AnimationStates.Walk;
                    }
                    else if (Input.GetKey(data.attack))
                    {
                        currentState = AnimationStates.Attack;
                    }
                    else if (Input.GetKey(data.dash) && !isResetingDash)
                    {
                        currentState = AnimationStates.Dash;
                    }
                    //exit
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Salio del estado: " + currentState.ToString());
                    }
                    break;
                case AnimationStates.Walk:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                    }
                    //update
                    Movement();
                    if (Input.GetKey(data.goUp) && !isJumping)
                    {
                        currentState = AnimationStates.Jump;
                    }
                    else if (Input.GetKey(data.attack))
                    {
                        currentState = AnimationStates.Attack;
                    }
                    else if (Input.GetKey(data.dash) && !isResetingDash)
                    {
                        currentState = AnimationStates.Dash;
                    }
                    else if (!Input.GetKey(data.goLeft) && !Input.GetKey(data.goRight))
                    {
                        currentState = AnimationStates.Idle;
                    }
                    //exit
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Salio del estado: " + currentState.ToString());
                    }
                    break;
                case AnimationStates.Jump:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                        Jump();
                    }
                    //update
                    Movement();
                    if (Input.GetKey(data.attack))
                    {
                        currentState = AnimationStates.Attack;
                    }
                    else if (Input.GetKey(data.dash) && !isResetingDash)
                    {
                        currentState = AnimationStates.Dash;
                    }
                    else if (!isJumping)
                    {
                        currentState = AnimationStates.Idle;
                    }
                    //exit
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Salio del estado: " + currentState.ToString());
                    }
                    break;
                case AnimationStates.Attack:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                        Attack();
                    }
                    //update
                    if (!isAttacking)
                    {
                        currentState = AnimationStates.Idle;
                    }
                    //exit
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Salio del estado: " + currentState.ToString());
                    }
                    break;
                case AnimationStates.Hurt:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                        StartCoroutine(HurtAnimationReseting());
                    }
                    //update
                    if (!isAnimatingHurt)
                    {
                        currentState = AnimationStates.Idle;
                    }
                    //exit
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Salio del estado: " + currentState.ToString());
                    }
                    break;
                case AnimationStates.Dash:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                        Dash();
                    }
                    //update
                    if (!isDashing)
                    {
                        currentState = AnimationStates.Idle;
                    }
                    //exit
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Salio del estado: " + currentState.ToString());
                    }
                    break;
                case AnimationStates.Death:
                    // OnEnter
                    if (previousState != currentState)
                    {
                        if (enableLogs) Debug.Log("Entro a estado: " + currentState.ToString());
                        previousState = currentState;
                        onAnimating?.Invoke(this, (int)currentState);
                        isAlive = false;
                    }
                    //update
                    //exit
                    break;
            }
        }
        else if (!isAlive)
        {
            onAnimating?.Invoke(this, (int)AnimationStates.Death);
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
        SlimeController.onPlayerRecieveDamage -= OnPlayerRecieveDamage_AnimateDamage;

        StopAllCoroutines();
        CoroutineAttack = null;
        CoroutineDash = null;
    }

    public void Movement()
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
    }

    public void Jump()
    {
        isJumping = true;
        playerRigidbody.velocityY = 0f;
        playerRigidbody.AddForce(Vector2.up * data.jumpForce, ForceMode2D.Impulse);
        onPlayerJump?.Invoke(this);
    }

    public void Attack()
    {
        if (CoroutineAttack != null)
            StopCoroutine(CoroutineAttack);

        CoroutineAttack = WaitingForNewAttack();
        StartCoroutine(CoroutineAttack);

        onPlayerAttack?.Invoke(this);
    }

    public void Dash()
    {

        if (transform.rotation.y == 0)
        {
            playerRigidbody.AddForce(Vector2.right * data.dashForce, ForceMode2D.Impulse);
        }
        else
        {
            playerRigidbody.AddForce(Vector2.left * data.dashForce, ForceMode2D.Impulse);
        }
        onPlayerDash?.Invoke(this);

        if (CoroutineDash != null)
            StopCoroutine(CoroutineDash);

        CoroutineDash = DashReseting();
        StartCoroutine(CoroutineDash);

        isAnimatingDash = true;

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
        currentState = AnimationStates.Hurt;
    }

    public void HealthSystem_onDie()
    {
        onPlayerDie?.Invoke(this);
        isAlive = false;
        currentState = AnimationStates.Death;
    }

    public IEnumerator WaitingForNewAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    public IEnumerator DashReseting()
    {
        isResetingDash = true;
        isDashing = true;
        yield return new WaitForSeconds(data.dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(data.dashCooldown - data.dashDuration);
        isResetingDash = false;
    }

    public IEnumerator HurtAnimationReseting()
    {
        isAnimatingHurt = true;
        yield return new WaitForSeconds(0.3f);
        isAnimatingHurt = false;
    }
}

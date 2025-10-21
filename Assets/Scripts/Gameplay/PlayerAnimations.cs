using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private static readonly int state = Animator.StringToHash("State");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerController.onAnimating += OnAnimating_SetState;
    }

    private void OnDisable()
    {
        PlayerController.onAnimating -= OnAnimating_SetState;
    }

    public void OnAnimating_SetState(PlayerController playerController, int animInt)
    {
        animator.SetInteger(state, animInt);
    }
}

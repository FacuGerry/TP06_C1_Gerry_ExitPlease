using System.Collections;
using UnityEngine;

public class HandleCollisionGround : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    private bool isReseting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.isJumping = false;

        controller.canJumpTwice = false;

        if (controller.isDashing)
            controller.isDashing = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isReseting)
            StartCoroutine(nameof(DashReseting));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.isJumping = true;

        controller.canJumpTwice = true;
    }

    public IEnumerator DashReseting()
    {
        isReseting = true;
        yield return new WaitForSeconds(0.7f);
        controller.isDashing = false;
        isReseting = false;
        yield return null;
    }
}

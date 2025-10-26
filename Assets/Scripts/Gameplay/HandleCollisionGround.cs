using System.Collections;
using UnityEngine;

public class HandleCollisionGround : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.isJumping = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.isJumping = true;
    }


}

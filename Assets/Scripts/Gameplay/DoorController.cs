using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public static event Action<DoorController> onDoorCollisioned;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onDoorCollisioned?.Invoke(this);
    }
}

using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = transform.position;
    }

    private void Update()
    {
        cameraPosition.x = target.position.x;
        transform.position = cameraPosition;
    }
}
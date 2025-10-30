using UnityEngine;

public class DinoController : MonoBehaviour
{
    [SerializeField] private Vector3 distance;
    [SerializeField] private Vector3 offset;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Vector3.one + (transform.position + offset);
        Vector3 size = Vector3.one + distance;
        Gizmos.DrawWireCube(center, size);
    }
}

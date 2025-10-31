using UnityEngine;

public class DinoController : MonoBehaviour
{
    [SerializeField] private float distance;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 start = transform.position;
        Vector3 end = transform.position - transform.right * distance;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start, end);
    }
}

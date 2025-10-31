using UnityEngine;

public class VultureController : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float distance;

    private void Update()
    {
        Trying();
    }

    private void Trying()
    {
        /*Vector3 start = transform.position;
        Vector3 end = transform.position + transform.right * distance;
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, end.normalized, distance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != transform)
            {
                Debug.Log($"Hit: {hit.transform.gameObject.name}");
            }
        }*/
    }

    private void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + transform.right * distance;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(start, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, end);
    }
}

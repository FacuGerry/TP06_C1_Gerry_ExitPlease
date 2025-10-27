using UnityEngine;

public class OrbCounter : MonoBehaviour
{
    [SerializeField] private OrbController controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.numOfOrbs++;
        gameObject.SetActive(false);
    }
}

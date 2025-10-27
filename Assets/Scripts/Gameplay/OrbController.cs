using System;
using TMPro;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField] private GameObject winCollider;
    [SerializeField] private TextMeshProUGUI orbCounter;

    [NonSerialized] public int numOfOrbs = 0;

    void Start()
    {
        numOfOrbs = 0;
        winCollider.SetActive(false);
    }

    void Update()
    {
        if (numOfOrbs == 5)
        {
            winCollider.SetActive(true);
        }

        orbCounter.text = numOfOrbs.ToString("0");
    }
}

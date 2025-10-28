using System;
using TMPro;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField] private GameObject winCollider;
    [SerializeField] private TextMeshProUGUI orbCounter;
    [SerializeField] private TextMeshProUGUI necessaryOrbsText;
    [SerializeField] private int necessaryOrbs;

    [NonSerialized] public int numOfOrbs = 0;

    void Start()
    {
        numOfOrbs = 0;
        winCollider.SetActive(false);
        necessaryOrbsText.text = necessaryOrbs.ToString("0");
    }

    void Update()
    {
        if (numOfOrbs == necessaryOrbs)
        {
            winCollider.SetActive(true);
        }

        orbCounter.text = numOfOrbs.ToString("0");
    }
}

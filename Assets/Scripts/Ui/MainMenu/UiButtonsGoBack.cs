using System;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonsGoBack : MonoBehaviour
{
    public static event Action<UiButtonsGoBack> onBack;

    [SerializeField] private Button goBack;

    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        goBack.onClick.AddListener(GoBack);
    }

    private void OnDestroy()
    {
        goBack.onClick.RemoveAllListeners();
    }

    public void GoBack()
    {
        onBack?.Invoke(this);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UiButtonsGoBack : MonoBehaviour
{
    [SerializeField] private Button goBack;

    void Start()
    {
        goBack.onClick.AddListener(GoBack);
    }

    void OnDestroy()
    {
        goBack.onClick.RemoveAllListeners();
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
    }
}

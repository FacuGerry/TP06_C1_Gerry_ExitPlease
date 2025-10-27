using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
    public static event Action<UiMainMenu> onSettingsClicked;
    public static event Action<UiMainMenu> onCreditsClicked;
    public static event Action<UiMainMenu> onTutorialClicked;

    private CanvasGroup canvas;

    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnTutorial;
    [SerializeField] private Button btnExit;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        btnPlay.onClick.AddListener(PlayClicked);
        btnSettings.onClick.AddListener(SettingsClicked);
        btnCredits.onClick.AddListener(CreditsClicked);
        btnTutorial.onClick.AddListener(TutorialClicked);
        btnExit.onClick.AddListener(ExitClicked);

        canvas.interactable = true;
        canvas.alpha = 1.0f;
        canvas.blocksRaycasts = true;

        Time.timeScale = 1.0f;
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnTutorial.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();

        StopAllCoroutines();
    }

    private void PlayClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void SettingsClicked()
    {
        onSettingsClicked?.Invoke(this);
        CanvasManageDisappear();
    }

    private void CreditsClicked()
    {
        onCreditsClicked?.Invoke(this);
        CanvasManageDisappear();
    }

    private void TutorialClicked()
    {
        onTutorialClicked?.Invoke(this);
        CanvasManageDisappear();
    }

    private void ExitClicked()
    {
        Application.Quit();
    }

    private void CanvasManageDisappear()
    {
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}

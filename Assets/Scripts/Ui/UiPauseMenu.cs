using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPauseMenu : MonoBehaviour
{

    public static event Action<UiPauseMenu> onSettingsOpen;

    private CanvasGroup canvas;
    [SerializeField] private PlayerController player;
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnMainMenu;

    private void Awake()
    {
        btnResume.onClick.AddListener(OnBtnResumeClicked);
        btnSettings.onClick.AddListener(OnBtnSettingsClicked);
        btnMainMenu.onClick.AddListener(OnBtnMainMenuClicked);

        canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        CanvasToggle(0, false);
        Time.timeScale = 1.0f;
    }

    private void OnEnable()
    {
        PlayerController.onPause += OnPause_PanelAppear;
        PlayerController.onResume += OnResume_PanelDisappear;
    }

    private void OnDisable()
    {
        PlayerController.onPause -= OnPause_PanelAppear;
        PlayerController.onResume -= OnResume_PanelDisappear;
    }

    private void OnDestroy()
    {
        btnResume.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnMainMenu.onClick.RemoveAllListeners();
    }

    public void OnPause_PanelAppear(PlayerController playerController)
    {
        CanvasToggle(1, true);
        Time.timeScale = 0f;
    }

    public void OnResume_PanelDisappear(PlayerController playerController)
    {
        CanvasToggle(0, false);
        Time.timeScale = 1.0f;
        player.isPause = false;
    }

    public void OnBtnResumeClicked()
    {
        CanvasToggle(0, false);
        Time.timeScale = 1.0f;
        player.isPause = false;
    }

    public void OnBtnSettingsClicked()
    {
        CanvasToggle(0, false);
        onSettingsOpen?.Invoke(this);
    }

    public void OnBtnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CanvasToggle(int alpha, bool appear)
    {
        canvas.alpha = alpha;
        canvas.interactable = appear;
        canvas.blocksRaycasts = appear;
    }
}

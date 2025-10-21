using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnMainMenu;

    private void Awake()
    {
        btnResume.onClick.AddListener(OnBtnResumeClicked);
        btnMainMenu.onClick.AddListener(OnBtnMainMenuClicked);
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
        btnMainMenu.onClick.RemoveAllListeners();
    }

    public void OnPause_PanelAppear(PlayerController playerController)
    {
        panelPause.SetActive(true);
    }

    public void OnResume_PanelDisappear(PlayerController playerController)
    {
        panelPause.SetActive(false);
    }

    public void OnBtnResumeClicked()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnBtnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

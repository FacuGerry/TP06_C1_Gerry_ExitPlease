using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelCredits;

    private void Start()
    {
        btnPlay.onClick.AddListener(PlayClicked);
        btnSettings.onClick.AddListener(SettingsClicked);
        btnCredits.onClick.AddListener(CreditsClicked);
        btnExit.onClick.AddListener(ExitClicked);

        Time.timeScale = 1.0f;
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    private void PlayClicked()
    {
        SceneManager.LoadScene("GameLevel1");
    }

    private void SettingsClicked()
    {
        panelSettings.SetActive(true);
    }

    private void CreditsClicked()
    {
        panelCredits.SetActive(true);
    }

    private void ExitClicked()
    {
        Application.Quit();
    }

}

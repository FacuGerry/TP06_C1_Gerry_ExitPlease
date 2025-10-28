using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UiPlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnMainMenu;

    private void Start()
    {
        btnReplay.onClick.AddListener(ReplayClicked);
        btnMainMenu.onClick.AddListener(MainMenuClicked);
    }

    private void OnEnable()
    {
        PlayerController.onPlayerDie += OnPlayerDie_LoseUi;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDie -= OnPlayerDie_LoseUi;
    }

    private void OnDestroy()
    {
        btnReplay.onClick.RemoveAllListeners();
        btnMainMenu.onClick.RemoveAllListeners();
    }

    public void ReplayClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlayerDie_LoseUi(PlayerController playerController)
    {
        StartCoroutine(nameof(WaitingForLoseScreen));
    }

    public IEnumerator WaitingForLoseScreen()
    {
        yield return new WaitForSeconds(3f);
        loseScreen.SetActive(true);
        yield return null;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiPlayerDie : MonoBehaviour
{
    [SerializeField] private CoinsDataSo coinsData;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject coinsCounter;
    [SerializeField] private TextMeshProUGUI coinsNum;
    [SerializeField] private TextMeshProUGUI totalCoinsNum;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private string sceneToLoad;

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
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1f;
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void OnPlayerDie_LoseUi(PlayerController playerController)
    {
        Time.timeScale = 0;
        coinsNum.text = coinsData.coins.ToString("0");
        totalCoinsNum.text = coinsData.totalCoins.ToString("0");
        coinsCounter.SetActive(false);
        loseScreen.SetActive(true);
    }
}

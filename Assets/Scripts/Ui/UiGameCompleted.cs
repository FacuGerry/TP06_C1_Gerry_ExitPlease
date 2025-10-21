using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGameCompleted : MonoBehaviour
{
    [SerializeField] private CoinsDataSo coinsData;
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private GameObject coinsCounter;
    [SerializeField] private TextMeshProUGUI coinsNum;
    [SerializeField] private TextMeshProUGUI totalCoinsNum;

    private void Start()
    {
        btnReplay.onClick.AddListener(ReplayClicked);
        btnMainMenu.onClick.AddListener(MainMenuClicked);
    }

    private void OnEnable()
    {
        DoorController.onDoorCollisioned += OnDoorCollisioned_LevelCompleteUiAppear;
    }

    private void OnDisable()
    {
        DoorController.onDoorCollisioned -= OnDoorCollisioned_LevelCompleteUiAppear;
    }

    private void OnDestroy()
    {
        btnReplay.onClick.RemoveAllListeners();
        btnMainMenu.onClick.RemoveAllListeners();
    }

    public void OnDoorCollisioned_LevelCompleteUiAppear(DoorController doorController)
    {
        Time.timeScale = 0f;
        coinsNum.text = coinsData.coins.ToString("0");
        totalCoinsNum.text = coinsData.totalCoins.ToString("0");
        coinsCounter.SetActive(false);
        levelCompleted.SetActive(true);
    }

    public void ReplayClicked()
    {
        SceneManager.LoadScene("GameLevel1");
        Time.timeScale = 1f;
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}

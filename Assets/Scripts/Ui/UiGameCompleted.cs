using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGameCompleted : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnMainMenu;

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
        levelCompleted.SetActive(true);
    }

    public void ReplayClicked()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1f;
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}

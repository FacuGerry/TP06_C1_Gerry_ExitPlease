using UnityEngine;
using UnityEngine.SceneManagement;

public class UiLevelCompleted : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float timer;

    private float timerValue;
    private bool isFinished = false;

    private void Start()
    {
        timerValue = timer;
    }

    private void OnEnable()
    {
        DoorController.onDoorCollisioned += OnDoorCollisioned_LevelCompleteUiAppear;
    }

    private void Update()
    {
        if (isFinished)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                levelCompleted.SetActive(false);
                SceneManager.LoadScene(sceneToLoad);
                timer = timerValue;
                isFinished = false;
            }
        }
    }

    private void OnDisable()
    {
        DoorController.onDoorCollisioned -= OnDoorCollisioned_LevelCompleteUiAppear;
    }

    public void OnDoorCollisioned_LevelCompleteUiAppear(DoorController doorController)
    {
        isFinished = true;
        //Aca podría agregar algo que "pause" el juego pero al mismo tiempo no
        levelCompleted.SetActive(true);
    }
}

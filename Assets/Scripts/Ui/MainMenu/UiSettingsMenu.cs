using UnityEngine;

public class UiSettingsMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasToToggle;
    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        UiMainMenu.onSettingsClicked += OnSettingsClicked_CanvasAppear;
        UiButtonsGoBack.onBack += OnBack_CanvasDisappear;
        UiPauseMenu.onSettingsOpen += OnSettingsClickedInGame_CanvasAppear;
    }

    private void OnDisable()
    {
        UiMainMenu.onSettingsClicked -= OnSettingsClicked_CanvasAppear;
        UiButtonsGoBack.onBack -= OnBack_CanvasDisappear;
        UiPauseMenu.onSettingsOpen -= OnSettingsClickedInGame_CanvasAppear;
    }

    public void OnSettingsClicked_CanvasAppear(UiMainMenu uiMainMenu)
    {
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void OnBack_CanvasDisappear(UiButtonsGoBack uiButtonsGoBack)
    {
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
        canvasToToggle.alpha = 1f;
        canvasToToggle.interactable = true;
        canvasToToggle.blocksRaycasts = true;
    }

    public void OnSettingsClickedInGame_CanvasAppear(UiPauseMenu uiPauseMenu)
    {
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}

using UnityEngine;

public class UiCreditsMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup MainMenuCanvas;
    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        UiMainMenu.onCreditsClicked += OnCreditsClicked_CanvasAppear;
        UiButtonsGoBack.onBack += OnBack_CanvasDisappear;
    }

    private void OnDisable()
    {
        UiMainMenu.onCreditsClicked -= OnCreditsClicked_CanvasAppear;
        UiButtonsGoBack.onBack -= OnBack_CanvasDisappear;
    }

    public void OnCreditsClicked_CanvasAppear(UiMainMenu uiMainMenu)
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
        MainMenuCanvas.alpha = 1f;
        MainMenuCanvas.interactable = true;
        MainMenuCanvas.blocksRaycasts = true;
    }
}

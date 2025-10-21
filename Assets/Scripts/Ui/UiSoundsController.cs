using UnityEngine;
using UnityEngine.UI;

public class UiSoundsController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SoundsDataSo data;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = data.masterVol;
        musicSlider.value = data.musicVol;
        sfxSlider.value = data.sfxVol;
    }

    private void Update()
    {
        Listener();
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }

    public void Listener()
    {
        masterSlider.onValueChanged.AddListener(OnMasterChanged);
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    public void OnMasterChanged(float vol)
    {
        data.masterVol = vol;
        data.mixer.SetFloat("MasterVol", vol);
    }

    public void OnMusicChanged(float vol)
    {
        data.musicVol = vol;
        data.mixer.SetFloat("MusicVol", vol);
    }

    public void OnSFXChanged(float vol)
    {
        data.sfxVol = vol;
        data.mixer.SetFloat("SFXVol", vol);
    }
}

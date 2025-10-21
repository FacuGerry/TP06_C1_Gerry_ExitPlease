using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Data/SoundSettings")]

public class SoundsDataSo : ScriptableObject
{
    public AudioMixer mixer;
    public float masterVol = 0;
    public float musicVol = 0;
    public float sfxVol = 0;
}

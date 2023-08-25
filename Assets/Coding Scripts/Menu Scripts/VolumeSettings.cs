using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider, musicSlider, sfxSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";

    [SerializeField] public const float DEFAULT_VOLUME = -30f;


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(MusicSoundHandler.MASTER_KEY, DEFAULT_VOLUME);
        musicSlider.value = PlayerPrefs.GetFloat(MusicSoundHandler.MUSIC_KEY, DEFAULT_VOLUME);
        sfxSlider.value = PlayerPrefs.GetFloat(MusicSoundHandler.SFX_KEY, DEFAULT_VOLUME);
    }

    private void OnDisable() 
    {
        SaveSoundSettings();
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicSoundHandler.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(MusicSoundHandler.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(MusicSoundHandler.SFX_KEY, sfxSlider.value);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, value);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, value);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, value);
    }
    
    

}

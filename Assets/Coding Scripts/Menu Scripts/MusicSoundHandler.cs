using UnityEngine;
using UnityEngine.Audio;

public class MusicSoundHandler : MonoBehaviour
{
    public static MusicSoundHandler Instance;

    [SerializeField] AudioMixer mixer;
    [SerializeField] private AudioSource musicSource, effectSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] buttonSFX;
    [SerializeField] AudioClip[] otherSFX;
    [SerializeField] AudioClip[] music;

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";


    private void Awake() 
    {    
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start() 
    {
        LoadVolume();
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip _clip)
    {
        musicSource.clip = _clip;
        musicSource.Play();
    }

    public void PlayButtonSFX()
    {
        int value = Random.Range(0,buttonSFX.Length);
        Debug.Log(value);
        effectSource.clip = buttonSFX[value];
        effectSource.Play();
    }

    private void LoadVolume() //volume saved in volumeSettings.cs
    {
        mixer.SetFloat(VolumeSettings.MIXER_MASTER, PlayerPrefs.GetFloat(MASTER_KEY, VolumeSettings.DEFAULT_VOLUME));
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, PlayerPrefs.GetFloat(MUSIC_KEY, VolumeSettings.DEFAULT_VOLUME));
        mixer.SetFloat(VolumeSettings.MIXER_SFX, PlayerPrefs.GetFloat(SFX_KEY, VolumeSettings.DEFAULT_VOLUME));
    }
}

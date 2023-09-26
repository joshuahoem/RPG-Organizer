using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JukeBoxHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown musicDropdown;
    [SerializeField] private AudioClip[] music;
    List<string> musicNames = new List<string>();
    Dictionary<string, AudioClip> dictionaryForMusic = new Dictionary<string, AudioClip>();

    public const string MUSIC_SAVED_KEY = "savedMusic";

    public string GetDefualtSong()
    {
        return music[0].ToString();
    }

    public void PlaySong(string key)
    {
        if (MusicSoundHandler.Instance != null)
        {
            MusicSoundHandler.Instance.PlayMusic(dictionaryForMusic[key]);
        }
    }

    private void Awake() 
    {
        if (musicDropdown != null)
        {
            musicDropdown.ClearOptions();
        }

        for(int i = 0; i< music.Length; i++)
        {
            string songName = music[i].name;
            musicNames.Add(songName);
            dictionaryForMusic.Add(songName, music[i]);
        }

        if (musicDropdown != null)
        {
            musicDropdown.AddOptions(musicNames);
            musicDropdown.RefreshShownValue();
        }
    }

    public void SongChange()
    {
        string key = musicDropdown.options[musicDropdown.value].text;
        MusicSoundHandler.Instance.PlayMusic(dictionaryForMusic[key]);
        PlayerPrefs.SetString(MUSIC_SAVED_KEY, key);
    }
}

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

    private void Start() 
    {
        musicDropdown.ClearOptions();
        for(int i = 0; i< music.Length; i++)
        {
            string songName = music[i].name;
            musicNames.Add(songName);
            dictionaryForMusic.Add(songName, music[i]);
        }

        musicDropdown.AddOptions(musicNames);
        musicDropdown.RefreshShownValue();
    }

    public void SongChange()
    {
        MusicSoundHandler.Instance.PlayMusic(dictionaryForMusic[musicDropdown.options[musicDropdown.value].text]);
    }
}

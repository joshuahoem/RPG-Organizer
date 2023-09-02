using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanelManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel, extraPanel, touchToStartStringObject;
    [SerializeField] GameObject[] panels;

    int hasStarted = 0; //false

    private void Start() 
    {
        // Debug.Log(PlayerPrefs.GetInt("hasStarted"));
        NewSaveSystem.Init();

        if (PlayerPrefs.HasKey("hasStarted"))
        {
            hasStarted = PlayerPrefs.GetInt("hasStarted");
        }

        if (hasStarted == 1)
        {
            DeactivateAllPanels();
            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(true);
            }
        }
        else
        {
            DeactivateAllPanels();
            if (touchToStartStringObject != null)
            {
                touchToStartStringObject.SetActive(true);
            }
        }        
    }

    public void FirstSwitchToMainPanel()
    {
        SwitchToMainPanel();
        if (touchToStartStringObject != null)
        {
            touchToStartStringObject.SetActive(false);
        }
        PlayerPrefs.SetInt("hasStarted", 1);
    }

    public void SwitchToMainPanel()
    {
        DeactivateAllPanels();
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        PlaySound();
    }

    public void SwitchToExtrasPanel()
    {
        DeactivateAllPanels();
        if (extraPanel != null)
        {
            extraPanel.SetActive(true);
        }

        PlaySound();
    }

    private void DeactivateAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    // private void Update() {
    //     if(Input.GetKey("a"))
    //     {
    //         PlayerPrefs.SetInt("hasStarted", 0);
    //     }
    // }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("hasStarted", 0);

        PlayerInfo info = NewSaveSystem.FindPlayerInfoFile();
        info.unlocks.Clear();
        NewSaveSystem.SavePlayerInfo(info);

        SaveObject save = NewSaveSystem.FindCurrentSave();
        save.abilityInventory.Clear();
        save.inventory.Clear();
        save.perks.Clear();
        NewSaveSystem.SaveChanges(save);
    }

    private void PlaySound()
    {
        MusicSoundHandler musicSoundHandler = FindObjectOfType<MusicSoundHandler>();
        if (musicSoundHandler != null)
        {
            musicSoundHandler.PlayButtonSFX();
        }
    }
}

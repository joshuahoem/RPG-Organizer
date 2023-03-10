using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterUnlockItemInfo : MonoBehaviour
{
    [SerializeField] public Race raceToUnlock;
    [SerializeField] public Class classToUnlock;
    private UnlockObject newUnlock;


    [SerializeField] GameObject lockImage;
    [SerializeField] Image characterImage;
    [SerializeField] Image bgOject;
    [SerializeField] TextMeshProUGUI nameTMPro;
    public event System.EventHandler OnNewRaceOrClassUnlocked;
    public event System.EventHandler<SelectedClassEventArgs> onSelectedRaceOrClass;


    private void Start() {
        DisplayName();
    }

    public void DisplayName()
    {
        if (classToUnlock != null)
        {
            nameTMPro.text = classToUnlock.name;
        }
        else if (raceToUnlock != null)
        {
            nameTMPro.text = raceToUnlock.name;
        }
        else
        {
            nameTMPro.text = "???";
        }
    }

    public void DisplayUnlocked()
    {
        lockImage.SetActive(false);
        if (raceToUnlock != null)
        {
            characterImage.sprite = raceToUnlock.picture;
            bgOject.color = raceToUnlock.imageColor;
        }
        if (classToUnlock != null)
        {
            characterImage.sprite = classToUnlock.logo;
            bgOject.color = classToUnlock.imageColor;
        }
    }

    public void ClickToUnlock()
    {
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();

        if(raceToUnlock != null)
        {
            newUnlock = new UnlockObject(raceToUnlock, null);
        }
        else if (classToUnlock != null)
        {
            newUnlock = new UnlockObject(null, classToUnlock);
        }

        playerInfo.unlocks.Add(newUnlock);
        NewSaveSystem.SavePlayerInfo(playerInfo);

        OnNewRaceOrClassUnlocked?.Invoke(this, EventArgs.Empty);
    }

    public void ClickToSelect()
    {
        onSelectedRaceOrClass?.Invoke(this, new SelectedClassEventArgs 
            {selectedClass = classToUnlock, selectedRace = raceToUnlock});
    }

    public class SelectedClassEventArgs : EventArgs
    {
        public Race selectedRace;
        public Class selectedClass;
    }
}

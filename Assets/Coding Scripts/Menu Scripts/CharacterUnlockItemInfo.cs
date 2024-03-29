using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class CharacterUnlockItemInfo : MonoBehaviour
{
    [SerializeField] public Race raceToUnlock;
    [SerializeField] public Class classToUnlock;
    
    private UnlockObject newUnlock;
    bool thisItemIsUnlocked = false;


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
            // characterImage.sprite = SaveManagerVersion3.LoadSprite(raceToUnlock.pathToPicture);
            characterImage.sprite = raceToUnlock.picture;
            bgOject.color = raceToUnlock.imageColor;
        }
        if (classToUnlock != null)
        {
            // characterImage.sprite = SaveManagerVersion3.LoadSprite(classToUnlock.pathToPicture);
            characterImage.sprite = classToUnlock.picture;
            bgOject.color = classToUnlock.imageColor;
        }
    }

    public void ClickToUnlock()
    {
        PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile();
        thisItemIsUnlocked = false;
        newUnlock = null;

        if(raceToUnlock != null)
        {
            newUnlock = new UnlockObject(raceToUnlock, null, raceToUnlock.name, "");
        }
        else if (classToUnlock != null)
        {
            newUnlock = new UnlockObject(null, classToUnlock, "", classToUnlock.name);
        }
        else //(newUnlock == null)
        {
            Debug.Log("error - no new unlock");
            return;
        }

        foreach (UnlockObject unlock in playerInfo.unlocks)
        {

            if ((newUnlock.raceStringID == unlock.raceStringID && unlock.raceStringID != "") || (newUnlock.classStringID == unlock.classStringID && unlock.classStringID != ""))
            {
                // Debug.Log("found result");
                thisItemIsUnlocked = true;
            }
        }

        if (!thisItemIsUnlocked)
        {
            // Debug.Log("new one added " + newUnlock.unlockedClass + " " + newUnlock.unlockedRace);
            playerInfo.unlocks.Add(newUnlock);
            SaveManagerVersion3.SavePlayerInfo(playerInfo);

            OnNewRaceOrClassUnlocked?.Invoke(this, EventArgs.Empty);

            thisItemIsUnlocked = true;
        }

        
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

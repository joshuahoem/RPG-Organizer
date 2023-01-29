using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterUnlockManager : MonoBehaviour
{
    [SerializeField] List<GameObject> unlockObjects = new List<GameObject>();
    [SerializeField] List<Race> defaultRaces = new List<Race>();
    [SerializeField] List<Class> defaultClasses = new List<Class>();


    private void Start() 
    {
        CharacterUnlockItemInfo[] unlockItemsInfo = FindObjectsOfType<CharacterUnlockItemInfo>();
        foreach (CharacterUnlockItemInfo go in unlockItemsInfo)
        {
            go.OnNewRaceOrClassUnlocked += Subscriber_OnEventClicked;
        }

        LoadDefaults();
        LoadUnlocks();
    }

    private void Update() {
        ResetUnlocks();
    }

    private void LoadDefaults()
    {
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();

        if (playerInfo.unlocks.Count <= 1)
        {
            Debug.Log("Loading");
            foreach (Race race in defaultRaces)
            {
                UnlockObject unlockObject = new UnlockObject(race, null);
                playerInfo.unlocks.Add(unlockObject);
            }

            foreach (Class characterClass in defaultClasses)
            {
                UnlockObject unlockObject = new UnlockObject(null, characterClass);
                playerInfo.unlocks.Add(unlockObject);
            }

            NewSaveSystem.SavePlayerInfo(playerInfo);

            foreach (UnlockObject unlock in playerInfo.unlocks)
            {
                Debug.Log(unlock.unlockedClass + " " + unlock.unlockedRace);
            }
        }
    }

    private void LoadUnlocks()
    {
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();        
        
        foreach (GameObject unlockGO in unlockObjects)
        {
            CharacterUnlockItemInfo goItemInfo = unlockGO.GetComponent<CharacterUnlockItemInfo>();
            foreach (UnlockObject unlock in playerInfo.unlocks)
            {
                if (unlock.unlockedClass == goItemInfo.classToUnlock && unlock.unlockedClass != null)
                {
                    goItemInfo.DisplayUnlocked();
                    break;
                }

                if (unlock.unlockedRace == goItemInfo.raceToUnlock && unlock.unlockedRace != null)
                {
                    goItemInfo.DisplayUnlocked();
                    break;
                }
            }
        }
    }

    public void SwitchPanels()
    {
        CharacterUnlockItemInfo[] unlockItemsInfo = FindObjectsOfType<CharacterUnlockItemInfo>();
        Debug.Log(unlockItemsInfo.Length);
        foreach (CharacterUnlockItemInfo go in unlockItemsInfo)
        {
            go.OnNewRaceOrClassUnlocked += Subscriber_OnEventClicked;
        }
    }

    private void Subscriber_OnEventClicked(object sender, EventArgs e)
    {
        Debug.Log("click");
        LoadUnlocks();
    }

    private void ResetUnlocks()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Clear Unlocks");
            PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();
            playerInfo.unlocks.Clear();
            NewSaveSystem.SavePlayerInfo(playerInfo);
        }
    } 
}

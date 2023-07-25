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

        Debug.Log("josh " + playerInfo.unlocks.Count + " unlocked");

        if (playerInfo.unlocks.Count <= 1)
        {
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

            // foreach (UnlockObject unlock in playerInfo.unlocks)
            // {
            //     Debug.Log(unlock.unlockedClass + " " + unlock.unlockedRace);
            // }
        }
    }

    private void LoadUnlocks()
    {
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile(); 

        Debug.Log("Josh thinks in unlocks there are " + playerInfo.unlocks.Count);       

        foreach (GameObject unlockGO in unlockObjects)
        {
            CharacterUnlockItemInfo goItemInfo = unlockGO.GetComponent<CharacterUnlockItemInfo>();
            foreach (UnlockObject unlock in playerInfo.unlocks)
            {
                if (unlock.unlockedClass != null && goItemInfo.classToUnlock != null)
                {
                    if (unlock.unlockedClass.name == goItemInfo.classToUnlock.name)
                    {
                        goItemInfo.DisplayUnlocked();
                        break;
                    }
                }

                if (unlock.unlockedRace != null && goItemInfo.raceToUnlock != null)
                {
                    if (unlock.unlockedRace.name == goItemInfo.raceToUnlock.name)
                    {
                        goItemInfo.DisplayUnlocked();
                        break;
                    }
                }
            }
        }
    }

    public void SwitchPanels()
    {
        CharacterUnlockItemInfo[] unlockItemsInfo = FindObjectsOfType<CharacterUnlockItemInfo>();
        foreach (CharacterUnlockItemInfo go in unlockItemsInfo)
        {
            go.OnNewRaceOrClassUnlocked += Subscriber_OnEventClicked;
        }
    }

    private void Subscriber_OnEventClicked(object sender, EventArgs e)
    {
        LoadUnlocks();
    }

    private void ResetUnlocks()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Clear Unlocks");
            PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();
            playerInfo.unlocks.Clear();
            NewSaveSystem.SavePlayerInfo(playerInfo);
            LoadDefaults();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("check unlocks");
            PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();
            foreach (UnlockObject unlock in playerInfo.unlocks)
            {
                if(unlock.unlockedRace != null)
                {
                    Debug.Log(unlock.unlockedRace);
                }
                if (unlock.unlockedClass != null)
                {
                    Debug.Log(unlock.unlockedClass);
                }
            }
            Debug.Log(playerInfo.unlocks.Count);

        }

    } 
}

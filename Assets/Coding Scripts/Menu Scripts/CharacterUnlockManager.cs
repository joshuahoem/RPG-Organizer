using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterUnlockManager : MonoBehaviour
{
    [SerializeField] List<GameObject> unlockObjects = new List<GameObject>();
    [SerializeField] List<Race> defaultRaces = new List<Race>();
    [SerializeField] List<Class> defaultClasses = new List<Class>();
    [SerializeField] RaceDatabase raceDatabase;
    [SerializeField] ClassDatabase classDatabase;

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

    // private void Update() {
    //     ResetUnlocks();
    // }

    private void LoadDefaults()
    {
        PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile();

        // Debug.Log("josh " + playerInfo.unlocks.Count + " unlocked");

        if (playerInfo.unlocks.Count <= 1)
        {
            foreach (Race race in defaultRaces)
            {
                UnlockObject unlockObject = new UnlockObject(race, null, race.name, "");
                playerInfo.unlocks.Add(unlockObject);
            }

            foreach (Class characterClass in defaultClasses)
            {
                UnlockObject unlockObject = new UnlockObject(null, characterClass, "", characterClass.name);
                playerInfo.unlocks.Add(unlockObject);
            }
            
        }

        //Load new instance between saves (close and reload app)
        foreach (UnlockObject unlock in playerInfo.unlocks)
        {
            if (unlock.unlockedClass == null && unlock.classStringID != "" && unlock.raceStringID == "")
            {
                unlock.unlockedClass = classDatabase.GetClassID[unlock.classStringID];  
            }
            if (unlock.unlockedRace == null && unlock.raceStringID != "" && unlock.classStringID == "")
            {
                unlock.unlockedRace = raceDatabase.GetRaceID[unlock.raceStringID];
            }
        }

        SaveManagerVersion3.SavePlayerInfo(playerInfo);

    }

    private void LoadUnlocks()
    {
        PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile(); 

        // foreach (UnlockObject unlock in playerInfo.unlocks)
        // {
        //     Debug.Log(" Josh unlocked " + unlock.unlockedClass + " " + unlock.unlockedRace);
        // }

        foreach (GameObject unlockGO in unlockObjects)
        {
            CharacterUnlockItemInfo goItemInfo = unlockGO.GetComponent<CharacterUnlockItemInfo>();
            foreach (UnlockObject unlock in playerInfo.unlocks)
            {
                if (unlock.classStringID != null && goItemInfo.classToUnlock != null)
                {

                    if (unlock.classStringID == goItemInfo.classToUnlock.name)
                    {
                        goItemInfo.DisplayUnlocked();
                        break;
                    }
                }

                if (unlock.raceStringID != null && goItemInfo.raceToUnlock != null)
                {
                    if (unlock.raceStringID == goItemInfo.raceToUnlock.name)
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
            PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile();
            playerInfo.unlocks.Clear();
            SaveManagerVersion3.SavePlayerInfo(playerInfo);
            LoadDefaults();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("check unlocks");
            PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile();
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

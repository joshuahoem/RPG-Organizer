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
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();

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
                // Debug.Log("class Start");
                // Debug.Log(unlock.unlockedClass + " class");
                // Debug.Log(unlock.classStringID + " class string");
                // Debug.Log(unlock.unlockedRace + " race");
                // Debug.Log(unlock.raceStringID + " race string");
                unlock.unlockedClass = classDatabase.GetClassID[unlock.classStringID];  
                // Debug.Log(classDatabase.GetClassID[unlock.classStringID] + " set class");              
            }
            if (unlock.unlockedRace == null && unlock.raceStringID != "" && unlock.classStringID == "")
            {
                // Debug.Log("Race Start");
                // Debug.Log(unlock.unlockedClass + " class");
                // Debug.Log(unlock.classStringID + " class string");
                // Debug.Log(unlock.unlockedRace + " race");
                // Debug.Log(unlock.raceStringID + " race string");
                unlock.unlockedRace = raceDatabase.GetRaceID[unlock.raceStringID];
                // Debug.Log(raceDatabase.GetRaceID[unlock.raceStringID] + " set race");
            }
        }

        NewSaveSystem.SavePlayerInfo(playerInfo);

    }

    private void LoadUnlocks()
    {
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile(); 

        // foreach (UnlockObject unlock in playerInfo.unlocks)
        // {
        //     Debug.Log(" Josh unlocked " + unlock.unlockedClass + " " + unlock.unlockedRace);
        // }

        // Debug.Log("Josh thinks in unlocks there are " + playerInfo.unlocks.Count);       

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

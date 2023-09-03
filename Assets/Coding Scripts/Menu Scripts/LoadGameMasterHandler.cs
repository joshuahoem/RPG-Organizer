using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameMasterHandler : MonoBehaviour
{
    [SerializeField] private ClassDatabase classDatabase; 
    [SerializeField] private RaceDatabase raceDatabase; 
    [SerializeField] private AbilityDatabase abilityDatabase; 
    [SerializeField] private PerkDatabase perkDatabase;
    [SerializeField] private ItemDatabase itemDatabase;
    public static LoadGameMasterHandler Instance { get; private set; }

    private void Awake() 
    {    
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }

    private void Start() 
    {
        StartMusic();
        NewSaveSystem.Init();
        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();
        SaveState saveState = NewSaveSystem.FindSaveState();

        //SUMMARY// Make sure player has correct Classes and Races
        foreach (UnlockObject unlock in playerInfo.unlocks)
        {
            if (unlock.raceStringID != string.Empty && unlock.classStringID == string.Empty)
            {
                //has a race
                unlock.unlockedRace = raceDatabase.GetRaceID[unlock.raceStringID];
            }
            else if (unlock.raceStringID == string.Empty && unlock.classStringID != string.Empty)
            {
                //has a class
                unlock.unlockedClass = classDatabase.GetClassID[unlock.classStringID];
            }
            else
            {
                Debug.Log("Error with race or class");
            }
        }

        //SUMMARY// Finds each save and makes sure they have correct items, abilities, and perks
        int numberOfCharacters = saveState.numberOfCharacters;
        for (int i = 0; i <= numberOfCharacters; i++)
        {
            SaveObject save = NewSaveSystem.Load(i);

            if (save == null) { continue; }

            foreach (AbilitySaveObject abilitySO in save.abilityInventory)
            {
                if (abilitySO.ability == null)
                {
                    Debug.Log(abilitySO.ability.abilityName + " name");
                    Debug.Log("chosen : " + abilityDatabase.GetStringID[abilitySO.stringID].abilityName);
                    abilitySO.ability = abilityDatabase.GetStringID[abilitySO.stringID];
                }
            }

        }

    }

    private void StartMusic()
    {
        JukeBoxHandler jukeBoxHandler = FindObjectOfType<JukeBoxHandler>();
        string defaultMusic = jukeBoxHandler.GetDefualtSong();
        string musicToPlay = PlayerPrefs.GetString(JukeBoxHandler.MUSIC_SAVED_KEY, defaultMusic);

        jukeBoxHandler.PlaySong(musicToPlay);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;


public class LoadGameMasterHandler : MonoBehaviour
{
    [SerializeField] private ClassDatabase classDatabase; 
    [SerializeField] private RaceDatabase raceDatabase; 
    [SerializeField] private AbilityDatabase abilityDatabase; 
    [SerializeField] private PerkDatabase perkDatabase;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private SpriteAtlas itemAtlas;
    [SerializeField] private SpriteAtlas abilitiesAtlas;
    [SerializeField] private SpriteAtlas raceAndClassAtlas;

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

    public PlayerInfo GetPlayerInfo()
    {
        PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile();

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

        return playerInfo;
    }

    public Perk GetPerk(string _perkID)
    {
        if (_perkID == null)
        {
            return null;
        }
        else
        {
            return perkDatabase.GetStringID[_perkID];
        }
    }

    public Item GetItem(string _itemID)
    {
        if (_itemID == null)
        {
            return null;
        }
        else
        {
            return itemDatabase.GetItem[_itemID];
        }
    }

    public Ability GetAbility(string _abilityID)
    {
        if (_abilityID == null) 
        { 
            return null;
        }
        else
        {
            return abilityDatabase.GetStringID[_abilityID];
        }
    }

    private void Start() 
    {
        StartMusic();
        SaveManagerVersion3.Init();
        PlayerInfo playerInfo = SaveManagerVersion3.FindPlayerInfoFile();
        SaveState saveState = SaveManagerVersion3.FindSaveState();

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
                unlock.unlockedClass.picture = SaveManagerVersion3.LoadSprite(unlock.unlockedClass.pathToPicture);
            }
            else
            {
                Debug.Log("Error with race or class");
            }
        }

        SaveManagerVersion3.SavePlayerInfo(playerInfo);

        //SUMMARY// Finds each save and makes sure they have correct items, abilities, and perks
        foreach (var kvp in CharacterRegistry.Instance.GetDictionary())
        {
            SaveObject save = kvp.Value;

            if (save == null) { continue; }

            if (save.raceObject == null)
            {
                save.raceObject = raceDatabase.GetRaceID[save.race];
            }
            if (save.classObject == null)
            {
                save.classObject = classDatabase.GetClassID[save.characterClass];
            }

            foreach (AbilitySaveObject abilitySO in save.abilityInventory)
            {
                if (abilitySO.ability == null)
                {
                    abilitySO.ability = abilityDatabase.GetStringID[abilitySO.stringID];
                    // Debug.Log(abilitySO.ability.abilityName + " name");
                    // Debug.Log("chosen : " + abilityDatabase.GetStringID[abilitySO.stringID].abilityName);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public static class SaveManagerVersion3
{
    public static readonly string SAVE_FOLDER = Path.Combine(Application.persistentDataPath, "Saves");


    public static void Init()
    {  
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void SaveGame(CharacterRegistry registry)
    {
        Dictionary<string, SaveObject> characterDictionary = new Dictionary<string, SaveObject>(registry.GetDictionary());

        string jsonData = JsonConvert.SerializeObject(characterDictionary);
        string savePath = Path.Combine(SAVE_FOLDER, "Registry.txt");
        File.WriteAllText(savePath, jsonData);
    }

    public static void LoadGame(CharacterRegistry registry)
    {
        string savePath = Path.Combine(SAVE_FOLDER, "Registry.txt");
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            Dictionary<string, SaveObject> characterDictionary = JsonConvert.DeserializeObject<Dictionary<string, SaveObject>>(jsonData);

            registry.ClearCharacters();

            foreach (KeyValuePair<string, SaveObject> kvp in characterDictionary)
            {
                registry.AddCharacter(kvp.Value);
            }
        }
    }

    public static void SaveStateOfGame(SaveState saveState)
    {
        string stateOfGame = JsonConvert.SerializeObject(saveState);

        string path = Path.Combine(SAVE_FOLDER, "character_manager.txt");

        if (File.Exists(path))
        {
            File.WriteAllText(path, stateOfGame);
        }
    }

    public static SaveState FindSaveState()
    {
        string path = Path.Combine(SAVE_FOLDER, "character_manager.txt");
        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<SaveState>(saveString);
        }
        else
        {
            SaveState saveState = new SaveState
            {

            };

            string json = JsonConvert.SerializeObject(saveState);
            
            File.WriteAllText(path, json);

            return saveState;
        }
        
    }

    public static PlayerInfo FindPlayerInfoFile()
    {
        string path = Path.Combine(SAVE_FOLDER, "PlayerInfo.txt");

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<PlayerInfo>(saveString);
        }
        else
        {
            Debug.Log("New Player Info");
            //create File
            PlayerInfo playerInfo = new PlayerInfo
            {
                
            };

            string json = JsonConvert.SerializeObject(playerInfo);
            
            File.WriteAllText(path, json);

            return playerInfo;
        }
    }

    public static void SavePlayerInfo(PlayerInfo _playerInfo)
    {
        string path = Path.Combine(SAVE_FOLDER, "PlayerInfo.txt");
        if (File.Exists(path))
        {
            string json = JsonConvert.SerializeObject(_playerInfo);

            File.WriteAllText(path, json);
        }
        else
        {
            Debug.LogError("Could not find Player Info");
        }

    }

    public static SaveObject FindCurrentSave()
    {
        SaveState state = FindSaveState();

        return CharacterRegistry.Instance.GetCharacter(state.fileIndexString);

    }

    // public static Sprite LoadSprite(string path)
    // {
    //     if (path == String.Empty) { return null; }
    //     byte[] imageData = File.ReadAllBytes(path);
    //     Texture2D tex = new Texture2D(2, 2);
    //     bool success = tex.LoadImage(imageData);

    //     tex.filterMode = FilterMode.Point;

    //     return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    // }

    public static bool DoesPlayerHaveThisAbility(Ability ability)
    {
        if (ability == null)
        {
            Debug.LogWarning("cant pass in a null ability: " + ability);
            return false;
        }
        SaveObject save = FindCurrentSave();
        foreach (AbilitySaveObject _ability in save.abilityInventory)
        {
            if (_ability.ability == null)
            {
                _ability.ability = LoadGameMasterHandler.Instance.GetAbility(_ability.stringID);
            }
            if (_ability.ability.abilityName == ability.abilityName)
            {
                return true;
            }
        }

        return false;

    }

    public static bool DoesPlayerHaveThisPerk(Perk perk)
    {
        SaveObject save = FindCurrentSave();
        foreach (PerkObject _perk in save.perks)
        {
            if (_perk.perk.perkName == perk.perkName)
            {
                return true;
            }
        }

        return false;

    }
}
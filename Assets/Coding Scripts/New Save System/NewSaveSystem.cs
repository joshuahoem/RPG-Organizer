using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class NewSaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";


    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
            Debug.Log("Josh created a new directory");
        }
    }

    public static void SaveCharacter(string saveString, int characterFileNumber)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");

        foreach (FileInfo fileInfo in saveFiles)
        {
            if (fileInfo.FullName == SAVE_FOLDER + "/save_" + characterFileNumber + ".txt")
            {
                Debug.Log("Match found!");
                if (File.Exists(SAVE_FOLDER + "/save_" + characterFileNumber + ".txt"))
                {
                    File.WriteAllText(SAVE_FOLDER + "/save_" + characterFileNumber + ".txt", saveString);
                    return;
                }
            }
        }

        int saveNumber = 1;
        while (File.Exists(SAVE_FOLDER + "/save_" + saveNumber + ".txt"))
        {
            saveNumber++;
        }
        
        //new one created
        File.WriteAllText(SAVE_FOLDER + "/save_" + saveNumber + ".txt", saveString);
    }

    public static void SaveStateOfGame(SaveState saveState)
    {
        string stateOfGame = JsonUtility.ToJson(saveState);

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            File.WriteAllText(SAVE_FOLDER + "/character_manager.txt", stateOfGame);
        }
    }

    public static string Load(int characterFileNumber)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");

        foreach (FileInfo fileInfo in saveFiles)
        {
            if (fileInfo.FullName == SAVE_FOLDER + "/save_" + characterFileNumber + ".txt")
            {
                if (File.Exists(SAVE_FOLDER + "/save_" + characterFileNumber + ".txt"))
                {
                    string saveString = File.ReadAllText(SAVE_FOLDER + "/save_" + characterFileNumber + ".txt");
                    return saveString;
                }
            }
        }

        return null;
        
    }

    public static int NumberOfCharacters()
    {
        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            return saveState.numberOfCharacters;
        }
        else
        {
            Debug.LogError("could not find folder!");
            return 0;
        }
    }

    public static SaveState FindSaveState()
    {
        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            return JsonUtility.FromJson<SaveState>(saveString);
        }

        return null;
    }

    public static SaveObject FindCurrentSave()
    {
        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            string charString = saveState.fileIndexString;

            if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt"))
            {
                string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

                return JsonUtility.FromJson<SaveObject>(newSaveString);

            }
            else
            {
                Debug.Log("Could not find character folder!");
                return null;
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
            return null;
        }
    }

    public static void SaveChanges(SaveObject save)
    {
        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");
            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);
            string charString = saveState.fileIndexString;

            string newCharacterString = JsonUtility.ToJson(save);
            File.WriteAllText(SAVE_FOLDER + "/save_" + charString + ".txt", newCharacterString);
        }
    }

    public static PlayerInfo FindPlayerInfoFile()
    {
        if (File.Exists(SAVE_FOLDER + "/PlayerInfo.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/PlayerInfo.txt");

            PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(saveString);

            return playerInfo;
        }
        else
        {
            //create File
            Debug.Log("new Player Info");
            Init();
            PlayerInfo playerInfo = new PlayerInfo
            {
                
            };

            string json = JsonUtility.ToJson(playerInfo);
            
            File.WriteAllText(SAVE_FOLDER + "/PlayerInfo.txt", json);

            return playerInfo;
        }
    }

    public static void SavePlayerInfo(PlayerInfo _playerInfo)
    {
        if (File.Exists(SAVE_FOLDER + "/PlayerInfo.txt"))
        {
            string json = JsonUtility.ToJson(_playerInfo);

            File.WriteAllText(SAVE_FOLDER + "/PlayerInfo.txt", json);
        }
        else
        {
            Debug.LogError("Could not find Player Info");
        }

    }
}

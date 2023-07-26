using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterManager : MonoBehaviour
{
    public static string SAVE_FOLDER;
    int numberOfCharacters;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject scrollManager;

    private void Start() 
    {
        NewSaveSystem.Init();

        SAVE_FOLDER = Application.persistentDataPath + "/Saves/";     

        if (!File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            //create File
            // Debug.Log("new manager");
            SaveState savestate = new SaveState
            {
                numberOfCharacters = 0
            };

            string json = JsonUtility.ToJson(savestate);

            File.WriteAllText(SAVE_FOLDER + "/character_manager.txt", json);
        }
        else if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            //already exists: load from file
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            numberOfCharacters = saveState.numberOfCharacters;

            if (numberOfCharacters > 0)
            {
                //load characters 

                DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
                FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
                int filesLength = saveFiles.Length;

                foreach (FileInfo fileInfo in saveFiles)
                {
                    if (File.Exists(SAVE_FOLDER + "/save_" + filesLength + ".txt"))
                    {
                        GameObject character = Instantiate(characterPrefab, 
                            transform.position, Quaternion.identity);
                        
                        character.transform.SetParent(scrollManager.transform, false);
                        character.GetComponent<CharacterUpdate>().LoadCharacter(filesLength);
                        
                    }

                    filesLength--;
                }   
                
            }
        }
        else
        {
            Debug.LogError("Something is wrong");
        }
    }
}

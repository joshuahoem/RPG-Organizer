using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class CharacterUpdate : MonoBehaviour
{
    public static string SAVE_FOLDER;

    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI characterLevel;
    [SerializeField] GameObject levelAlert;
    [SerializeField] TextMeshProUGUI levelPoints;

    [SerializeField] Image characterPicture;
    [SerializeField] RaceDatabase raceDatabase;
    [SerializeField] ClassDatabase classDatabase;


    private void Awake() 
    {
        SAVE_FOLDER = Application.persistentDataPath + "/Saves/";     
    }

    public void LoadCharacter(int characterFileNumber)
    {
        if (File.Exists(SAVE_FOLDER + "/save_" + characterFileNumber + ".txt"))
        {
            //Debug.Log("Foud file: " + characterFileNumber);
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save_" + characterFileNumber + ".txt");

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            characterName.text = saveObject.nameOfCharacter;
            characterLevel.text = saveObject.level.ToString();
            
            if (saveObject.raceObject != null)
            {
                characterPicture.sprite = saveObject.raceObject.picture;
            }
            else
            {
                foreach (Race _race in raceDatabase.allRaces)
                {
                    if (saveObject.race == _race.name)
                    {
                        characterPicture.sprite = _race.picture;
                        saveObject.raceObject = _race;
                    }
                }

                foreach (Class _class in classDatabase.allClasses)
                {
                    if (saveObject.characterClass == _class.name)
                    {
                        saveObject.classObject = _class;
                        break;
                    }
                }

                NewSaveSystem.SaveChanges(saveObject);
            }

            if (saveObject.hasLevelUp)
            {
                if (levelAlert != null)
                {
                    levelAlert.SetActive(true);
                    levelPoints.text = saveObject.levelPoints.ToString();
                }
            }
            else
            {
                if (levelAlert != null)
                {
                    levelAlert.SetActive(false);
                }
            }

            gameObject.name = characterFileNumber.ToString();
        }
        else
        {
            Debug.Log("file not found");
            int newNumberOfCharacters = NewSaveSystem.NumberOfCharacters() - 1;

            SaveState savestate = new SaveState
            {
                numberOfCharacters = newNumberOfCharacters
            };

            string json = JsonUtility.ToJson(savestate);

            if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
            {
                File.WriteAllText(SAVE_FOLDER + "/character_manager.txt", json);
            }
            else
            {
                Debug.LogError("Issue Saving");
            }

            Invoke("DestroyObject", 0.01f);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class CharacterUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI characterLevel;
    [SerializeField] GameObject levelAlert;
    [SerializeField] TextMeshProUGUI levelPoints;

    [SerializeField] Image characterPicture;
    [SerializeField] RaceDatabase raceDatabase;
    [SerializeField] ClassDatabase classDatabase;

    public void LoadCharacter(SaveObject saveObject)
    {
            characterName.text = saveObject.nameOfCharacter;
            characterLevel.text = saveObject.level.ToString();
            
            if (saveObject.raceObject != null)
            {
                characterPicture.sprite = SaveManagerVersion3.LoadSprite(saveObject.raceObject.pathToPicture);
            }
            else
            {
                foreach (Race _race in raceDatabase.allRaces)
                {
                    if (saveObject.race == _race.name)
                    {
                        characterPicture.sprite = SaveManagerVersion3.LoadSprite(_race.pathToPicture);
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

            gameObject.name = saveObject.characterID;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

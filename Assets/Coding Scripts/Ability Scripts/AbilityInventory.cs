using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class AbilityInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI abilityPointsTextNumber;
    [SerializeField] TextMeshProUGUI abilityPointsText;
    [SerializeField] string classAbilityText;
    [SerializeField] string raceAbilityText;

    SaveObject save;
    string charString;
    private List<AbilitySaveObject> abilities;
    AbilitySaveObject objectToRemove;
    AbilitySaveObject _abilityToAdd;


    private void Start() 
    {
        save = FindCurrentSave();

        abilities = save.abilityInventory;


        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        panelManager.onAbilityUnlocked += Subscriber_UnlockAbility;

        SaveState saveState = NewSaveSystem.FindSaveState();
        saveState.screenState = ScreenState.AbilityScreen;
        NewSaveSystem.SaveStateOfGame(saveState);

        if(saveState.classAbilityBool)
        {
            abilityPointsTextNumber.text = save.classAbilityPoints.ToString(); 
            abilityPointsText.text = classAbilityText;
        }
        else if (saveState.raceAbilityBool)
        {
            abilityPointsTextNumber.text = save.raceAbilityPoints.ToString(); 
            abilityPointsText.text = raceAbilityText;
        }
        else
        {
            Debug.LogError("no state for class or race");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("cleared abilities");
            save.abilityInventory.Clear();
            save.perks.Clear();
            SaveChanges();
        }
    }
    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {   
        AbilitySaveObject abilitySaveObject = ReturnNewAbilityObject(e._ability.ability);
        SaveState state = NewSaveSystem.FindSaveState();

        if (abilitySaveObject != null)
        {
            if (state.classAbilityBool)
            { 
                save.classAbilityPoints -= e._ability.ability.unlockCost; 
                abilityPointsTextNumber.text = save.classAbilityPoints.ToString(); 
            }
            else if (state.raceAbilityBool)
            { 
                save.raceAbilityPoints -= e._ability.ability.unlockCost; 
                abilityPointsTextNumber.text = save.raceAbilityPoints.ToString(); 
            }

            abilitySaveObject.unlocked = true;
            save.abilityInventory.Add(abilitySaveObject);
        }
        else
        {
            Debug.Log("has it");

            AbilitySaveObject _abilitySaveObject = new AbilitySaveObject(e._ability.ability, AbilityType.classAblity, 1, 0, true);
            foreach (AbilitySaveObject item in save.abilityInventory)
            {
                if (item.ability == _abilitySaveObject.ability)
                {
                    abilitySaveObject = item;
                }
            }

            // Debug.Log(abilitySaveObject.currentLevel);
            // Debug.Log(abilitySaveObject.ability.allAbilityLevels.Length);
            int checkNumber = (abilitySaveObject.currentLevel + 1);
            if (abilitySaveObject.ability.allAbilityLevels.Length >= checkNumber)
            {
                Debug.Log("Level Increased");
                abilitySaveObject.currentLevel++;

                foreach (AbilitySaveObject abilitySO in save.abilityInventory)
                {
                    if (abilitySO.ability == abilitySaveObject.ability)
                    {
                        objectToRemove = abilitySO;
                    }
                }
                save.abilityInventory.Remove(objectToRemove);
                save.abilityInventory.Add(abilitySaveObject);

            }
        }

        SaveChanges();

        FindObjectOfType<AbilityTabManager>().UpdateTabs(e._ability.ability); //change to an event to update instead

        foreach (AbilityInstanceObject instance in FindObjectsOfType<AbilityInstanceObject>())
        {
            if (e._ability.ability == instance.abilitySO.ability)
            {
                instance.abilitySO = abilitySaveObject;
                FindObjectOfType<AbilityPanelManager>().abilitySO = abilitySaveObject;
            }
        }
    }

    public AbilitySaveObject ReturnNewAbilityObject(Ability _ability)
    {
        save = FindCurrentSave(); 
        SaveState state = NewSaveSystem.FindSaveState();

        if (state.raceAbilityBool)
        {
            Debug.Log("race ability");
            _abilityToAdd = new AbilitySaveObject(_ability, AbilityType.raceAbility, 1, 0, true);
        }
        else if (state.classAbilityBool)
        {
            Debug.Log("class ability");
            _abilityToAdd = new AbilitySaveObject(_ability, AbilityType.classAblity, 1, 0, true);
        }
        else
        {
            Debug.Log("Error - no race or class bool set in save state");
            _abilityToAdd = null;
        }

        foreach (AbilitySaveObject item in save.abilityInventory)
        {
            if (item.ability == _abilityToAdd.ability)
            {
                return null;
            }
        }

        return _abilityToAdd;
    }

    private SaveObject FindCurrentSave()
    {
        string SAVE_FOLDER = Application.dataPath + "/Saves/";

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            charString = saveState.fileIndexString;

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

    private void SaveChanges()
    {
        string newCharacterString = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/Saves/" + 
            "/save_" + charString + ".txt", newCharacterString);
    }

}

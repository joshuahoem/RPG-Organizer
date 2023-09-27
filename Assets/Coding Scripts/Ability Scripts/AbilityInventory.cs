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
    [SerializeField] public Color lockedColor;
    [SerializeField] public Color unlockedColor;
    [SerializeField] public Color clickableColor;


    SaveObject save;
    string charString;
    private List<AbilitySaveObject> abilities;
    AbilitySaveObject objectToRemove;
    AbilitySaveObject _abilityToAdd;


    private void Start() 
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        abilities = save.abilityInventory;


        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        panelManager.onAbilityUnlocked += Subscriber_UnlockAbility;

        PerkPanelManager perkPanelManager = FindObjectOfType<PerkPanelManager>();
        perkPanelManager.onPerkUnlocked += Subscriber_UnlockPerk;

        SaveState saveState = SaveManagerVersion3.FindSaveState();
        saveState.screenState = ScreenState.AbilityScreen;
        SaveManagerVersion3.SaveStateOfGame(saveState);

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

    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {   
        AbilitySaveObject abilitySaveObject = ReturnNewAbilityObject(e._ability.ability);
        SaveState state = SaveManagerVersion3.FindSaveState();
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if (abilitySaveObject != null)
        {
            //just unlocked
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
            SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);      

        }
        else
        {
            //Has it

            AbilitySaveObject _abilitySaveObject = new AbilitySaveObject(e._ability.ability.abilityName, e._ability.ability, AbilityType.classAblity, 1, 0, true);
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

            if (state.classAbilityBool)
            { 
                if (e._ability.unlocked)
                {
                    //find new cost
                    save.classAbilityPoints -= e._ability.ability.allAbilityLevels[e._ability.viewingLevel].upgradeCost;
                    abilityPointsTextNumber.text = save.classAbilityPoints.ToString(); 
                }
                else
                {
                    //unlock for first time
                    save.classAbilityPoints -= e._ability.ability.unlockCost; 
                    abilityPointsTextNumber.text = save.classAbilityPoints.ToString(); 
                }
                
            }
            else if (state.raceAbilityBool)
            { 
                if (e._ability.unlocked)
                {
                    //find new cost
                    save.raceAbilityPoints -= e._ability.ability.allAbilityLevels[e._ability.currentLevel].upgradeCost;
                    abilityPointsTextNumber.text = save.raceAbilityPoints.ToString(); 
                }
                else
                {
                    //unlock first time
                    save.raceAbilityPoints -= e._ability.ability.unlockCost; 
                    abilityPointsTextNumber.text = save.raceAbilityPoints.ToString(); 
                }
                
            }  

            SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);      

        }

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
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        SaveState state = SaveManagerVersion3.FindSaveState();

        if (state.raceAbilityBool)
        {
            // Debug.Log("race ability");
            _abilityToAdd = new AbilitySaveObject(_ability.abilityName, _ability, AbilityType.raceAbility, 1, 0, true);
        }
        else if (state.classAbilityBool)
        {
            // Debug.Log("class ability");
            _abilityToAdd = new AbilitySaveObject(_ability.abilityName, _ability, AbilityType.classAblity, 1, 0, true);
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

    private void Subscriber_UnlockPerk(object sender, PerkPanelManager.UnlockPerkEventArgs e)
    {
        PerkObject perkObject = e.eventPerkObject;
        SaveState state = SaveManagerVersion3.FindSaveState();
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if (perkObject != null)
        {
            //just unlocked
            if (state.classAbilityBool)
            { 
                save.classAbilityPoints -= perkObject.perk.unlockCost;
                abilityPointsTextNumber.text = save.classAbilityPoints.ToString();    
            }
            else if (state.raceAbilityBool)
            { 
                save.raceAbilityPoints -= perkObject.perk.unlockCost;
                abilityPointsTextNumber.text = save.raceAbilityPoints.ToString(); 
            }

            // perkObject.unlocked = true; //no unlocked state for perkobject
            save.perks.Add(perkObject);

            SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
        }
    }

}

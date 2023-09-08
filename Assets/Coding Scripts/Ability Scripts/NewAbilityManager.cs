using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewAbilityManager : MonoBehaviour
{
    List<Transform> abilityListManager = new List<Transform>();
    [SerializeField] Color lockedColor;
    [SerializeField] Color unlockedColor;
    [SerializeField] public Color clickableColor;
    [SerializeField] Color arrowUnlockColor;
    [SerializeField] Color arrowLockedColor;


    void Start()
    {
        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        panelManager.onAbilityUnlocked += Subscriber_UnlockAbility;  

        PerkPanelManager perkPanelManager = FindObjectOfType<PerkPanelManager>();
        perkPanelManager.onPerkUnlocked += Subscriber_UnlockPerk;   

        SetUpTree();
    }

    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {
        SetUpTree();
    }

    private void Subscriber_UnlockPerk(object sender, PerkPanelManager.UnlockPerkEventArgs e)
    {
        SetUpTree();
    }

    private void SetUpTree()
    {
        SetUpList();      
        SaveObject save = SaveManagerVersion3.FindCurrentSave();  

        //set all to locked color border
        foreach (Transform obj in abilityListManager)
        {
            AbilityInstanceObject instance = obj.GetComponent<AbilityInstanceObject>();
            if (instance == null) { continue; }
            instance.borderImage.color = lockedColor;
        }

        //set starting ability border
        SaveState saveState = SaveManagerVersion3.FindSaveState();
        if (saveState.raceAbilityBool)
        {
            FindObjectOfType<AbilityTreeManager>().raceAbilityDictionary[save.race].
                GetComponent<RectTransformHandler>().startingFocusObject.gameObject.
                GetComponent<AbilityInstanceObject>().borderImage.color = clickableColor;
        }
        else
        {
            FindObjectOfType<AbilityTreeManager>().classAbilityDictionary[save.characterClass].
                GetComponent<RectTransformHandler>().startingFocusObject.gameObject.
                GetComponent<AbilityInstanceObject>().borderImage.color = clickableColor;
        }


        foreach (Transform obj in abilityListManager)
        {
            AbilityInstanceObject instance = obj.GetComponent<AbilityInstanceObject>();
            if (instance == null) { continue; }
            if (instance.ability != null)
            {
                if (SaveManagerVersion3.DoesPlayerHaveThisAbility(instance.ability))
                {
                    for (int i = 0; i < instance.abilitiesThatUnlock.Length; i++)
                    {
                        instance.abilitiesThatUnlock[i].GetComponent<AbilityInstanceObject>().borderImage.color = clickableColor;
                    }
                    foreach (GameObject arrow in instance.arrows)
                    {
                        arrow.SetActive(true);
                    }
                }
                else
                {
                    foreach (GameObject arrow in instance.arrows)
                    {
                        arrow.SetActive(false);
                    }
                }
            }

            if (instance.perk != null)
            {
                if (SaveManagerVersion3.DoesPlayerHaveThisPerk(instance.perk))
                {
                    for (int i = 0; i < instance.abilitiesThatUnlock.Length; i++)
                    {
                        instance.abilitiesThatUnlock[i].GetComponent<AbilityInstanceObject>().borderImage.color = clickableColor;
                    }
                    foreach (GameObject arrow in instance.arrows)
                    {
                        arrow.SetActive(true);
                    }
                }
                else
                {
                    foreach (GameObject arrow in instance.arrows)
                    {
                        arrow.SetActive(false);
                    }
                }
            }
        }

        //set border color for unlocked for abilities
        foreach (AbilitySaveObject abilitySO in save.abilityInventory)
        {
            foreach (Transform obj in abilityListManager)
            {
                AbilityInstanceObject instance = obj.GetComponent<AbilityInstanceObject>();
                if (instance == null) { continue; }
                if (instance.ability == null) { continue; }
                if (instance.ability == abilitySO.ability)
                {
                    //unlocked
                    instance.borderImage.color = unlockedColor;
                    continue;
                }
            }
        }

        //set border color for unlocked for perks
        foreach(PerkObject perkObject in save.perks)
        {
            foreach (Transform obj in abilityListManager)
            {
                AbilityInstanceObject instance = obj.GetComponent<AbilityInstanceObject>();
                if (instance == null) { continue; }
                if (instance.perk == null) { continue; }
                if (instance.perk == perkObject.perk)
                {
                    //unlock
                    instance.borderImage.color = unlockedColor;
                    continue;
                }
            }
        }
    }

    private void SetUpList()
    {
        abilityListManager.Clear();
        SaveState saveState = SaveManagerVersion3.FindSaveState();
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (saveState.raceAbilityBool)
        {
            foreach (Transform child in FindObjectOfType<AbilityTreeManager>().raceAbilityDictionary[save.race].transform)
            {
                abilityListManager.Add(child);
                // Debug.Log("Added: " + child.name + " to race");
            }
            
        }
        else if (saveState.classAbilityBool)
        {
            foreach (Transform child in FindObjectOfType<AbilityTreeManager>().classAbilityDictionary[save.characterClass].transform)
            {
                abilityListManager.Add(child);
                // Debug.Log("Added: " + child.name + " to class");
            }
        }
    }
}

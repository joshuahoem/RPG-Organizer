using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AbilityInstanceObject : MonoBehaviour
{
    [Header("Ability Info")]
    [SerializeField] Ability ability;

    [Header("Set Up Info")]
    [SerializeField] Image borderImage;
    [SerializeField] Image abilityImage;
    
    [Header("To View Only")]
    public AbilitySaveObject abilitySO;
    
    [Space(20)] [Header("Unlocked Info")]
    [SerializeField] GameObject[] abilitiesThatUnlock;
    List<GameObject> arrows = new List<GameObject>();
    [SerializeField] Transform parentTransformForArrows;


    private void Start() 
    {
        if (ability.abilitySpriteIcon != null)
        {
            abilityImage.sprite = ability.abilitySpriteIcon;
        }
        if (abilityImage != null)
        {
            borderImage.color = ability.borderColor;
        }

        abilitySO = GetAbilitySaveObject();

        SetupAbilityTree();

    }

    private void SetupAbilityTree()
    {
        AbilityPanelManager panelManager = FindObjectOfType<AbilityPanelManager>();
        panelManager.onAbilityUnlocked += Subscriber_UnlockAbility;

        foreach (GameObject ability in abilitiesThatUnlock)
        {
            ability.GetComponent<ArrowDirectionTest>().startingObject = this.gameObject;
            ability.GetComponent<ArrowDirectionTest>().endingObject = ability;
            ability.GetComponent<ArrowDirectionTest>().UpdateArrow(ability.name);
            arrows.Add(ability.GetComponent<ArrowDirectionTest>().arrowInstance);
        }

        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }

        foreach (GameObject ability in abilitiesThatUnlock)
        {
            ability.GetComponent<Button>().interactable = false;
        }

        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (AbilitySaveObject abilitySave in save.abilityInventory)
        {
            if (abilitySave.ability == this.ability)
            {
                if (abilitySave.unlocked)
                {
                    // Debug.Log("unlocking " + this.name);
                    foreach (GameObject ability in abilitiesThatUnlock)
                    {
                        ability.GetComponent<Button>().interactable = true;
                    }
                    foreach (GameObject arrow in arrows)
                    {
                        arrow.SetActive(true);
                    }
                }
            }
        }


        
    }

    private AbilitySaveObject GetAbilitySaveObject()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (AbilitySaveObject _abilitySO in save.abilityInventory)
        {
            if (ability == _abilitySO.ability)
            {
                return _abilitySO;
            }
        }

        SaveState state = NewSaveSystem.FindSaveState();

        if (state.raceAbilityBool)
        {
            return new AbilitySaveObject(ability.abilityName, ability, AbilityType.raceAbility, 0, 0, false);
        }
        else if (state.classAbilityBool)
        {
            return new AbilitySaveObject(ability.abilityName, ability, AbilityType.classAblity, 0, 0, false);
        }
        else
        {
            Debug.Log("Error - no race or class bool set in save state");
            return null;
        }

    }

    public void DisplayAbilityPanel()
    {
        //When Clicked on!
        AbilityPanelManager manager = FindObjectOfType<AbilityPanelManager>();
        manager.abilityInfoPanel.SetActive(true);
        manager.DisplayAbility(abilitySO);
        FindObjectOfType<EventHandler>().OnAbilityClickedFunction(abilitySO);
        // onAbilityClicked?.Invoke(this, new AbilityPanelManager.UnlockAbilityEventArgs { _ability = ability });
    }


    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {
        if (e._ability.ability == this.ability)
        {
            foreach (GameObject ability in abilitiesThatUnlock)
            {
                ability.GetComponent<Button>().interactable = true;
            }
            foreach (GameObject arrow in arrows)
            {
                arrow.SetActive(true);
            }
        }
        
    }
}

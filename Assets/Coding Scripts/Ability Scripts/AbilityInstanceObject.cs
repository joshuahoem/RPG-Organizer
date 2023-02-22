using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AbilityInstanceObject : MonoBehaviour
{
    [Header("Ability Info")]
    [SerializeField] Ability abilty;

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
        if (abilty.abilitySpriteIcon != null)
        {
            abilityImage.sprite = abilty.abilitySpriteIcon;
        }
        if (abilityImage != null)
        {
            borderImage.color = abilty.borderColor;
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
            if (abilitySave.ability == this.abilty)
            {
                if (abilitySave.unlocked)
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


        
    }

    private AbilitySaveObject GetAbilitySaveObject()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (AbilitySaveObject _abilitySO in save.abilityInventory)
        {
            if (abilty == _abilitySO.ability)
            {
                return _abilitySO;
            }
        }

        return new AbilitySaveObject(abilty, AbilityType.classAblity, 0, 0, false);
    }

    public void DisplayAbilityPanel()
    {
        //When Clicked on!
        AbilityPanelManager manager = FindObjectOfType<AbilityPanelManager>();
        manager.abilityInfoPanel.SetActive(true);
        manager.DisplayAbility(abilitySO);
        FindObjectOfType<EventHandler>().OnAbilityClickedFunction(abilitySO);
        // onAbilityClicked?.Invoke(this, new AbilityPanelManager.UnlockAbilityEventArgs { _ability = abilty });
    }


    private void Subscriber_UnlockAbility(object sender, AbilityPanelManager.UnlockAbilityEventArgs e)
    {
        if (e._ability.ability == this.abilty)
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

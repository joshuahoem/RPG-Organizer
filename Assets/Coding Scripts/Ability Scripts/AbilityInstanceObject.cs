using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AbilityInstanceObject : MonoBehaviour
{
    [Header("Ability Info")]
    [SerializeField] public Ability ability;
    [SerializeField] public Perk perk;

    [Header("Set Up Info")]
    [SerializeField] public Image borderImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] Image abilityImage;
    [SerializeField] int objectID;
    
    [Header("To View Only")]
    public AbilitySaveObject abilitySO;
    public PerkObject perkObject;
    
    [Space(20)] [Header("Unlocked Info")]
    [SerializeField] public GameObject[] abilitiesThatUnlock;
    public List<GameObject> arrows = new List<GameObject>();
    Transform parentTransformForArrows;


    private void Awake() 
    {
        if (ability != null)
        {
            if (ability.abilitySpriteIcon != null)
            {
                abilityImage.sprite = ability.abilitySpriteIcon;
            }
            
            backgroundImage.color = ability.borderColor;

            abilitySO = GetAbilitySaveObject();

        }

        if (perk != null)
        {
            if (perk.perkImageIcon != null)
            {
                abilityImage.sprite = perk.perkImageIcon;
            }
    
            backgroundImage.color = perk.borderColor;

            perkObject = FindPerkObject();

        }

        foreach (GameObject ability in abilitiesThatUnlock)
        {
            ability.GetComponent<ArrowDirectionTest>().startingObject = this.gameObject;
            ability.GetComponent<ArrowDirectionTest>().endingObject = ability;
            ability.GetComponent<ArrowDirectionTest>().UpdateArrow(ability.name);
            arrows.Add(ability.GetComponent<ArrowDirectionTest>().arrowInstance);
        }

        parentTransformForArrows = this.transform.parent.transform;

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

    private PerkObject FindPerkObject()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (PerkObject _perkObject in save.perks)
        {
            if (_perkObject.ID == this.objectID)
            {
                if (_perkObject.perk.perkName == this.perk.perkName)
                {
                    return _perkObject;
                }
            }
        }

        return new PerkObject(perk.perkName, perk, 0, false, objectID);
    }

    public void DisplayAbilityPanel()
    {
        //When Clicked on!
        if (perk != null)
        {
            FindObjectOfType<PerkPanelManager>().perkPanelObject.SetActive(true);
            FindObjectOfType<PerkPanelManager>().DisplayPerkPanel(perkObject, CanUnlockBool());
        }
        else if (ability != null)
        {
            FindObjectOfType<AbilityPanelManager>().abilityInfoPanel.SetActive(true);
            FindObjectOfType<AbilityPanelManager>().DisplayAbility(abilitySO, CanUnlockBool());
        }
        else
        {
            Debug.Log("Error - No perk or ability assigned");
        }
    }

    private bool CanUnlockBool()
    {
        NewAbilityManager newAbilityManager = FindObjectOfType<NewAbilityManager>();
        if (borderImage.color == newAbilityManager.clickableColor)
        {
            return true;
        }

        return false;
    }

}

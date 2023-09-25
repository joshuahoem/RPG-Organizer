using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopupInstance : MonoBehaviour
{
    [SerializeField] Sprite magicSprite, staminaSprite, healthSprite;
    [SerializeField] TextMeshProUGUI numberTMP;
    [SerializeField] Image image;   
    [SerializeField] Color normalColor, invisibleColor;

    public void Init(AbilitySaveObject abilitySaveObject)
    {
        if (abilitySaveObject.ability.costType == CostType.Magic)
        {
            numberTMP.text = abilitySaveObject.ability.allAbilityLevels[abilitySaveObject.currentLevel-1].magicCost.ToString();
            image.sprite = magicSprite;
        }
        else if (abilitySaveObject.ability.costType == CostType.Stamina)
        {
            numberTMP.text = abilitySaveObject.ability.allAbilityLevels[abilitySaveObject.currentLevel-1].staminaCost.ToString();
            image.sprite = staminaSprite;
        }
        else if (abilitySaveObject.ability.costType == CostType.Both)
        {
            numberTMP.text = abilitySaveObject.ability.allAbilityLevels[abilitySaveObject.currentLevel-1].magicCost.ToString();
        }
        else
        {
            //none
        }
    }

    public void ConsumedItemInit(InventoryItem inventoryItem)
    {
        if (inventoryItem.item.magicToRecover > 0)
        {
            image.sprite = magicSprite;
            image.color = normalColor;
            numberTMP.text = $"+{inventoryItem.item.magicToRecover}";
        }
        else if (inventoryItem.item.healthToRecover > 0)
        {
            image.sprite = healthSprite;
            image.color = normalColor;
            numberTMP.text = $"+{inventoryItem.item.healthToRecover}";            
        }
        else if (inventoryItem.item.staminaToRecover > 0)
        {
            image.sprite = staminaSprite;
            image.color = normalColor;
            numberTMP.text = $"+{inventoryItem.item.staminaToRecover}";
        }
        else
        {
            //nothing to display
            image.color = invisibleColor;
            numberTMP.text = "";
        }
    }

}

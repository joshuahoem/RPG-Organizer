using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopupInstance : MonoBehaviour
{
    [SerializeField] Sprite magicSprite;
    [SerializeField] Sprite staminaSprite;
    [SerializeField] TextMeshProUGUI numberTMP;
    [SerializeField] Image image;   

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

}

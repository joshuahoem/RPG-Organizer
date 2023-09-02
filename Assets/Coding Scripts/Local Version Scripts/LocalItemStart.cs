using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalItemStart : MonoBehaviour
{
    Item item;
    InventoryItem itemInfo;
    #region Display 
    [Header("Item Info")]
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI statStringOne;
    [SerializeField] TextMeshProUGUI statStringTwo;

    [SerializeField] TextMeshProUGUI statNumberOne;
    [SerializeField] TextMeshProUGUI statNumberTwo;

    [SerializeField] TextMeshProUGUI goldCostNumber;

    [SerializeField] string attackString;
    [SerializeField] string rangeString;
    [SerializeField] string defenseString;
    [SerializeField] string magicDefenseString;
    [SerializeField] string stackSizeString;


    [SerializeField] Image imageSprite;
    [SerializeField] Image backgroundSprite;
    [SerializeField] Color WeaponColor;
    [SerializeField] Color ArmorColor;

    [SerializeField] Color ConsumableColor;
    [SerializeField] Color SpecialColor;
    #endregion
    
    [Header("Abilities")]
    [SerializeField] Ability dualWieldAbility;

    public void DisplayItemInfo(Item _item, InventoryItem _itemInfo)
    {
        item = _item;
        itemInfo = _itemInfo;
        imageSprite.sprite = SaveManagerVersion3.LoadSprite(item.pathToPicture);
        itemName.text = _item.itemName.ToString();
        if (goldCostNumber != null)
            goldCostNumber.text = _item.goldCost.ToString();

        switch (item.itemType)
        {
            case ItemType.Armor:
                statStringOne.text = defenseString;
                statStringTwo.text = magicDefenseString;
                statNumberOne.text = _item.defense.ToString();
                statNumberTwo.text = _item.magicDefense.ToString();
                backgroundSprite.color = ArmorColor;
                break;
            case ItemType.Weapon:
                statStringOne.text = attackString;
                statStringTwo.text = rangeString;
                backgroundSprite.color = WeaponColor;
                if (_itemInfo.equipmentSlotIndex == 6)
                {
                    //offhand
                    if (NewSaveSystem.DoesPlayerHaveThisAbility(dualWieldAbility))
                    {
                        statNumberOne.text = _item.mainDamage.ToString();
                        statNumberTwo.text = _item.mainAttackRange.ToString();
                    }
                    else
                    {
                        statNumberOne.text = _item.offDamage.ToString();
                        statNumberTwo.text = _item.offAttackRange.ToString();
                    }
                }
                else
                {
                    //main hand
                    statNumberOne.text = _item.mainDamage.ToString();
                    statNumberTwo.text = _item.mainAttackRange.ToString();
                }

                break;
            case ItemType.Consumable:
                statStringOne.text = stackSizeString;
                statStringTwo.text = string.Empty;
                statNumberOne.text = _itemInfo.amount.ToString() + "/" + item.numberInStack.ToString();
                statNumberTwo.text = string.Empty;
                backgroundSprite.color = ConsumableColor;
                break;
            case ItemType.Special:
                statStringOne.text = string.Empty;
                statStringTwo.text = string.Empty;
                statNumberOne.text = string.Empty;
                statNumberTwo.text = string.Empty;
                backgroundSprite.color = SpecialColor;
                break;
            case ItemType.Scroll:
                statStringOne.text = string.Empty;
                statStringTwo.text = string.Empty;
                statNumberOne.text = string.Empty;
                statNumberTwo.text = string.Empty;
                backgroundSprite.color = SpecialColor;
                break;

        }
        
    }

    public void DisplayAbilityInfo(AbilitySaveObject ability)
    {
        imageSprite.sprite = ability.ability.picture;
        itemName.text = ability.ability.abilityName;
        statStringOne.text = string.Empty;
        statStringTwo.text = string.Empty;
        statNumberOne.text = string.Empty;
        statNumberTwo.text = string.Empty;
        backgroundSprite.color = SpecialColor;
    }


    public void ClickedItem()
    {
        FindObjectOfType<TabManager>().itemInfoPanel.SetActive(true);
        FindObjectOfType<TabManager>().itemInfoPanel.
            GetComponent<ItemPanelDisplay>().DisplayItemInfo(item, itemInfo);
    }

}

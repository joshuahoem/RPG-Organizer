using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;

public class ItemPanelDisplay : MonoBehaviour
{
    #region //Stat Display
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI mainTextDisplay;
    [SerializeField] TextMeshProUGUI offTextDisplay;
    [SerializeField] TextMeshProUGUI goldCostDisplay;
    [SerializeField] TextMeshProUGUI goldSellDisplay;


    [SerializeField] TextMeshProUGUI statNumberOne;
    [SerializeField] TextMeshProUGUI statNumberTwo;
    [SerializeField] TextMeshProUGUI statNumberThree;
    [SerializeField] TextMeshProUGUI statNumberFour;

    [SerializeField] TextMeshProUGUI offStatNumberOne;
    [SerializeField] TextMeshProUGUI offStatNumberTwo;
    [SerializeField] TextMeshProUGUI offStatNumberThree;
    [SerializeField] TextMeshProUGUI offStatNumberFour;

    [SerializeField] TextMeshProUGUI statTextOne;
    [SerializeField] TextMeshProUGUI statTextTwo;
    [SerializeField] TextMeshProUGUI statTextThree;
    [SerializeField] TextMeshProUGUI statTextFour;

    [SerializeField] string damageString;
    [SerializeField] string rangeString;
    [SerializeField] string magicDamageString;
    [SerializeField] string oneHandedString;
    [SerializeField] string twoHandedString;
    [SerializeField] string handsString;
    [SerializeField] string stackSizeString;
    [SerializeField] string defenseString;
    [SerializeField] string magicDefenseString;
    [SerializeField] string mainTextDisplayString;
    [SerializeField] string offTextDisplayString;
    [SerializeField] string amountRemainingString;

    

    [SerializeField] GameObject[] buttonsToDeactivate;
    [SerializeField] GameObject equipArmorButton;
    [SerializeField] GameObject equipMainHandButton;
    [SerializeField] GameObject equipOffHandButton;
    [SerializeField] GameObject equipWeaponButton;
    [SerializeField] GameObject equipSpecial;
    [SerializeField] GameObject equipRingOne;
    [SerializeField] GameObject equipRingTwo;
    [SerializeField] GameObject equipArrowButton;



    [SerializeField] GameObject consumeItemButton;
    [SerializeField] GameObject unequipButton;
    [SerializeField] GameObject sellToShopButton;
    [SerializeField] GameObject PurchaseButton;
    [SerializeField] GameObject learnScrollAbilityButton;


    [SerializeField] Image itemImage;
    #endregion

    [SerializeField] Color commonColor;
    [SerializeField] Color uncommonColor;
    [SerializeField] Color rareColor;
    [SerializeField] Color epicColor;
    [SerializeField] Color legendaryColor;
    [SerializeField] GameObject borderObject;
    [SerializeField] TextMeshProUGUI qualityText;


    [SerializeField] TextMeshProUGUI descriptionText;
    // [SerializeField] Image itemImage;
    Item item;
    InventoryItem itemInfo;
    bool inShop;
    InventoryItem searchResult;
    [SerializeField] ItemDatabase database;
    [SerializeField] LocalInventoryManager inventoryManager;

    [SerializeField] private ErrorMessageHandler errorMessageHandler;
    int lootCheck;
    int searchIndex;

    [Header("Abilities")]
    [SerializeField] Perk weakPerk;
    [SerializeField] Ability strengthAbility;

    // private void Update() {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         // Debug.Log("1");
    //         SaveObject _save = SaveManagerVersion3.FindCurrentSave();
    //         foreach (InventoryItem _item in _save.inventory)
    //         {
    //             Debug.Log(_item.item.itemName + " inventory");
    //         }

    //         foreach (InventoryItem _item in _save.equipment)
    //         {
    //             if (_item.item != null)
    //             {
    //                 Debug.Log(_item.item.itemName + "equipment");
    //             }
    //         }
    //     }
    // }


    public void DeactivateAllButtons()
    {
        foreach (GameObject button in buttonsToDeactivate)
        {
            button.SetActive(false);
        }
    }
    public void DisplayItemInfo(Item _item, InventoryItem _itemInfo)
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        DeactivateAllButtons();
        inShop = FindObjectOfType<LocalShop>().inShop;
        ItemType type = _item.GetItemType();
        ItemRarity rarity = _item.GetRarity();

        item = _item;
        itemInfo = _itemInfo;
        itemName.text = _item.itemName;
        descriptionText.text = _item.description;
        if(itemImage != null)
        // itemImage.sprite = SaveManagerVersion3.LoadSprite(_item.pathToPicture);
        itemImage.sprite = _item.picture;

        //gold cost display
        if (!inShop)
        {
            //inventory version
            if (_itemInfo.amount < _item.numberInStack && _item.GetItemType() == ItemType.Special)
            {
                goldSellDisplay.text = "0";
            }
            else
            {
                goldSellDisplay.text = _item.sellCost.ToString();
            }

            sellToShopButton.SetActive(true);
            PurchaseButton.SetActive(false);
        }
        else if (inShop)
        {
            //shop version
            goldCostDisplay.text = _item.goldCost.ToString();
            sellToShopButton.SetActive(false);
            PurchaseButton.SetActive(true);
        }

        qualityText.text = rarity.ToString();
        switch (rarity)
        {
            case ItemRarity.PeasantCraft:
                //common
                borderObject.GetComponent<Image>().color = commonColor;
                qualityText.color = commonColor;
                break;
            case ItemRarity.ApprenticeForged:
                //uncommon
                borderObject.GetComponent<Image>().color = uncommonColor;
                qualityText.color = uncommonColor;
                break;
            case ItemRarity.ImperialForged:
                //rare
                borderObject.GetComponent<Image>().color = rareColor;
                qualityText.color = rareColor;
                break;
            case ItemRarity.ArtisanCraft:
                //epic
                borderObject.GetComponent<Image>().color = epicColor;
                qualityText.color = epicColor;
                break;
            case ItemRarity.MasterForged:
                //legendary
                borderObject.GetComponent<Image>().color = legendaryColor;
                qualityText.color = legendaryColor;
                break;
        }

        switch(type)
        {
            case ItemType.Weapon:
                DisplayWeaponInfo(_item, _itemInfo);
                break;
            case ItemType.Armor:
                DisplayArmorInfo(_item, _itemInfo);
                break;
            case ItemType.Consumable:
                DisplayConsumableInfo(_item, _itemInfo);
                break;
            case ItemType.Special:
                DisplaySpecialInfo(_item, _itemInfo);
                break;
            case ItemType.Scroll:
                DisplaySpecialInfo(_item, _itemInfo);
                break;
            case ItemType.Arrows:
                DisplayArrowInfo(_itemInfo);
                break;
        }
        
    }

    private void DisplayWeaponInfo(Item _item, InventoryItem _itemInfo)
    {
        mainTextDisplay.text = mainTextDisplayString;
        offTextDisplay.text = offTextDisplayString;

        statTextOne.text = damageString;
        statTextTwo.text = magicDamageString;
        statTextThree.text = rangeString;
        statTextFour.text = handsString;

        statNumberOne.text = _item.mainDamage.ToString();
        statNumberTwo.text = _item.mainMagicDamage.ToString();
        statNumberThree.text = _item.mainAttackRange.ToString();
        statNumberFour.text = _item.numberOfHands.ToString();

        offStatNumberOne.text = _item.offDamage.ToString();
        offStatNumberTwo.text = _item.offMagicDamage.ToString();
        offStatNumberThree.text = _item.offAttackRange.ToString();
        offStatNumberFour.text = string.Empty;

        if (FindObjectOfType<LocalShop>().inShop) {return;}
        if (_itemInfo.equipped)
        {
            unequipButton.SetActive(true);
        }
        else
        {
            if (this.equipWeaponButton == null) {return;}
            switch (item.numberOfHands)
            {
                case NumberOfHands.OneHanded:
                    equipMainHandButton.SetActive(true);
                    equipOffHandButton.SetActive(true);
                    break;
                case NumberOfHands.TwoHanded:
                    equipWeaponButton.SetActive(true);
                    break;
            }
        }
    }

    private void DisplayArmorInfo(Item _item, InventoryItem _itemInfo)
    {
        mainTextDisplay.text = mainTextDisplayString;
        offTextDisplay.text = string.Empty;

        statTextOne.text = defenseString;
        statTextTwo.text = magicDefenseString;
        statTextThree.text = string.Empty;
        statTextFour.text = string.Empty;

        statNumberOne.text = _item.defense.ToString();
        statNumberTwo.text = _item.magicDefense.ToString();
        statNumberThree.text = string.Empty;
        statNumberFour.text = string.Empty;

        offStatNumberOne.text = string.Empty;
        offStatNumberTwo.text = string.Empty;
        offStatNumberThree.text = string.Empty;
        offStatNumberFour.text = string.Empty;

        if (FindObjectOfType<LocalShop>().inShop) {return;}
        if (_itemInfo.equipped)
        {
            unequipButton.SetActive(true);
            // equipArmorButton.SetActive(false);
        }
        else
        {
            if (this.equipArmorButton == null) {return;}
            equipArmorButton.SetActive(true);
            // unequipButton.SetActive(false);

        }
    }

    private void DisplayConsumableInfo(Item _item, InventoryItem _itemInfo)
    {
        mainTextDisplay.text = string.Empty;
        offTextDisplay.text = string.Empty;

        statTextOne.text = stackSizeString;
        statTextTwo.text = string.Empty;
        statTextThree.text = string.Empty;
        statTextFour.text = string.Empty;

        statNumberOne.text = _itemInfo.amount.ToString() + "/" + _item.numberInStack.ToString();
        statNumberTwo.text = string.Empty;
        statNumberThree.text = string.Empty;
        statNumberFour.text = string.Empty;

        offStatNumberOne.text = string.Empty;
        offStatNumberTwo.text = string.Empty;
        offStatNumberThree.text = string.Empty;
        offStatNumberFour.text = string.Empty;

        if (FindObjectOfType<LocalShop>().inShop) {return;}
        if (this.consumeItemButton == null) {return;}
        consumeItemButton.SetActive(true);
    }

    private void DisplaySpecialInfo(Item _item, InventoryItem _itemInfo)
    {
        mainTextDisplay.text = string.Empty;
        offTextDisplay.text = string.Empty;
        
        statTextOne.text = string.Empty;
        statTextTwo.text = string.Empty;
        statTextThree.text = string.Empty;
        statTextFour.text = string.Empty;

        statNumberOne.text = string.Empty;
        statNumberTwo.text = string.Empty;
        statNumberThree.text = string.Empty;
        statNumberFour.text = string.Empty;

        offStatNumberOne.text = string.Empty;
        offStatNumberTwo.text = string.Empty;
        offStatNumberThree.text = string.Empty;
        offStatNumberFour.text = string.Empty;

        if (FindObjectOfType<LocalShop>().inShop) {return;}
        if (this.equipRingOne == null || this.equipRingTwo == null || this.equipSpecial == null) { return; }

        if (_itemInfo.item.itemType.ToString() == ItemType.Scroll.ToString())
        {
            learnScrollAbilityButton.SetActive(true);
        }
        else
        {
            if (_itemInfo.equipped)
            {
                unequipButton.SetActive(true);
            }
            else
            {
                if (item.equipmentSlot == EquipmentSlot.JewelryOne || item.equipmentSlot == EquipmentSlot.JewelryTwo)
                {
                    equipRingOne.SetActive(true);
                    equipRingTwo.SetActive(true);
                }
                else
                {
                    equipSpecial.SetActive(true);
                }
            }
        }
        
    }

    private void DisplayArrowInfo(InventoryItem _itemInfo)
    {
        mainTextDisplay.text = mainTextDisplayString;
        offTextDisplay.text = offTextDisplayString;

        statTextOne.text = damageString;
        statTextTwo.text = magicDamageString;
        statTextThree.text = rangeString;
        statTextFour.text = handsString;

        statNumberOne.text = _itemInfo.item.mainDamage.ToString();
        statNumberTwo.text = _itemInfo.item.mainMagicDamage.ToString();
        statNumberThree.text = _itemInfo.item.mainAttackRange.ToString();
        statNumberFour.text = _itemInfo.item.numberOfHands.ToString();

        offStatNumberOne.text = _itemInfo.item.offDamage.ToString();
        offStatNumberTwo.text = _itemInfo.item.offMagicDamage.ToString();
        offStatNumberThree.text = _itemInfo.item.offAttackRange.ToString();
        offStatNumberFour.text = string.Empty;

        if (FindObjectOfType<LocalShop>().inShop) {return;}
        if (_itemInfo.equipped)
        {
            unequipButton.SetActive(true);
        }
        else
        {
            if (this.equipArrowButton == null) {return;}
            equipArrowButton.SetActive(true);
        }
    }

    public void EquipArmor()
    {
        inventoryManager.Equip(itemInfo,0);
        inventoryManager.DisplayInventoryUI();
    }

    public void EquipMainHand()
    {
        //5 equips to main hand
        inventoryManager.Equip(itemInfo,5);
        inventoryManager.DisplayInventoryUI();

    }

    public void EquipOffHand()
    {
        //6 equips to off hand
        inventoryManager.Equip(itemInfo,6);
        inventoryManager.DisplayInventoryUI();

    }

    public void EquipWeapon()
    {
        //for two-handed weapons
        if (SaveManagerVersion3.DoesPlayerHaveThisPerk(weakPerk))
        {
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.TooHeavy);
        }
        else if (SaveManagerVersion3.DoesPlayerHaveThisAbility(strengthAbility))
        {
            SaveObject save = SaveManagerVersion3.FindCurrentSave();

            if (save.equipment[5] == null)
            {
                inventoryManager.Equip(itemInfo,5);
                inventoryManager.DisplayInventoryUI();
                return;
            }
            else if (save.equipment[6] == null)
            {
                inventoryManager.Equip(itemInfo,6);
                inventoryManager.DisplayInventoryUI();
                return;
            }
            else
            {
                inventoryManager.Equip(itemInfo,5);
                inventoryManager.DisplayInventoryUI();
                return;
            }

        }
        else
        {
            inventoryManager.Equip(itemInfo,5);
            inventoryManager.Equip(itemInfo,6);
            inventoryManager.DisplayInventoryUI();
        }
        

    }

    public void EquipSpecial()
    {
        Debug.Log("special");
        inventoryManager.Equip(itemInfo, 0);
        inventoryManager.DisplayInventoryUI();
    }

    public void EquipRingOne()
    {
        inventoryManager.Equip(itemInfo, 7);
        inventoryManager.DisplayInventoryUI();
    }
    public void EquipRingTwo()
    {
        inventoryManager.Equip(itemInfo, 8);
        inventoryManager.DisplayInventoryUI();
    }

    public void EquipQuiver()
    {
        inventoryManager.Equip(itemInfo, 9);
        inventoryManager.DisplayInventoryUI();
    }

    public void Unequip()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        inventoryManager.Unequip(itemInfo, save);
        inventoryManager.LoadEquipment();
        inventoryManager.LoadInventory();     
        inventoryManager.DisplayInventoryUI();

    }

    public void ConsumeItem()
    {
        //consume item
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        bool removedBool = false;

        foreach (InventoryItem inv in save.inventory)
        {
            if (inv.item == item && !removedBool)
            {
                inv.amount -= 1;
                // Debug.Log(inv.amount);
                removedBool = true;

                if (inv.amount <= 0)
                {
                    // Debug.Log("need to delete");
                    // inventoryManager.Consume(item, itemInfo);
                    searchResult = inv;
                }
            }
        }

        save.inventory.Remove(searchResult);


        inventoryManager.LoadInventory();
        inventoryManager.DisplayInventoryUI();

    }

    public void PurchaseItem()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        // Check if can hold ----- OLD SYSTEM
        // No longer check for loot in inventory, check only to see if they can equip
        // lootCheck = 0;
        // foreach (InventoryItem item in save.inventory)
        // {
        //     if (item == null) { continue; }
        //     if (item.item == null)
        //     {
        //         item.item = LoadGameMasterHandler.Instance.GetItem(item.stringID);
        //     }

        //     if (item.item.GetItemType() != ItemType.Special && item.item.GetItemType() != ItemType.Scroll)
        //     {
        //         lootCheck += 1;
        //     }
        // }

        // foreach (InventoryItem item in save.equipment)
        // {
        //     if (item == null) { continue; }
        //     if (item.item == null) { continue; }
        //     if (item.item.GetItemType() != ItemType.Special && item.item.GetItemType() != ItemType.Scroll)
        //     {
        //         lootCheck += 1;
        //     }
        // }


        // if (save.equipment[5] != null)
        // {
        //     if (save.equipment[5].item != null)
        //     {
        //         if (save.equipment[5].item.numberOfHands == NumberOfHands.TwoHanded)
        //         {
        //             // Debug.Log("removing 1");
        //             lootCheck -= 1;
        //         }
        //     }

        // }

        // if (lootCheck >= save.holdingCapacity && item.GetItemType() != ItemType.Special)
        // {
        //     // Debug.Log("not strong enough to carry"); //error
        //     errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoStrength);
        //     return;
        // }

        //need to check gold
        if(save.gold >= item.goldCost)
        {
            InventoryItem newItem = new InventoryItem
                (item, item.numberInStack, false, 0);
            save.inventory.Add(newItem);

            save.gold -= item.goldCost;
        }
        else
        {
            // Debug.Log("not enough gold"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoGold);
        }

        FindObjectOfType<LocalShop>().LoadShop();
    }

    public void SellItem()
    {
        SaveObject _save = SaveManagerVersion3.FindCurrentSave();
        searchResult = null;
        foreach (InventoryItem _item in _save.inventory)
        {
            if(_item.item.itemName == item.itemName)
            {
                // Debug.Log("found in inventory");
                searchResult = _item;
            }
        }

        if (searchResult == null)
        {
            foreach (InventoryItem _item in _save.equipment)
            {
                if (_item == null) { continue; }
                if (_item.item != null)
                {
                    if (_item.item.itemName == item.itemName)
                    {
                        // Debug.Log("found in equipment");
                        searchResult = _item;
                    }
                }
            }
        }
        
        //Actions
        if (searchResult.equipped)
        {
            foreach (InventoryItem inv in _save.equipment)
            {
                if (inv == null) { continue; }
                if (inv.item == searchResult.item)
                {
                    searchIndex = inv.equipmentSlotIndex;
                }
            }

            _save.equipment[searchIndex] = null;
            InventoryItem _newItem = new InventoryItem
                        (searchResult.item, searchResult.item.numberInStack, false, 0);
            _save.inventory.Add(_newItem);

            SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
        }

        _save = SaveManagerVersion3.FindCurrentSave();
        foreach (InventoryItem _item in _save.inventory)
        {
            if(_item.stringID == item.itemName)
            {
                searchResult = _item;
            }
        }

        // Debug.Log(searchResult.item + " item");

        if (searchResult.amount < searchResult.item.numberInStack)
        {
            //selling grapes with less than max amount
        }
        else
        {
            // Debug.Log("sold");
            _save.gold += item.sellCost;      
        }

        _save.inventory.Remove(searchResult);
        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);

        // Debug.Log(_save.inventory.Count + " inv count");

        inventoryManager.DisplayInventoryUI();
        inventoryManager.LoadEquipment();
        inventoryManager.LoadInventory();

    }

    public void LearnAbilityAction()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        //old way
        // if (save.spellbookCapacity <= save.abilityInventory.Count)
        // {
        //     // Debug.Log("not enough intelligence to get an ability");//error
        //     errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoIntelligence);
        //     return;
        // }

        //#TODO
        if (item.ability.unlockHealth > save.baseHealth)
        {
            // Debug.Log("not enough health"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.AbilityNoHealth);
            return;
        }
        if (item.ability.unlockStamina > save.baseStamina)
        {
            // Debug.Log("not enough Stamina"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.AbilityNoStamina);
            return;
        }
        if (item.ability.unlockMagic > save.baseMagic)
        {
            // Debug.Log("not enough Magic"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.AbilityNoMagic);
            return;
        }
        if (item.ability.unlockStrength > save.baseStrength)
        {
            // Debug.Log("not enough Strength"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.AbilityNoStrength);
            return;
        }
        if (item.ability.unlockIntelligence > save.baseIntelligence)
        {
            // Debug.Log("not enough Intelligence"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.AbilityNoIntelligence);
            return;
        }
        if (item.ability.unlockSpeed > save.baseSpeed)
        {
            // Debug.Log("not enough Speed"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.AbilityNoSpeed);
            return;
        }

        foreach (AbilitySaveObject abilitySO in save.abilityInventory)
        {
            if (abilitySO.ability == item.ability)
            {
                //already has it
                // Debug.Log("already has it");
                return;
            }
        }

        // Debug.Log("does not have it");
        AbilitySaveObject abilitySaveObject = new AbilitySaveObject(item.ability.abilityName, item.ability, AbilityType.learnedAbility, 0, 0, true);
        save.abilityInventory.Add(abilitySaveObject);
        ConsumeItem();


    }
}

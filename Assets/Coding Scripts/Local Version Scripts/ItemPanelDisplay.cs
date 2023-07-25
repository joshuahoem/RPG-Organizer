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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            SaveObject _save = NewSaveSystem.FindCurrentSave();
            foreach (InventoryItem _item in _save.inventory)
            {
                Debug.Log(_item.item.itemName + " inventory");
            }

            foreach (InventoryItem _item in _save.equipment)
            {
                if (_item.item != null)
                {
                    Debug.Log(_item.item.itemName + "equipment");
                }
            }
        }
    }


    public void DeactivateAllButtons()
    {
        foreach (GameObject button in buttonsToDeactivate)
        {
            button.SetActive(false);
        }
    }
    public void DisplayItemInfo(Item _item, InventoryItem _itemInfo)
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();
        DeactivateAllButtons();
        inShop = FindObjectOfType<LocalShop>().inShop;
        ItemType type = _item.GetItemType();
        ItemRarity rarity = _item.GetRarity();

        item = _item;
        itemInfo = _itemInfo;
        itemName.text = _item.itemName;
        descriptionText.text = _item.description;
        if(itemImage != null)
        itemImage.sprite = item.imageSprite;

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

    public void EquipArmor()
    {
        inventoryManager.Equip(item,0);
        inventoryManager.DisplayInventoryUI();
        // SaveChanges();
    }

    public void EquipMainHand()
    {
        //5 equips to main hand
        inventoryManager.Equip(item,5);
        inventoryManager.DisplayInventoryUI();
        // SaveChanges();

    }

    public void EquipOffHand()
    {
        //6 equips to off hand
        inventoryManager.Equip(item,6);
        inventoryManager.DisplayInventoryUI();
        // SaveChanges();

    }

    public void EquipWeapon()
    {
        //for two-handed weapons
        inventoryManager.Equip(item,5);
        inventoryManager.Equip(item,6);
        inventoryManager.DisplayInventoryUI();
        // SaveChanges();

    }

    public void EquipSpecial()
    {
        inventoryManager.Equip(item, 0);
        inventoryManager.DisplayInventoryUI();
    }

    public void EquipRingOne()
    {
        inventoryManager.Equip(item, 9);
        inventoryManager.DisplayInventoryUI();
    }
    public void EquipRingTwo()
    {
        inventoryManager.Equip(item, 10);
        inventoryManager.DisplayInventoryUI();
    }

    public void Unequip()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        inventoryManager.Unequip(itemInfo, save);
        inventoryManager.LoadEquipment();
        inventoryManager.LoadInventory();     
        inventoryManager.DisplayInventoryUI();

    }

    public void ConsumeItem()
    {
        //consume item
        SaveObject save = NewSaveSystem.FindCurrentSave();
        bool removedBool = false;

        foreach (InventoryItem inv in save.inventory)
        {
            if (inv.item == item && !removedBool)
            {
                inv.amount -= 1;
                Debug.Log(inv.amount);
                removedBool = true;

                if (inv.amount <= 0)
                {
                    Debug.Log("need to delete");
                    // inventoryManager.Consume(item, itemInfo);
                    searchResult = inv;
                }
            }
        }

        save.inventory.Remove(searchResult);

        NewSaveSystem.SaveChanges(save);        

        inventoryManager.LoadInventory();
        inventoryManager.DisplayInventoryUI();

    }

    public void PurchaseItem()
    {
        //need to check gold
        SaveObject save = NewSaveSystem.FindCurrentSave();

        //Check if can hold
        lootCheck = 0;
        foreach (InventoryItem item in save.inventory)
        {
            if (item.item.GetItemType() != ItemType.Special && item.item.GetItemType() != ItemType.Scroll)
            {
                lootCheck += 1;
            }
        }

        foreach (InventoryItem item in save.equipment)
        {
            if (item.item == null) { continue; }
            if (item.item.GetItemType() != ItemType.Special && item.item.GetItemType() != ItemType.Scroll)
            {
                lootCheck += 1;
            }
        }

        if (save.equipment[5].item != null)
        {
            if (save.equipment[5].item.numberOfHands == NumberOfHands.TwoHanded)
            {
                Debug.Log("removing 1");
                lootCheck -= 1;
            }

        }

        if (lootCheck >= save.holdingCapacity && item.GetItemType() != ItemType.Special)
        {
            Debug.Log("not strong enough to carry"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoStrength);
            return;
        }

        if(save.gold >= item.goldCost)
        {
            InventoryItem newItem = new InventoryItem
                (item, database.GetID[item], item.numberInStack, false, 0);
            save.inventory.Add(newItem);

            save.gold -= item.goldCost;
        }
        else
        {
            Debug.Log("not enough gold"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoGold);
        }

        NewSaveSystem.SaveChanges(save);
        FindObjectOfType<LocalShop>().LoadShop();
    }

    public void SellItem()
    {
        SaveObject _save = NewSaveSystem.FindCurrentSave();
        searchResult = null;
        foreach (InventoryItem _item in _save.inventory)
        {
            if(_item.item.itemName == item.itemName)
            {
                Debug.Log("found in inventory");
                searchResult = _item;
            }
        }

        if (searchResult == null)
        {
            foreach (InventoryItem _item in _save.equipment)
            {
                if (_item.item != null)
                {
                    if (_item.item.itemName == item.itemName)
                    {
                        Debug.Log("found in equipment");
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
                if (inv.item == searchResult.item)
                {
                    searchIndex = inv.equipmentSlotIndex;
                }
            }

            _save.equipment[searchIndex].equipmentSlotIndex = 0;
            InventoryItem _newItem = new InventoryItem
                        (searchResult.item, database.GetID[searchResult.item], searchResult.item.numberInStack, false, 0);
            _save.inventory.Add(_newItem);

            _save.equipment[searchIndex].equipped = false;
            _save.equipment[searchIndex].item = null;

            NewSaveSystem.SaveChanges(_save);

            // inventoryManager.LoadEquipment();
        }

        _save = NewSaveSystem.FindCurrentSave();
        foreach (InventoryItem _item in _save.inventory)
        {
            if(_item.ID == database.GetID[item])
            {
                searchResult = _item;
            }
        }

        Debug.Log(searchResult.item + " item");

        if (searchResult.amount < searchResult.item.numberInStack)
        {
            //selling grapes with less than max amount
        }
        else
        {
            Debug.Log("sold");
            _save.gold += item.sellCost;      
        }

        _save.inventory.Remove(searchResult);
        NewSaveSystem.SaveChanges(_save); 
        _save = NewSaveSystem.FindCurrentSave();        

        // Debug.Log(_save.inventory.Count + " inv count");

        inventoryManager.DisplayInventoryUI();
        inventoryManager.LoadEquipment();
        inventoryManager.LoadInventory();

    }

    public void LearnAbilityAction()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        //error 
        if (save.spellbookCapacity <= save.abilityInventory.Count)
        {
            Debug.Log("not enough intelligence to get an ability");//error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoIntelligence);
            return;
        }

        foreach (AbilitySaveObject abilitySO in save.abilityInventory)
        {
            if (abilitySO.ability == item.ability)
            {
                //already has it
                Debug.Log("already has it");
                return;
            }
        }

        Debug.Log("does not have it");
        AbilitySaveObject abilitySaveObject = new AbilitySaveObject(item.ability.abilityName, item.ability, AbilityType.learnedAbility, 0, 0, true);
        save.abilityInventory.Add(abilitySaveObject);
        ConsumeItem();

        NewSaveSystem.SaveChanges(save);        

    }
}

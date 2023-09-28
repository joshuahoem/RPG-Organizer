using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEditor;

public enum NumberOfHands
{
    NoHands,
    OneHanded,
    TwoHanded
}

public enum EquipmentSlot
{
    Head,
    Torso,
    Legs,
    Feet,
    Arms, //gloves
    MainHand, //weapon
    OffHand, //weapon
    JewelryOne,
    JewelryTwo,
    Quiver, //Arrows
    None
}

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Special,
    Scroll,
    Arrows
}

public enum WeaponType
{
    None,
    Bow,
    Arrow,
    Hammer,
    Axe,
    Sword,
    Dagger,
    Other
}

public enum ItemRarity
{
    PeasantCraft, //Common
    ApprenticeForged, //Uncommon
    ImperialForged, //Rare
    ArtisanCraft, //Epic
    MasterForged //Legendary
    
}

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    [TextArea(5,10)]
    public string description;
    [JsonIgnore] public Sprite picture;
    public int goldCost;
    public int sellCost;
    public int numberInStack = 1;
    public ItemType itemType;
    public ItemRarity itemRarity;
    public EquipmentSlot equipmentSlot;
    public bool enchanted = false;
    public string pathToPicture;

    public bool isIncluded;

    [Header("Weapon Info")]
    public int mainDamage;
    public int mainMagicDamage;
    public int mainAttackRange;
    public int offDamage;
    public int offMagicDamage;
    public int offAttackRange;
    public NumberOfHands numberOfHands;
    public WeaponType weaponType;

    [Header("Armor Info")]
    public int defense;
    public int magicDefense;

    [Header("Stat Modifier Info")]
    public int healthModifier;
    public int staminaModifier;
    public int magicModifier;
    public int strengthModifier;
    public int intelligenceModifier;
    public int speedModifier;  
    public int attackModifier;
    public int defenseModifier;
    public int arcaneModifier;
    public int wardModifier;  

    [Header("Scroll Info")]
    public Ability ability;

    [Header("Consumable Info")]
    public int healthToRecover;
    public int staminaToRecover;
    public int magicToRecover;




    public ItemType GetItemType()
    {
        return itemType;
    }

    public ItemRarity GetRarity()
    {
        return itemRarity;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public void OnEnable()
    {
        if (picture == null)
        {
            Debug.LogWarning(this.itemName + " does not have a picture");
        }
    }

}

[System.Serializable]
public class InventoryItem
{
    [JsonIgnore] public Item item;
    public string stringID;
    public int amount;
    public bool equipped;
    //public bool unlocked;
    public int equipmentSlotIndex;
    public InventoryItem(Item _item, int _amount, bool _equipped, int _equipmentSlotIndex)
    {
        if (_item == null) 
        { 
            // Debug.Log("Skipped");
            return; 
        }
        item = _item;
        stringID = _item.itemName;
        amount = _amount;
        equipped = _equipped;
        equipmentSlotIndex = _equipmentSlotIndex;
    }
}

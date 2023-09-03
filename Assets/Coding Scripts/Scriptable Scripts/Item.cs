using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEditor;

public enum NumberOfHands
{
    OneHanded,
    TwoHanded,
    NoHands
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
    Scroll
}

public enum WeaponType
{
    None,
    Bow,
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
        if (picture != null)
        {
            pathToPicture = AssetDatabase.GetAssetPath(picture);
        }
        // else
        // {
        //     Debug.Log(name);
        //     byte[] imageData = File.ReadAllBytes(pathToPicture);
        //     Texture2D tex = new Texture2D(2, 2);
        //     bool success = tex.LoadImage(imageData);
        //     Debug.Log(success + " was successful or not");

        //     picture = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        
        // }
    }

}

[System.Serializable]
public class InventoryItem
{
    [JsonIgnore] public Item item;
    public int ID;
    public int amount;
    public bool equipped;
    //public bool unlocked;
    public int equipmentSlotIndex;
    public InventoryItem(Item _item, int _ID, int _amount, bool _equipped, int _equipmentSlotIndex)
    {
        item = _item;
        ID = _ID;
        amount = _amount;
        equipped = _equipped;
        equipmentSlotIndex = _equipmentSlotIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Perk", menuName = "ScriptableObject/Perk")]
public class Perk : ScriptableObject
{
    [Header("Perk Info")]
    public int unlockCost;
    public string perkName;
    
    [Header("Icon Info")]
    public Sprite perkImageIcon;
    public Color borderColor;
    
    [Header("Bonus Stats")]
    public int bonusHealth;
    public int bonusStamina;
    public int bonusMagic;
    public int bonusStrength;
    public int bonusIntelligence;
    public int bonusSpeed;

    [Header("Bonus Multipliers")]
    public float healthMultiplier;
    public float staminaMultiplier;
    public float magicMultiplier;
    public float strengthMultiplier;
    public float intelligenceMultiplier;
    public float speedMultiplier;

    [Header("Description")]
    [TextArea(5,10)] public string description;

}

[System.Serializable] public class PerkObject
{
    public Perk perk;
    public int count;
    public bool unlockedBool;
    public int ID;
    public string stringID;

    public PerkObject(string _stringID, Perk _perk, int _count, bool _unlockedBool, int _ID)
    {
        stringID = _stringID;
        perk = _perk;
        count = _count;
        unlockedBool = _unlockedBool;
        ID = _ID;
    }
}

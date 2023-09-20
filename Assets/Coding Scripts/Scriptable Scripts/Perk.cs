using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEditor;

[CreateAssetMenu(fileName = "New Perk", menuName = "ScriptableObject/Perk")]
public class Perk : ScriptableObject
{
    [Header("Perk Info")]
    public int unlockCost;
    public string perkName;
    
    [Header("Icon Info")]
    [JsonIgnore] public Sprite picture;
    [JsonIgnore] public Color borderColor;
    public string pathToPicture;
    
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

    public void OnEnable()
    {
        if (picture == null)
        {
            Debug.LogWarning(this.perkName + " does not have a picture");
        }
    }

}

[System.Serializable] public class PerkObject
{
    [JsonIgnore] public Perk perk;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

public enum CostType
{
    Magic,
    Stamina,
    Both,
    None
}

[CreateAssetMenu(fileName = "New Ability", menuName = "ScriptableObject/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    [JsonIgnore] public Sprite picture;
    public string pathToPicture;
    [JsonIgnore] [SerializeField] public Color borderColor;
    public CostType costType;
    public int unlockCost;
    public int unlockHealth = 1;
    public int unlockStamina = 1;
    public int unlockMagic = 1;
    public int unlockStrength = 1;
    public int unlockIntelligence = 1;
    public int unlockSpeed = 1;

    public AbilityLevelObject[] allAbilityLevels;

    // public void OnEnable()
    // {
    //     if (picture != null)
    //     {
    //         #if UNITY_EDITOR
    //         pathToPicture = AssetDatabase.GetAssetPath(picture);
    //         #endif
    //     }
    // }

}

[System.Serializable] public class AbilityLevelObject
{
    public int level;
    public int upgradeCost;

    public int magicCost;
    public int staminaCost;
    public string range;
    public string damage;
    public string magicDamage;

    [TextArea(5,20)] public string description;

}

public enum AbilityType
{
    classAblity,
    raceAbility,
    learnedAbility
}

[System.Serializable] public class AbilitySaveObject
{
    public string stringID;
    [JsonIgnore] public Ability ability;
    public AbilityType abilityType;
    public int currentLevel;
    public int viewingLevel;
    public bool unlocked;
    
    public AbilitySaveObject(string _stringID, Ability _ability, AbilityType _abilityType, int _level,
        int _viewingLevel, bool _unlocked)
    {
        stringID = _stringID;
        ability = _ability;
        abilityType = _abilityType;
        currentLevel = _level;
        viewingLevel = _viewingLevel;
        unlocked = _unlocked;
    }
}


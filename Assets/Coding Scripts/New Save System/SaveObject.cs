using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveObject
{
    public int numberOfCharacters;
    public int characterFileNumber;

    public string nameOfCharacter;
    public string characterID;

    public int baseHealth;
    public int baseStamina;
    public int baseMagic;
    public int baseSpeed;
    public int baseStrength;
    public int baseIntelligence;

    public int currentHealth;
    public int currentStamina;
    public int currentMagic;
    public int currentSpeed;
    public int currentStrength;
    public int currentIntelligence;

    public int level;
    public int levelPoints;
    public int levelRolls;
    public int classAbilityPoints;
    public int raceAbilityPoints;
    public bool hasLevelUp = false;

    public int gold;

    public string race;
    public string characterClass;

    public int bonusAttack;
    public int bonusDefense;
    public int holdingCapacity;

    public int bonusMagicAttack;
    public int bonusMagicDefense;
    public int spellbookCapacity;

    public int movement;

    public List<AbilitySaveObject> abilityInventory = new List<AbilitySaveObject>();
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public List<PerkObject> perks = new List<PerkObject>();
    public InventoryItem[] equipment;
    public List<string> notes = new List<string>();

    [JsonIgnore] public Race raceObject;
    [JsonIgnore] public Class classObject;

    public SaveObject()
    {
        characterID = Guid.NewGuid().ToString();
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length - 1;

        equipment = new InventoryItem[numSlots];
    }

}
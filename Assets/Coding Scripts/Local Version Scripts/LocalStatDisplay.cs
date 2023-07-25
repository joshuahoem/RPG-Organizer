using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LocalStatDisplay : MonoBehaviour
{
    [Header("Main Info")]
    #region //TMP references
    [SerializeField] TextMeshProUGUI nameOfCharacter;
    [SerializeField] TextMeshProUGUI race;
    [SerializeField] TextMeshProUGUI characterSelectedClass;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI magic;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI intelligence;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] Image imageReference;
    [SerializeField] Image classimage;
    [SerializeField] TextMeshProUGUI racePointsTMP;
    [SerializeField] TextMeshProUGUI classPointsTMP;
    [SerializeField] TextMeshProUGUI goldTMP;
    #endregion

    [Header("Bonus Stats")]
    #region //Bonus Stats from Stats
    [SerializeField] TextMeshProUGUI bonusAttack;
    [SerializeField] TextMeshProUGUI bonusDefense;
    [SerializeField] TextMeshProUGUI holdingCapacity;
    [SerializeField] TextMeshProUGUI bonusMagicAttack;
    [SerializeField] TextMeshProUGUI bonusMagicDefense;
    [SerializeField] TextMeshProUGUI spellbookCapacity;
    [SerializeField] TextMeshProUGUI movement;
    #endregion

    [Header("Eqipment Bonus")]
    #region Bonus Stats from Equipment
    [SerializeField] TextMeshProUGUI bonusAttackEquipment;
    [SerializeField] TextMeshProUGUI bonusDefenseEquipment;
    [SerializeField] TextMeshProUGUI bonusMagicAttackEquipment;
    [SerializeField] TextMeshProUGUI bonusMagicDefenseEquipment;

    [SerializeField] TextMeshProUGUI bonusAttackStatTMP;
    [SerializeField] TextMeshProUGUI bonusArmorStatTMP;
    [SerializeField] TextMeshProUGUI bonusArcaneStatTMP;
    [SerializeField] TextMeshProUGUI bonusWardStatTMP;

    [SerializeField] TextMeshProUGUI totalAttackEquipment;
    [SerializeField] TextMeshProUGUI totalDefenseEquipment;
    [SerializeField] TextMeshProUGUI totalMagicAttackEquipment;
    [SerializeField] TextMeshProUGUI totalMagicDefenseEquipment;
    #endregion

    [Header("Base Bonus")]
    #region Bonus Base Stats
    [SerializeField] TextMeshProUGUI bonusHealthTMP;
    [SerializeField] TextMeshProUGUI bonusStaminaTMP;
    [SerializeField] TextMeshProUGUI bonusMagicTMP;
    [SerializeField] TextMeshProUGUI bonusStrengthTMP;
    [SerializeField] TextMeshProUGUI bonusIntelligenceTMP;
    [SerializeField] TextMeshProUGUI bonusSpeedTMP;
    int bonusHealth = 0;
    int bonusStamina = 0;
    int bonusMagic = 0;
    int bonusStrength = 0;
    int bonusIntelligence = 0;
    int bonusSpeed = 0;
    #endregion

    public SaveObject save;
    public string charString;

    private void Awake() 
    {
        FindCurrentSave();
        LoadCurrentSave();
    }

    public void UpdateUI()
    {
        FindCurrentSave();
        LoadBaseStatBonus();
        LoadCurrentSave();
        UpdateBonusUI();
    }

    private void FindCurrentSave()
    {
        string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";

        if (File.Exists(SAVE_FOLDER + "/character_manager.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/character_manager.txt");

            SaveState saveState = JsonUtility.FromJson<SaveState>(saveString);

            charString = saveState.fileIndexString;

            if (File.Exists(SAVE_FOLDER + "/save_" + charString + ".txt"))
            {
                string newSaveString = File.ReadAllText(SAVE_FOLDER + "/save_" + charString + ".txt");

                save = JsonUtility.FromJson<SaveObject>(newSaveString);

            }
            else
            {
                Debug.Log("Could not find character folder!");
            }
        }
        else
        {
            Debug.Log("Could not find character manager folder!");
        }
    }

    private void LoadCurrentSave()
    {
        nameOfCharacter.text = save.nameOfCharacter;
        race.text = save.race;
        characterSelectedClass.text = save.characterClass;
        levelText.text = save.level.ToString();
        racePointsTMP.text = save.raceAbilityPoints.ToString();
        classPointsTMP.text = save.classAbilityPoints.ToString();
        goldTMP.text = save.gold.ToString();
        if (save.raceObject != null)
        {
            if (save.raceObject.picture != null)
            { imageReference.sprite = save.raceObject.picture; }
        }
        if (save.classObject != null)
        {
            if (save.classObject.logo != null)
            { classimage.sprite = save.classObject.logo; }
        }

        float healthX = 1;
        float staminaX = 1;
        float magicX = 1;
        float strengthX = 1;
        float intelligenceX = 1;
        float speedX = 1;

        foreach (PerkObject perk in save.perks)
        {
            if (perk.perk == null) 
            { 
                Debug.Log("no perk here");
                continue;
            }
            healthX += perk.perk.healthMultiplier;
            staminaX += perk.perk.staminaMultiplier;
            magicX += perk.perk.magicMultiplier;
            strengthX += perk.perk.strengthMultiplier;
            intelligenceX += perk.perk.intelligenceMultiplier;
            speedX += perk.perk.speedMultiplier;
        }
        
        int _bonusHealth = Mathf.FloorToInt((bonusHealth + save.baseHealth) * healthX);
        int _bonusStamina = Mathf.FloorToInt((bonusStamina + save.baseStamina) * staminaX);
        int _bonusMagic = Mathf.FloorToInt((bonusMagic + save.baseMagic) * magicX);
        int _bonusStrength = Mathf.FloorToInt((bonusStrength + save.baseStrength) * strengthX);
        int _bonusIntelligence = Mathf.FloorToInt((bonusIntelligence + save.baseIntelligence) * intelligenceX);
        int _bonusSpeed = Mathf.FloorToInt((bonusSpeed + save.baseSpeed) * speedX);

        if (_bonusHealth < save.currentHealth) { save.currentHealth = _bonusHealth; }
        if (_bonusStamina < save.currentStamina) { save.currentStamina = _bonusStamina; }
        if (_bonusMagic < save.currentMagic) { save.currentMagic = _bonusMagic; }

        health.text = save.currentHealth + "/" + _bonusHealth;
        stamina.text = save.currentStamina + "/" + _bonusStamina;
        magic.text = save.currentMagic + "/" + _bonusMagic;

        strength.text = _bonusStrength.ToString();
        intelligence.text = _bonusIntelligence.ToString();
        speed.text = _bonusSpeed.ToString();

        //Bonus Stats
        bonusAttack.text = save.bonusAttack.ToString();
        bonusDefense.text = save.bonusDefense.ToString();
        holdingCapacity.text = save.holdingCapacity.ToString();

        bonusMagicAttack.text = save.bonusMagicAttack.ToString();
        bonusMagicDefense.text = save.bonusMagicDefense.ToString();
        spellbookCapacity.text = save.spellbookCapacity.ToString();

        movement.text = save.movement.ToString();

        FindObjectOfType<CharacterPanelManager>().characterNameString = save.nameOfCharacter;
    }

    private void UpdateBonusUI()
    {
        SaveObject _save = NewSaveSystem.FindCurrentSave();
        int _bonusAttack = 0;
        int _totalAttack = save.bonusAttack;
        int _bonusDefense = 0;
        int _totalDefense = save.bonusDefense;
        int _bonusMagicAttack = 0;
        int _totalMagicAttack = save.bonusMagicAttack;
        int _bonusMagicDefense = 0;
        int _totalMagicDefense = save.bonusMagicDefense;

        foreach (InventoryItem item in save.equipment)
        {
            if (item.item != null)
            {
                if (item.equipmentSlotIndex == (int) EquipmentSlot.MainHand)
                {
                    _bonusAttack += item.item.mainDamage;
                    _bonusMagicAttack += item.item.mainMagicDamage;
                }
                else if (item.equipmentSlotIndex == (int) EquipmentSlot.OffHand)
                {
                    _bonusAttack += item.item.offDamage;
                    _bonusMagicAttack += item.item.offMagicDamage;
                }

                _bonusDefense += item.item.defense;
                _bonusMagicDefense += item.item.magicDefense;
            }
        }

        bonusAttackEquipment.text = _bonusAttack.ToString();
        bonusDefenseEquipment.text = _bonusDefense.ToString();
        bonusMagicAttackEquipment.text = _bonusMagicAttack.ToString();
        bonusMagicDefenseEquipment.text = _bonusMagicDefense.ToString();

        _totalAttack += _bonusAttack;
        _totalDefense += _bonusDefense;
        _totalMagicAttack += _bonusMagicAttack;
        _totalMagicDefense += _bonusMagicDefense;

        bonusAttackStatTMP.text = save.bonusAttack.ToString();
        bonusArmorStatTMP.text = save.bonusDefense.ToString();
        bonusArcaneStatTMP.text = save.bonusMagicAttack.ToString();
        bonusWardStatTMP.text = save.bonusMagicDefense.ToString();

        totalAttackEquipment.text = _totalAttack.ToString();
        totalDefenseEquipment.text = _totalDefense.ToString();
        totalMagicAttackEquipment.text = _totalMagicAttack.ToString();
        totalMagicDefenseEquipment.text = _totalMagicDefense.ToString();


    }

    private void LoadBaseStatBonus()
    {
        bonusHealth = 0;
        bonusStamina = 0;
        bonusMagic = 0;
        bonusStrength = 0;
        bonusIntelligence = 0;
        bonusSpeed = 0;

        foreach (PerkObject perkObject in save.perks)
        {
            bonusHealth += perkObject.perk.bonusHealth * perkObject.count;
            bonusStamina += perkObject.perk.bonusStamina * perkObject.count;
            bonusMagic += perkObject.perk.bonusMagic * perkObject.count;
            bonusStrength += perkObject.perk.bonusStrength * perkObject.count;
            bonusIntelligence += perkObject.perk.bonusIntelligence * perkObject.count;
            bonusSpeed += perkObject.perk.bonusSpeed * perkObject.count;
        }

        bonusHealthTMP.text = bonusHealth.ToString();
        bonusStaminaTMP.text = bonusStamina.ToString();
        bonusMagicTMP.text = bonusMagic.ToString();
        bonusStrengthTMP.text = bonusStrength.ToString();
        bonusIntelligenceTMP.text = bonusIntelligence.ToString();
        bonusSpeedTMP.text = bonusSpeed.ToString();

    }
}

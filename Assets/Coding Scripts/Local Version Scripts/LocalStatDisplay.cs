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

    [Header("Abilities")]
    [SerializeField] Ability dualWieldAbility;
    public string charString;

    private void Start() 
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        LoadBaseStatBonus();
        LoadCurrentSave();
        UpdateBonusUI();
    }

    private void LoadCurrentSave()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (nameOfCharacter != null) { nameOfCharacter.text = save.nameOfCharacter; }
        if (race != null) { race.text = save.race; }
        if (characterSelectedClass != null) { characterSelectedClass.text = save.characterClass; }
        if (levelText != null) { levelText.text = save.level.ToString(); }
        if (racePointsTMP != null) { racePointsTMP.text = save.raceAbilityPoints.ToString(); }
        if (classPointsTMP != null) { classPointsTMP.text = save.classAbilityPoints.ToString(); }
        if (goldTMP != null) { goldTMP.text = save.gold.ToString(); }
        if (save.raceObject != null)
        {
            { if (imageReference != null) { imageReference.sprite = SaveManagerVersion3.LoadSprite(save.raceObject.pathToPicture); } }
        }
        if (save.classObject != null)
        {
            { if (classimage != null) { classimage.sprite = SaveManagerVersion3.LoadSprite(save.classObject.pathToPicture); } }
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
                // Debug.Log("no perk here");
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

        if (health != null) { health.text = save.currentHealth + "/" + _bonusHealth; }
        if (stamina != null) { stamina.text = save.currentStamina + "/" + _bonusStamina; }
        if (magic != null) { magic.text = save.currentMagic + "/" + _bonusMagic; }

        if (strength != null) { strength.text = _bonusStrength.ToString(); }
        if (intelligence != null) { intelligence.text = _bonusIntelligence.ToString(); }
        if (speed != null) { speed.text = _bonusSpeed.ToString(); }

        //Bonus Stats
        if (bonusAttack != null) { bonusAttack.text = save.bonusAttack.ToString(); }
        if (bonusDefense != null) { bonusDefense.text = save.bonusDefense.ToString(); }
        if (holdingCapacity != null) { holdingCapacity.text = save.holdingCapacity.ToString(); }

        if (bonusMagicAttack != null) { bonusMagicAttack.text = save.bonusMagicAttack.ToString(); }
        if (bonusMagicDefense != null) { bonusMagicDefense.text = save.bonusMagicDefense.ToString(); }
        if (spellbookCapacity != null) { spellbookCapacity.text = save.spellbookCapacity.ToString(); }

        if (movement != null) { movement.text = save.movement.ToString(); }

        CharacterPanelManager characterPanelManager = FindObjectOfType<CharacterPanelManager>();
        if (characterPanelManager != null) { characterPanelManager.characterNameString = save.nameOfCharacter; }
    }

    private void UpdateBonusUI()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
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
            if (item == null) { continue; }
            if (item.item != null)
            {
                if (item.equipmentSlotIndex == (int) EquipmentSlot.MainHand)
                {
                    _bonusAttack += item.item.mainDamage;
                    _bonusMagicAttack += item.item.mainMagicDamage;
                }
                else if (item.equipmentSlotIndex == (int) EquipmentSlot.OffHand)
                {
                    if (SaveManagerVersion3.DoesPlayerHaveThisAbility(dualWieldAbility))
                    {
                        _bonusAttack += item.item.mainDamage;
                        _bonusMagicAttack += item.item.mainMagicDamage;
                    }
                    else
                    {
                        _bonusAttack += item.item.offDamage;
                        _bonusMagicAttack += item.item.offMagicDamage;
                    }
                }

                _bonusDefense += item.item.defense;
                _bonusMagicDefense += item.item.magicDefense;
            }
        }

        if (bonusAttackEquipment != null) { bonusAttackEquipment.text = _bonusAttack.ToString(); }
        if (bonusDefenseEquipment != null) { bonusDefenseEquipment.text = _bonusDefense.ToString(); }
        if (bonusMagicAttackEquipment != null) { bonusMagicAttackEquipment.text = _bonusMagicAttack.ToString(); }
        if (bonusMagicDefenseEquipment != null) { bonusMagicDefenseEquipment.text = _bonusMagicDefense.ToString(); }

        _totalAttack += _bonusAttack;
        _totalDefense += _bonusDefense;
        _totalMagicAttack += _bonusMagicAttack;
        _totalMagicDefense += _bonusMagicDefense;

        if (bonusAttackStatTMP != null) { bonusAttackStatTMP.text = save.bonusAttack.ToString(); }
        if (bonusArmorStatTMP != null) { bonusArmorStatTMP.text = save.bonusDefense.ToString(); }
        if (bonusArcaneStatTMP != null) { bonusArcaneStatTMP.text = save.bonusMagicAttack.ToString(); }
        if (bonusWardStatTMP != null) { bonusWardStatTMP.text = save.bonusMagicDefense.ToString(); }

        if (totalAttackEquipment != null) { totalAttackEquipment.text = _totalAttack.ToString(); }
        if (totalDefenseEquipment != null) { totalDefenseEquipment.text = _totalDefense.ToString(); }
        if (totalMagicAttackEquipment != null) { totalMagicAttackEquipment.text = _totalMagicAttack.ToString(); }
        if (totalMagicDefenseEquipment != null) { totalMagicDefenseEquipment.text = _totalMagicDefense.ToString(); }


    }

    private void LoadBaseStatBonus()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        bonusHealth = 0;
        bonusStamina = 0;
        bonusMagic = 0;
        bonusStrength = 0;
        bonusIntelligence = 0;
        bonusSpeed = 0;

        foreach (PerkObject perkObject in save.perks)
        {
            if (perkObject.perk == null)
            {
                perkObject.perk = LoadGameMasterHandler.Instance.GetPerk(perkObject.stringID);
            }
            bonusHealth += perkObject.perk.bonusHealth * perkObject.count;
            bonusStamina += perkObject.perk.bonusStamina * perkObject.count;
            bonusMagic += perkObject.perk.bonusMagic * perkObject.count;
            bonusStrength += perkObject.perk.bonusStrength * perkObject.count;
            bonusIntelligence += perkObject.perk.bonusIntelligence * perkObject.count;
            bonusSpeed += perkObject.perk.bonusSpeed * perkObject.count;
        }

        if (bonusHealthTMP != null) { bonusHealthTMP.text = bonusHealth.ToString(); }
        if (bonusStaminaTMP != null) { bonusStaminaTMP.text = bonusStamina.ToString(); }
        if (bonusMagicTMP != null) { bonusMagicTMP.text = bonusMagic.ToString(); }
        if (bonusStrengthTMP != null) { bonusStrengthTMP.text = bonusStrength.ToString(); }
        if (bonusIntelligenceTMP != null) { bonusIntelligenceTMP.text = bonusIntelligence.ToString(); }
        if (bonusSpeedTMP != null) { bonusSpeedTMP.text = bonusSpeed.ToString(); }

    }
}

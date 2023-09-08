using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class LocalLevelUpManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelPoints;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI magic;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI intelligence;
    [SerializeField] TextMeshProUGUI speed;

    [SerializeField] Button levelupButton;
    [SerializeField] GameObject[] alertObjects;
    [SerializeField] TextMeshProUGUI alertText;

    #region Level Up Panel
    [SerializeField] GameObject levelupPanel;
    [SerializeField] TextMeshProUGUI numberOfRollsTMP;
    int maxRolls;
    #endregion

    string indexOfSave;

    int level;
    int points;
    int rolls;
    int currentHealth;
    int currentStamina;
    int currentMagic;
    int currentStrength;
    int currentIntelligence;
    int currentSpeed;

    int _classAbilityPoints;
    int _raceAbilityPoints;

    [Header("Abilities")] 
    [SerializeField] Ability noviceAbility;
    [SerializeField] Ability dwarvenStrengthAbility;

    private void Start() 
    {
        indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;

        SetStats();
        UpdateUI();
        CheckIfLevelUp();
    }

    public void CheckChanges()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        indexOfSave = FindObjectOfType<LocalStatDisplay>().charString;

        SetStats();
        UpdateUI();
    }

    private void SetStats()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        level = save.level;
        points = save.levelPoints;
        rolls = save.levelRolls;

        currentHealth = save.baseHealth;
        currentStamina = save.baseStamina;
        currentMagic = save.baseMagic;
        currentStrength = save.baseStrength;
        currentIntelligence = save.baseIntelligence;
        currentSpeed = save.baseSpeed;

        _raceAbilityPoints = save.raceAbilityPoints;
        _classAbilityPoints = save.classAbilityPoints;
    }

    private void UpdateUI()
    {
        levelText.text = level.ToString();
        levelPoints.text = points.ToString();
        health.text = currentHealth.ToString();
        stamina.text = currentStamina.ToString();
        magic.text = currentMagic.ToString();
        strength.text = currentStrength.ToString();
        intelligence.text = currentIntelligence.ToString();
        speed.text = currentSpeed.ToString();
    }

    public void AddLevel()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        save.level++;
        save.levelPoints += 2;
        save.levelRolls += 2;

        level++;
        points += 2;
        rolls += 2;

        save.raceAbilityPoints += 1;
        save.classAbilityPoints += 1;

        save.hasLevelUp = true;
    }

    public void ConfirmLevelChanges()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        save.level = level;
        save.levelPoints = points;
        save.levelRolls = rolls;

        save.baseHealth = currentHealth;
        save.baseStamina = currentStamina;
        save.baseMagic = currentMagic;
        save.baseStrength = currentStrength;
        save.baseIntelligence = currentIntelligence;
        save.baseSpeed = currentSpeed;

        save.classAbilityPoints = _classAbilityPoints;
        save.raceAbilityPoints = _raceAbilityPoints;

        #region//bonus Stats
        SaveLoadManager saveLoad = FindObjectOfType<SaveLoadManager>();
        int bonusAttackModifer = saveLoad.bonusAttackModifer;
        int bonusDefenseModifer = saveLoad.bonusDefenseModifer;
        int holdingCapacityModifer = saveLoad.holdingCapacityModifer;
        int bonusMagicAttackModifer = saveLoad.bonusMagicAttackModifer;
        int bonusMagicDefenseModifer = saveLoad.bonusMagicDefenseModifer;
        int spellbookCapacityModifer = saveLoad.spellbookCapacityModifer;
        int movementModifer = saveLoad.movementModifer;

        save.bonusAttack = Mathf.FloorToInt(save.baseStrength / bonusAttackModifer);
        save.bonusDefense = Mathf.FloorToInt(save.baseStrength / bonusDefenseModifer);
        save.holdingCapacity = Mathf.FloorToInt(save.baseStrength / holdingCapacityModifer);

        save.bonusMagicAttack = Mathf.FloorToInt(save.baseIntelligence / bonusMagicAttackModifer);
        save.bonusMagicDefense = Mathf.FloorToInt(save.baseIntelligence / bonusMagicDefenseModifer);
        save.spellbookCapacity = Mathf.FloorToInt(save.baseIntelligence / spellbookCapacityModifer);

        save.movement = Mathf.FloorToInt(save.baseSpeed / movementModifer);
        #endregion
        
        if (save.levelPoints < 1)
        {
            save.hasLevelUp = false;
        }

        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);

    }

    public void CheckIfLevelUp()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if (save.hasLevelUp == false)
        {
            levelupButton.interactable = false;
            foreach (GameObject alert in alertObjects)
            {
                alert.SetActive(false);
            }
        }
        else if (save.hasLevelUp == true)
        {
            levelupButton.interactable = true;
            foreach (GameObject alert in alertObjects)
            {
                alert.SetActive(true);
            }
            alertText.text = save.levelPoints.ToString();

        }
    }
    public void AddHealth()
    {
        if (points < 1) {return;}
        currentHealth++;
        points--;
        UpdateUI();
    }

    public void AddStamina()
    {
        if (points < 1) { return; }
        currentStamina++;
        points--;
        UpdateUI();
    }

    public void AddMagic()
    {
        if (points < 1) { return; }
        currentMagic++;
        points--;
        UpdateUI();
    }

    public void AddStrength()
    {
        if (points < 1) { return; }
        if (SaveManagerVersion3.DoesPlayerHaveThisAbility(dwarvenStrengthAbility))
        {
            currentStrength++;
        }
        currentStrength++;
        points--;
        UpdateUI();
    }

    public void AddIntelligence()
    {
        if (points < 1) { return; }
        currentIntelligence++;
        points--;
        UpdateUI();
    }

    public void AddSpeed()
    {
        if (points < 1) { return; }
        currentSpeed++;
        points--;
        UpdateUI();
    }

    public void SubtractHealth()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if ( currentHealth == save.baseHealth) {return;}
        currentHealth--;
        points++;
        UpdateUI();
    }

    public void SubtractStamina()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if ( currentStamina == save.baseStamina) {return;}
        currentStamina--;
        points++;
        UpdateUI();
    }

    public void SubtractMagic()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if ( currentMagic == save.baseMagic) {return;}
        currentMagic--;
        points++;
        UpdateUI();
    }

    public void SubtractStrength()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if ( currentStrength == save.baseStrength) {return;}
        if (SaveManagerVersion3.DoesPlayerHaveThisAbility(dwarvenStrengthAbility))
        {
            currentStrength--;
        }
        currentStrength--;
        points++;
        UpdateUI();
    }

    public void SubtractIntelligence()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if ( currentIntelligence == save.baseIntelligence) {return;}
        currentIntelligence--;
        points++;
        UpdateUI();
    }

    public void SubtractSpeed()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        if ( currentSpeed == save.baseSpeed) {return;}
        currentSpeed--;
        points++;
        UpdateUI();
    }

    public void LoadRolls()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (save.levelRolls <= 0) {return;}
        levelupPanel.SetActive(true);
        maxRolls = save.levelRolls;
        save.levelRolls = 0;
        numberOfRollsTMP.text = save.levelRolls.ToString();
    }

    public void AddLevelRolls()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (save.levelRolls + 1 > maxRolls) {return;}
        save.levelRolls++;
        numberOfRollsTMP.text = save.levelRolls.ToString();
    }

    public void SubtractLevelRolls()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (save.levelRolls - 1 < 0) {return;}
        save.levelRolls--;
        numberOfRollsTMP.text = save.levelRolls.ToString();
    }

    public void ConfirmLevelRolls()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        // Debug.Log(save.levelRolls);
        // Debug.Log(save.levelPoints);
        save.levelPoints += save.levelRolls;
        if (SaveManagerVersion3.DoesPlayerHaveThisAbility(noviceAbility))
        {
            save.levelPoints += 2;
        }
        save.levelRolls = 0;

        levelupPanel.SetActive(false);
        SetStats();
        UpdateUI();
    }

}

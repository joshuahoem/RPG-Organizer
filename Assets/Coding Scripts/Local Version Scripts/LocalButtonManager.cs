using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public enum StatEnum
{
    Health,
    Stamina,
    Magic,
    Strength,
    Intelligence,
    Speed
}

public class LocalButtonManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statDisplayObject;
    [SerializeField] int amountToChange;
    public string charString;

    [SerializeField] StatEnum statChanging;

    int bonusHealth = 0;
    int bonusStamina = 0;
    int bonusMagic = 0;
    int bonusStrength = 0;
    int bonusIntelligence = 0;
    int bonusSpeed = 0;

    int totalHealth;
    int totalStamina;
    int totalMagic;
    int totalStrength;
    int totalIntelligence;
    int totalSpeed;

    public void AddAmount()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        LoadBaseStatBonus();
        LoadMultipliers();

        switch (statChanging)
        {
            case StatEnum.Health:
                if ((save.currentHealth + amountToChange) > (totalHealth)) {return;}
                save.currentHealth += amountToChange;
                statDisplayObject.text = save.currentHealth + "/" + (totalHealth);
                break;
            case StatEnum.Stamina:
                if ((save.currentStamina + amountToChange) > (totalStamina)) {return;}
                save.currentStamina += amountToChange;
                statDisplayObject.text = save.currentStamina + "/" + (totalStamina);
                break;
            case StatEnum.Magic:
                if ((save.currentMagic + amountToChange) > (totalMagic)) {return;}
                save.currentMagic += amountToChange;
                statDisplayObject.text = save.currentMagic + "/" + (totalMagic);
                break;
            case StatEnum.Strength:
                if ((save.currentStrength + amountToChange) > (totalStrength)) {return;}
                save.currentStrength += amountToChange;
                statDisplayObject.text = save.currentStrength + "/" + (totalStrength);
                break;
            case StatEnum.Intelligence:
                if ((save.currentIntelligence + amountToChange) > (totalIntelligence)) {return;}
                save.currentIntelligence += amountToChange;
                statDisplayObject.text = save.currentIntelligence + "/" + (totalIntelligence);
                break;
            case StatEnum.Speed:
                if ((save.currentSpeed + amountToChange) > (totalSpeed)) {return;}
                save.currentSpeed += amountToChange;
                statDisplayObject.text = save.currentSpeed + "/" + (totalSpeed);
                break;
        }


    }

    public void SubtractAmount()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        LoadBaseStatBonus();
        LoadMultipliers();

        switch (statChanging)
        {
            case StatEnum.Health:
                if (((save.currentHealth) - amountToChange) < 0) {return;}
                save.currentHealth -= amountToChange;
                statDisplayObject.text = save.currentHealth + "/" + (totalHealth);
                break;
            case StatEnum.Stamina:
                if (((save.currentStamina) - amountToChange) < 0) {return;}
                save.currentStamina -= amountToChange;
                statDisplayObject.text = save.currentStamina + "/" + (totalStamina);
                break;
            case StatEnum.Magic:
                if (((save.currentMagic) - amountToChange) < 0) {return;}
                save.currentMagic -= amountToChange;
                statDisplayObject.text = save.currentMagic + "/" + (totalMagic);
                break;
            case StatEnum.Strength:
                if (((save.currentStrength) - amountToChange) < 0) {return;}
                save.currentStrength -= amountToChange;
                statDisplayObject.text = save.currentStrength + "/" + (totalStrength);
                break;
            case StatEnum.Intelligence:
                if (((save.currentIntelligence) - amountToChange) < 0) {return;}
                save.currentIntelligence -= amountToChange;
                statDisplayObject.text = save.currentIntelligence + "/" + (totalIntelligence);
                break;
            case StatEnum.Speed:
                if (((save.currentSpeed) - amountToChange) < 0) {return;}
                save.currentSpeed -= amountToChange;
                statDisplayObject.text = save.currentSpeed + "/" + (totalSpeed);
                break;
        }

    }

    private void LoadBaseStatBonus()
    {
        bonusHealth = 0;
        bonusStamina = 0;
        bonusMagic = 0;
        bonusStrength = 0;
        bonusIntelligence = 0;
        bonusSpeed = 0;

        SaveObject save = SaveManagerVersion3.FindCurrentSave();

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
    }

    private void LoadMultipliers()
    {
        float healthX = 1;
        float staminaX = 1;
        float magicX = 1;
        float strengthX = 1;
        float intelligenceX = 1;
        float speedX = 1;

        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        foreach (PerkObject perk in save.perks)
        {
            healthX += perk.perk.healthMultiplier;
            staminaX += perk.perk.staminaMultiplier;
            magicX += perk.perk.magicMultiplier;
            strengthX += perk.perk.strengthMultiplier;
            intelligenceX += perk.perk.intelligenceMultiplier;
            speedX += perk.perk.speedMultiplier;
        }

        foreach (InventoryItem item in save.equipment)
        {
            if (item == null) { continue; }
            if (item.item == null)
            { item.item = LoadGameMasterHandler.Instance.GetItem(item.stringID); }

            if (item.item != null)
            {
                bonusHealth += item.item.healthModifier;
                bonusStamina += item.item.staminaModifier;
                bonusMagic += item.item.magicModifier;
                bonusStrength += item.item.strengthModifier;
                bonusIntelligence += item.item.intelligenceModifier;
                bonusSpeed += item.item.speedModifier;
            }
        }

        totalHealth = Mathf.FloorToInt((bonusHealth + save.baseHealth) * healthX);
        totalStamina = Mathf.FloorToInt((bonusStamina + save.baseStamina) * staminaX);
        totalMagic = Mathf.FloorToInt((bonusMagic + save.baseMagic) * magicX);
        totalStrength = Mathf.FloorToInt((bonusStrength + save.baseStrength) * strengthX);
        totalIntelligence = Mathf.FloorToInt((bonusIntelligence + save.baseIntelligence) * intelligenceX);
        totalSpeed = Mathf.FloorToInt((bonusSpeed + save.baseSpeed) * speedX);
    }

}

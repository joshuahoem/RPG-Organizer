using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldToAddTMP;
    [SerializeField] TextMeshProUGUI currentGoldTMP;
    [SerializeField] LootManager lootManager;
    int goldToAdd;
    private void Start() 
    {
        goldToAdd = 5;
        goldToAddTMP.text = goldToAdd.ToString();

        SaveObject save = NewSaveSystem.FindCurrentSave();
        currentGoldTMP.text = save.gold.ToString();
    }

    public void FullRest()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        float healthX = 1;
        float staminaX = 1;
        float magicX = 1;

        int bonusHealth = 0;
        int bonusStamina = 0;
        int bonusMagic = 0;

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

            bonusHealth += perk.perk.bonusHealth * perk.count;
            bonusStamina += perk.perk.bonusStamina * perk.count;
            bonusMagic += perk.perk.bonusMagic * perk.count;
        }

        save.currentHealth = Mathf.FloorToInt((bonusHealth + save.baseHealth) * healthX);
        save.currentStamina = Mathf.FloorToInt((bonusStamina + save.baseStamina) * staminaX);
        save.currentMagic = Mathf.FloorToInt((bonusMagic + save.baseMagic) * magicX);

        NewSaveSystem.SaveChanges(save);
    }

    public void IncreaseGold()
    {
        goldToAdd++;
        goldToAddTMP.text = goldToAdd.ToString();
    }

    public void DecreaseGold()
    {
        if (goldToAdd - 1 < 0) {return;}
        goldToAdd--;
        goldToAddTMP.text = goldToAdd.ToString();
    }

    public void AddGold()
    {
        lootManager.AddGold(0, goldToAdd);
        goldToAdd = 5;
        goldToAddTMP.text = goldToAdd.ToString();

        SaveObject save = NewSaveSystem.FindCurrentSave();
        currentGoldTMP.text = save.gold.ToString();
    }

    public void SpendGold()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();
        save.gold -= goldToAdd;
        NewSaveSystem.SaveChanges(save);
        currentGoldTMP.text = save.gold.ToString();

        goldToAdd = 5;
        goldToAddTMP.text = goldToAdd.ToString();
    }

    
}

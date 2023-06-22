using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldToAddTMP;
    [SerializeField] LootManager lootManager;
    int goldToAdd;
    private void Start() 
    {
        goldToAdd = 5;
        goldToAddTMP.text = goldToAdd.ToString();
    }

    public void FullRest()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        save.currentHealth = save.baseHealth;
        save.currentStamina = save.baseStamina;
        save.currentMagic = save.baseMagic;

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
    }

    
}

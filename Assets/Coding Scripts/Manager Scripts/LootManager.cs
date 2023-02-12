using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyScript;
using TMPro;

public class LootManager : MonoBehaviour
{
    [SerializeField] LootDrop[] lootTables;
    [SerializeField] LootDrop currentLootTable;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] ItemDatabase database;

    bool added;

    private void Start() 
    {
        dropdown.options.Clear();
        
        foreach (LootDrop loot in lootTables)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = loot.name});
        }

        dropdown.RefreshShownValue();
        dropdown.SetValueWithoutNotify(0);

        FindCurrentLootDrop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (InventoryItem item in NewSaveSystem.FindCurrentSave().inventory)
            {
                Debug.Log(item.item);
            }

            if (NewSaveSystem.FindCurrentSave().inventory.Count <= 0)
            {
                Debug.Log("none");
            }
        }
    }

    public void FindCurrentLootDrop()
    {
        string lootName = dropdown.options[dropdown.value].text;

        foreach (LootDrop loot in lootTables)
        {
            if (loot.name == lootName)
            {
                currentLootTable = loot;
            }
        }

    }

    public void GetLoot()
    {
        List<Item> loot = new List<Item>();
        loot = currentLootTable.SpawnDrop(1);
        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (Item item in loot)
        {
            foreach (InventoryItem invItem in save.inventory)
            {
                added = false;

                if (invItem.item == item)
                {
                    //same, need to add to stack
                    if (invItem.amount < item.numberInStack)
                    {
                        invItem.amount++;
                        added = true;
                    }
                }

            }

            if (!added)
            {
                InventoryItem newItem = new InventoryItem(item, database.GetID[item], 1, false, 0);
                save.inventory.Add(newItem);  
            }

        }
        
        NewSaveSystem.SaveChanges(save);          
    }

}

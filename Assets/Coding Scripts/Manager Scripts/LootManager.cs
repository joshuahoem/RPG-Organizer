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
    [SerializeField] private ErrorMessageHandler errorMessageHandler;

    [Header("Loot Panel Display")]
    [SerializeField] GameObject lootPanel;
    [SerializeField] GameObject lootItemPrefab;
    [SerializeField] Transform parentLootTransform;

    [Header("Rolls Options")]
    [SerializeField] TextMeshProUGUI rollsTMP;
    [SerializeField] int minRolls;
    [SerializeField] int maxRolls;

    [Header("Gold Modifiers")]
    [SerializeField] Sprite goldImage;
    [SerializeField] int minGoldRange;
    [SerializeField] int maxGoldRange;


    List<GameObject> lootListPrefabs = new List<GameObject>();
    bool added;
    int rolls;
    int numberBeingAdded;


    private void Start() 
    {
        lootPanel.SetActive(false);
        rolls = 3;
        rollsTMP.text = rolls.ToString();

        dropdown.options.Clear();
        foreach (LootDrop loot in lootTables)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = loot.name});
        }

        dropdown.RefreshShownValue();
        dropdown.SetValueWithoutNotify(0);

        FindCurrentLootDrop();
    }

    #region update Debug
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         foreach (InventoryItem item in NewSaveSystem.FindCurrentSave().inventory)
    //         {
    //             Debug.Log(item.item);
    //         }

    //         if (NewSaveSystem.FindCurrentSave().inventory.Count <= 0)
    //         {
    //             Debug.Log("none");
    //         }
    //     }
    // }
    #endregion

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

    public void IncreaseRolls()
    {
        if (rolls + 1 > maxRolls) {return;}
        rolls++;
        rollsTMP.text = rolls.ToString();
    }

    public void DecreaseRolls()
    {
        if (rolls - 1 < minRolls) {return;}
        rolls--;
        rollsTMP.text = rolls.ToString();
    }



    public void GetLoot()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

        //check if can hold first
        if (save.inventory.Count >= save.holdingCapacity)
        {
            Debug.Log("not strong enough to carry"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoStrength);
            return;
        }

        List<Item> loot = new List<Item>();
        loot = currentLootTable.SpawnDrop(rolls);

        lootPanel.SetActive(true);

        foreach (GameObject prefabGO in lootListPrefabs)
        {
            Destroy(prefabGO);
        }
        lootListPrefabs.Clear();

        foreach (Item item in loot)
        {
            foreach (InventoryItem invItem in save.inventory)
            {
                added = false;
                numberBeingAdded = 1;

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
                InventoryItem newItem = new InventoryItem(item, database.GetID[item], numberBeingAdded, false, 0);
                save.inventory.Add(newItem);  
            }

            added = false;
            
            foreach (GameObject lootPreabGO in lootListPrefabs)
            {
                Item lootedItem = lootPreabGO.GetComponent<LootDisplay>().item;
                if (lootedItem == item)
                {
                    lootPreabGO.GetComponent<LootDisplay>().UpdateLootDisplay();
                    added = true;
                }
            }

            if (!added)
            {
                GameObject newLoot = Instantiate(lootItemPrefab, transform.position, transform.rotation);
                newLoot.transform.SetParent(parentLootTransform);
                newLoot.GetComponent<LootDisplay>().DisplayLoot(item);
                lootListPrefabs.Add(newLoot);
            }

        }

        //ADD GOLD
        int goldAmount = 0;
        for (int i = 0; i<rolls; i++)
        {
            Debug.Log("adding");
            goldAmount += Random.Range(minGoldRange, maxGoldRange);
        }

        GameObject goldLoot = Instantiate(lootItemPrefab, transform.position, transform.rotation);
        goldLoot.transform.SetParent(parentLootTransform);
        goldLoot.GetComponent<LootDisplay>().DisplayGoldLoot(goldAmount);
        lootListPrefabs.Add(goldLoot);
        
        save.gold += goldAmount;
        Debug.Log(goldAmount);
        
        //Save Loot
        NewSaveSystem.SaveChanges(save);       
        rolls = 3;  
        rollsTMP.text = rolls.ToString();

    }

}

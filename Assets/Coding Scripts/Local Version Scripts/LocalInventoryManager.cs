using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Unity.VisualScripting;

public class LocalInventoryManager : MonoBehaviour
{
    #region Display
    [SerializeField] GameObject inventoryObject;
    [SerializeField] GameObject equipmentObjectPrefab;
    [SerializeField] GameObject emptyEquipmentPrefab;
    [SerializeField] GameObject inUseEquipmentPrefab;

    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] GameObject equipmentPanel;


    [SerializeField] GameObject parentObject;
    [SerializeField] TabManager tabManager;
    [SerializeField] ShopContentFitter contentFitter;
    [SerializeField] TextMeshProUGUI holdingCapacityNumber;
    #endregion

    SaveObject save;
    public string charString;
    [SerializeField] ItemDatabase database;

    InventoryItem searchResult;
    int searchIndex;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    List<GameObject> displayedItems = new List<GameObject>();

    public InventoryItem[] currentEquipment;
    public Transform[] parentObjectTransforms;
    List<GameObject> equipmentDisplayItems = new List<GameObject>();

    [Header("Abilities")]
    [SerializeField] Ability strengthAbility;

    private void Start() 
    {
        itemInfoPanel.SetActive(false);

        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (save.equipment.Length <= 1)
        {
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length - 1;
            save.equipment = new InventoryItem[numSlots];
        }


        currentEquipment = save.equipment;

        CheckInventory();
        
    }

    private void CheckInventory()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        foreach (InventoryItem item in save.inventory)
        {
            if (item == null)
            {
                continue;
            }
            if (item.item == null)
            {
                item.item = LoadGameMasterHandler.Instance.GetItem(item.stringID);

            }
        }

        for (int i = 0; i < save.equipment.Length; i++)
        {
            if (save.equipment[i] == null) 
            {
                continue;
            }

            if (save.equipment[i].item == null && save.equipment[i].equipped)
            {
                save.equipment[i].item = LoadGameMasterHandler.Instance.GetItem(save.equipment[i].stringID);
            }
        }
    }

    public void Equip(InventoryItem newItem, int chosenSlotIndex)
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        if (newItem.item == null)
        {
            LoadGameMasterHandler.Instance.GetItem(newItem.stringID);
        }
        if (chosenSlotIndex == 0)
        {
            //0 is default and so none was chosen
            int slotIndex = (int)newItem.item.equipmentSlot;

            if(currentEquipment[slotIndex] == null) 
            {
                InventoryItem newInstance = new InventoryItem(newItem.item, 1, true, slotIndex); 
                currentEquipment[slotIndex] = newInstance;
            }
            else if (currentEquipment[slotIndex].item == null)
            {
                InventoryItem newInstance = new InventoryItem(newItem.item, 1, true, slotIndex); 
                currentEquipment[slotIndex] = newInstance;
            }
            else
            {
                // Debug.Log("not empty, switching");
                if (currentEquipment[slotIndex].item.numberOfHands == NumberOfHands.TwoHanded)
                {
                    if (slotIndex == (int) EquipmentSlot.MainHand)
                    {
                        currentEquipment[slotIndex + 1] = null;
                    }
                    else if (slotIndex == (int) EquipmentSlot.OffHand)
                    {
                        currentEquipment[slotIndex - 1] = null;
                    }
                    
                }
                inventory.Add(UnequipNoSave(currentEquipment[slotIndex].item));

                InventoryItem newInstance = new InventoryItem(newItem.item, 1, true, slotIndex); 
                currentEquipment[slotIndex] = newInstance;

            }

            
        }
        else
        {
            if (currentEquipment[chosenSlotIndex] == null)
            {
                currentEquipment[chosenSlotIndex] = new InventoryItem(newItem.item, 1, true, chosenSlotIndex); 
            }
            else if(currentEquipment[chosenSlotIndex].item == null)
            {
                currentEquipment[chosenSlotIndex] = new InventoryItem(newItem.item, 1, true, chosenSlotIndex); 
            }
            else
            {
                inventory.Add(UnequipNoSave(currentEquipment[chosenSlotIndex].item));
                if (currentEquipment[chosenSlotIndex].item.numberOfHands == NumberOfHands.TwoHanded)
                {
                    if (chosenSlotIndex == (int) EquipmentSlot.MainHand)
                    {
                        currentEquipment[chosenSlotIndex + 1] = null;
                    }
                    else if (chosenSlotIndex == (int) EquipmentSlot.OffHand)
                    {
                        currentEquipment[chosenSlotIndex - 1] = null;
                    }
                }
                currentEquipment[chosenSlotIndex] = new InventoryItem(newItem.item, 1, true, chosenSlotIndex);

            }

        }

        save.equipment = currentEquipment;

        foreach (InventoryItem invItem in inventory)
        {
            if (invItem.item.itemName == newItem.item.itemName)
            {
                searchResult = invItem;
            }
        }
        inventory.Remove(searchResult);
        save.inventory = inventory;
        LoadInventory();
    }

    public void Unequip(InventoryItem newItem, SaveObject _save)
    {
        // searchResult = null;
        // foreach (InventoryItem inv in save.equipment)
        // {
        //     if (inv.item == newItem)
        //     {
        //         searchResult = inv;
        //         searchIndex = inv.equipmentSlotIndex;
        //     }
        // }

        searchIndex = newItem.equipmentSlotIndex;
        searchResult = _save.equipment[searchIndex];

        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        save.equipment[searchIndex].equipmentSlotIndex = 0;

        if (save.equipment[searchIndex].item.numberOfHands == NumberOfHands.TwoHanded)
        {
            if (save.equipment[5].item != null)
            {  
                // Debug.Log("main");
                save.equipment[5] = null;

            }
            if (save.equipment[6].item != null)
            {
                // Debug.Log("off");
                save.equipment[6] = null;
            }
        }
        else
        {
            save.equipment[searchIndex] = null;
        }

        if (newItem.item == null)
        { newItem.item = LoadGameMasterHandler.Instance.GetItem(newItem.stringID); }

        InventoryItem _newItem = new InventoryItem
                    (newItem.item, newItem.item.numberInStack, false, 0); 
        save.inventory.Add(_newItem);

        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
        
    }

    private InventoryItem UnequipNoSave(Item newItem)
    {
        bool removed = false;
        SaveObject save = SaveManagerVersion3.FindCurrentSave();

        for (int i=0; i< save.equipment.Length; i++)
        {
            if (save.equipment[i] == null) { continue; }
            if(save.equipment[i].item == newItem && !removed)
            {
                save.equipment[i].equipped = false;
                InventoryItem _newItem = new InventoryItem 
                    (save.equipment[i].item, 1, false, 0);

                    return _newItem;                
            }
        }

        return default;

        // save.equipment[searchIndex].item = null;
    }

    public void Consume(Item newItem, InventoryItem _itemInfo)
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        bool removed = false;

        for (int i=0; i< save.equipment.Length; i++)
        {
            if(save.equipment[i].item == newItem && !removed)
            {
                save.inventory.Remove(_itemInfo);
                // Debug.Log("found and removed");
                removed = true;
            }
        }

        LoadInventory();
    }
    public void LoadInventory()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        inventory = save.inventory;

        foreach (GameObject item in displayedItems)
        {
            Destroy(item.gameObject);
        }
        displayedItems.Clear();

        foreach (InventoryItem item in inventory)
        {
            if (item == null) { continue; }
            if (item.item == null)
            {
                item.item = LoadGameMasterHandler.Instance.GetItem(item.stringID);
            }
            if (item.item.GetItemType().ToString() == tabManager.tabState.ToString() 
                || (item.item.GetItemType().ToString() == ItemType.Scroll.ToString() && tabManager.tabState.ToString() == TabManagerState.Special.ToString())
                || (item.item.GetItemType().ToString() == ItemType.Arrows.ToString() && tabManager.tabState.ToString() == TabManagerState.Weapon.ToString()))
            {
                GameObject _item = Instantiate(inventoryObject, 
                    transform.position, transform.rotation);

                _item.transform.SetParent(parentObject.transform, false);
                displayedItems.Add(_item);

                _item.GetComponent<LocalItemStart>().DisplayItemInfo(item.item, item);
            }
        }

        contentFitter.FitContent(displayedItems.Count);        
        
    }

    public void DisplayInventoryUI()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        int lootCheck = 0;
        foreach (InventoryItem item in save.inventory)
        {
            if (item.item.GetItemType() != ItemType.Special && item.item.GetItemType() != ItemType.Scroll)
            {
                lootCheck += 1;
            }
        }

        foreach (InventoryItem item in save.equipment)
        {
            if (item == null) { continue; }
            if (item.item == null) { continue; }
            if (item.item.GetItemType() != ItemType.Special && item.item.GetItemType() != ItemType.Scroll)
            {
                lootCheck += 1;
            }
        }

        if (save.equipment.Length > 1)
        {
            if (save.equipment[5] != null)
            {
                if (save.equipment[5].item != null)
                {
                    if (save.equipment[5].item.numberOfHands == NumberOfHands.TwoHanded)
                    {
                        // Debug.Log("removing 1");
                        lootCheck -= 1;
                    }
                }

            }    
        }
        
        
        holdingCapacityNumber.text = lootCheck + "/" + save.holdingCapacity;
        itemInfoPanel.SetActive(false);
        equipmentPanel.SetActive(false);

    }

    public void LoadEquipment()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        currentEquipment = save.equipment;

        foreach (GameObject equipInstance in equipmentDisplayItems)
        {
            Destroy(equipInstance);
        }
        equipmentDisplayItems.Clear();

        for (int i=0; i<save.equipment.Length; i++)
        {
            if (save.equipment[i] == null)
            {
                GameObject equipInstance = Instantiate(emptyEquipmentPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform, false);
                equipmentDisplayItems.Add(equipInstance);
                continue;
            }
            else if (save.equipment[i].item == null)
            {
                save.equipment[i].item = LoadGameMasterHandler.Instance.GetItem(save.equipment[i].stringID);

                GameObject equipInstance = Instantiate(equipmentObjectPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform, false);
                equipInstance.GetComponent<LocalItemStart>().DisplayItemInfo(save.equipment[i].item, save.equipment[i]);
                equipmentDisplayItems.Add(equipInstance);
                // GameObject equipInstance = Instantiate(emptyEquipmentPrefab, new Vector2(0,0), transform.rotation);
                // equipInstance.transform.SetParent(parentObjectTransforms[i].transform, false);
                // equipmentDisplayItems.Add(equipInstance);
                // continue;
            }
            else if (save.equipment[i].item.numberOfHands == NumberOfHands.TwoHanded && i == (int)EquipmentSlot.OffHand && !SaveManagerVersion3.DoesPlayerHaveThisAbility(strengthAbility))
            {
                GameObject equipInstance = Instantiate(inUseEquipmentPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform, false);
                equipmentDisplayItems.Add(equipInstance);
            }
            else
            {
                GameObject equipInstance = Instantiate(equipmentObjectPrefab, new Vector2(0,0), transform.rotation);
                equipInstance.transform.SetParent(parentObjectTransforms[i].transform, false);
                equipInstance.GetComponent<LocalItemStart>().DisplayItemInfo(save.equipment[i].item, save.equipment[i]);
                equipmentDisplayItems.Add(equipInstance);
            }
        }
    }

}

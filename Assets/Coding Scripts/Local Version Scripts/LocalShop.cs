using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalShop : MonoBehaviour
{
    ItemDatabase database;
    [SerializeField] TabManager tabManager;
    [SerializeField] ShopContentFitter contentFitter;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject parentObject;
    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TMP_Dropdown locationInputField;

    public bool inShop = false;

    List<GameObject> displayedItems = new List<GameObject>();

    private void Start() 
    {
        itemInfoPanel.SetActive(false);

        if (FindObjectOfType<LocationShopDatabase>() != null)
            database = FindObjectOfType<LocationShopDatabase>().GetItemDatabase();
    }

    public void LoadShop()
    {   
        inShop = true;
        foreach (GameObject item in displayedItems)
        {
            Destroy(item.gameObject);
        }
        displayedItems.Clear();

        if (database == null)
        {
            FindObjectOfType<LocationShopDatabase>().StartUp();
        }

        database = FindObjectOfType<LocationShopDatabase>().GetItemDatabase();

        foreach (Item item in database.allItems)
        {
            if (item.GetItemType().ToString() == tabManager.tabState.ToString() 
                || (item.GetItemType().ToString() == ItemType.Scroll.ToString() && tabManager.tabState.ToString() == TabManagerState.Special.ToString())
                || (item.GetItemType().ToString() == ItemType.Arrows.ToString() && tabManager.tabState.ToString() == TabManagerState.Weapon.ToString()))
            {
                GameObject _item = Instantiate(itemPrefab, transform.position, 
                    transform.rotation);
                _item.transform.SetParent(parentObject.transform, false);

                displayedItems.Add(_item);
                _item.GetComponent<LocalItemStart>().DisplayItemInfo(item, new InventoryItem(item, item.numberInStack, false, 0));
            }
            
        }

        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        goldText.text = save.gold.ToString();

        itemInfoPanel.GetComponent<ItemPanelDisplay>().DeactivateAllButtons();

        contentFitter.FitContent(displayedItems.Count);
    }

    public void CloseShop()
    {
        inShop = false;
    }

}

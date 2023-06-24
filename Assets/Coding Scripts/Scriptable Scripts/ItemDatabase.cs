using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObject/ItemDatabase")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] allItems;
    public Dictionary<Item, int> GetID = new Dictionary<Item, int>();
    // public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();

    public void OnAfterDeserialize()
    {
        //
        Array.Sort(allItems, (x,y) => String.Compare(x.itemName, y.itemName));

    }

    private void OnEnable() 
    {

        GetID = new Dictionary<Item, int>();
        // GetItem = new Dictionary<int, Item>();
        for (int i = 0; i < allItems.Length; i++)
        {
            if (!GetID.ContainsKey(allItems[i]))
            {
                GetID.Add(allItems[i], i);
            }
            // GetItem.Add(i, allItems[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        //
        // GetID = new Dictionary<Item, int>();
    }
}

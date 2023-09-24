using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObject/Database/Item")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] allItems;
    // public Dictionary<Item, int> GetID = new Dictionary<Item, int>();
    public Dictionary<string, Item> GetItem = new Dictionary<string, Item>();

    public void OnAfterDeserialize()
    {
        //
    }

    public void OnEnable() 
    {
        // GetID = new Dictionary<Item, int>();
        GetItem = new Dictionary<string, Item>();
        for (int i = 0; i < allItems.Length; i++)
        {
            if (!GetItem.ContainsKey(allItems[i].itemName))
            {
                GetItem.Add(allItems[i].itemName, allItems[i]);
            }
            // GetItem.Add(i, allItems[i]);
        }

        UnityEditor.EditorUtility.SetDirty(this);

    }

    public void OnBeforeSerialize()
    {
        //
        // GetID = new Dictionary<Item, int>();
    }
}

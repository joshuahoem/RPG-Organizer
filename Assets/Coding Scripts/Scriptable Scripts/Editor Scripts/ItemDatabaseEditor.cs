using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor 
{
    public static string itemDatabaseString = "Item Database";
    public static string abilityDatabaseString = "All Abilities";
    public static string perkDatabaseString = "AllPerks";
    public static string classDatabaseString = "ClassDatabase";
    public static string raceDatabaseString = "RaceDatabase";

    private bool initToggles = false;
    private List<Item> itemsList = new List<Item>();

    public override void OnInspectorGUI() 
    {
        ItemDatabase itemDatabase = (ItemDatabase)target;
        ItemDatabase database = Resources.Load<ItemDatabase>("Item Database");

        DrawDefaultInspector();

        GUILayout.Space(10);

        Array.Sort(itemDatabase.allItems, (x,y) => string.Compare(x.itemName, y.itemName)); 

        if (!initToggles)
        {
            InitTogglesMethod(itemDatabase, database);
            initToggles = true;
        }

        // if (GUILayout.Button("Toggle All Items On"))
        // {
        //     ToggleAllItems(itemDatabase, true);
        // } 

        // if (GUILayout.Button ("Toggle all Items Off"))
        // {
        //     ToggleAllItems(itemDatabase, false);
        // }  

        foreach (Item item in database.allItems)
        {
            EditorGUILayout.BeginHorizontal();
            item.isIncluded = EditorGUILayout.ToggleLeft(item.itemName, item.isIncluded);
            EditorGUILayout.EndHorizontal();

            if (!item.isIncluded) { continue; }
            if (!itemsList.Contains(item)) { itemsList.Add(item); }
        }
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(itemDatabase);
            AssetDatabase.SaveAssets();

            UpdateToggles(itemDatabase);
        }
    }


    private void InitTogglesMethod(ItemDatabase itemDatabase, ItemDatabase database)
    {
        foreach (Item item in database.allItems)
        {
            item.isIncluded = itemDatabase.GetItem.ContainsKey(item.itemName);
        }
    }

    private void UpdateToggles(ItemDatabase itemDatabase)
    {
        foreach (Item item in itemsList)
        {
            if (!itemDatabase.GetItem.ContainsKey(item.itemName))
            {
                Array.Resize(ref itemDatabase.allItems, itemDatabase.allItems.Length + 1);
                itemDatabase.allItems[itemDatabase.allItems.Length - 1] = item;
            }
        }

        itemDatabase.OnEnable();
    }

    private void ToggleAllItems(ItemDatabase itemDatabase, bool included)
    {
        foreach (Item item in itemDatabase.allItems)
        {
            if (item != null)
            {
                item.isIncluded = included;
            }
        }

        EditorUtility.SetDirty(itemDatabase);
    }
}

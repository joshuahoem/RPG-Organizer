using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class ItemChecker : MonoBehaviour
{
    public static string itemDatabaseString = "Item Database";
    [MenuItem("Tools/Check Items")]
    public static void CheckItems()
    {   
        string[] guids = AssetDatabase.FindAssets("t:Item");

        ItemDatabase itemDatabase = Resources.Load<ItemDatabase>("Item Database");

        HashSet<string> databaseItemNames = new HashSet<string>();
        foreach (Item item in itemDatabase.allItems)
        {
            databaseItemNames.Add(item.itemName);
        }

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);

            if (!databaseItemNames.Contains(item.itemName))
            {
                Debug.LogError("Not in: " + item.itemName);
            }
        }
    }
}

#endif

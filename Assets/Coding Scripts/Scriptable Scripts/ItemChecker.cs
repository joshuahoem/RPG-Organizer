using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class ItemChecker : MonoBehaviour
{
    public static string itemDatabaseString = "Item Database";
    public static string abilityDatabaseString = "All Abilities";
    public static string perkDatabaseString = "AllPerks";
    public static string classDatabaseString = "ClassDatabase";
    public static string raceDatabaseString = "RaceDatabase";


    [MenuItem("Tools/Check Items")]
    public static void CheckItems()
    {   
        string[] guids = AssetDatabase.FindAssets("t:Item");

        ItemDatabase itemDatabase = Resources.Load<ItemDatabase>(itemDatabaseString);

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

        Debug.Log("Finished Checking: Items");
    }

    [MenuItem("Tools/Check Abilities")]
    public static void CheckAbilities()
    {   
        string[] guids = AssetDatabase.FindAssets("t:Ability");

        AbilityDatabase abilityDatabase = Resources.Load<AbilityDatabase>(abilityDatabaseString);

        HashSet<string> databaseItemNames = new HashSet<string>();
        foreach (Ability ability in abilityDatabase.allAbilities)
        {
            databaseItemNames.Add(ability.abilityName);
        }

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Ability ability = AssetDatabase.LoadAssetAtPath<Ability>(assetPath);

            if (!databaseItemNames.Contains(ability.abilityName))
            {
                Debug.LogError("Not in: " + ability.abilityName);
            }
        }

        Debug.Log("Finished Checking: Abilities");
    }

    [MenuItem("Tools/Check Perks")]
    public static void CheckPerks()
    {   
        string[] guids = AssetDatabase.FindAssets("t:Perk");

        PerkDatabase perkDatabase = Resources.Load<PerkDatabase>(perkDatabaseString);

        HashSet<string> databaseItemNames = new HashSet<string>();
        foreach (Perk perk in perkDatabase.allPerks)
        {
            databaseItemNames.Add(perk.perkName);
        }

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Perk perk = AssetDatabase.LoadAssetAtPath<Perk>(assetPath);

            if (!databaseItemNames.Contains(perk.perkName))
            {
                Debug.LogError("Not in: " + perk.perkName);
            }
        }

        Debug.Log("Finished Checking: Perks");
    }

    [MenuItem("Tools/Check Classes")]
    public static void CheckClasses()
    {   
        string[] guids = AssetDatabase.FindAssets("t:Class");

        ClassDatabase classDatabase = Resources.Load<ClassDatabase>(classDatabaseString);

        HashSet<string> databaseItemNames = new HashSet<string>();
        foreach (Class _class in classDatabase.allClasses)
        {
            databaseItemNames.Add(_class.name);
        }

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Class _class = AssetDatabase.LoadAssetAtPath<Class>(assetPath);

            if (!databaseItemNames.Contains(_class.name))
            {
                Debug.LogError("Not in: " + _class.name);
            }
        }

        Debug.Log("Finished Checking: Classes");
    }

    [MenuItem("Tools/Check Races")]
    public static void CheckRaces()
    {   
        string[] guids = AssetDatabase.FindAssets("t:Race");

        RaceDatabase raceDatabase = Resources.Load<RaceDatabase>(raceDatabaseString);

        HashSet<string> databaseItemNames = new HashSet<string>();
        foreach (Race race in raceDatabase.allRaces)
        {
            databaseItemNames.Add(race.name);
        }

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Race race = AssetDatabase.LoadAssetAtPath<Race>(assetPath);

            if (!databaseItemNames.Contains(race.name))
            {
                Debug.LogError("Not in: " + race.name);
            }
        }

        Debug.Log("Finished Checking: Races");
    }
}

#endif

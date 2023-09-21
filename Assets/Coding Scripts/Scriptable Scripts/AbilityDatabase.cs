using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "AbilityDatabase", menuName = "ScriptableObject/Database/Ability")]

public class AbilityDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Ability[] allAbilities;
    public Dictionary<string, Ability> GetStringID = new Dictionary<string, Ability>();

    public void OnAfterDeserialize()
    {
        allAbilities = allAbilities.Where(ability => ability != null).ToArray();
        
        Array.Sort(allAbilities, (x,y) => 
        { 
            if (x == null && y == null) { Debug.Log("These are both null: " + x + " " + y); return 0; }
            if (x == null) { Debug.Log("This is null: " + x); return -1; }
            if (y == null) { Debug.Log("This is null: " + y); return -1; }
            return string.Compare(x.abilityName, y.abilityName); 
        });
    }

    private void OnEnable() 
    {
        GetStringID = new Dictionary<string, Ability>();
        for (int i = 0; i < allAbilities.Length; i++)
        {
            if (!GetStringID.ContainsKey(allAbilities[i].abilityName))
            {
                GetStringID.Add(allAbilities[i].abilityName, allAbilities[i]);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        // 
    }
}

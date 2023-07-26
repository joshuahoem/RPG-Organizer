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
        Array.Sort(allAbilities, (x,y) => String.Compare(x.abilityName, y.abilityName));
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

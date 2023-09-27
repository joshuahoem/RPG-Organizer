using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "PerkDatabase", menuName = "ScriptableObject/Database/Perk")]
public class PerkDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Perk[] allPerks;
    public Dictionary<string, Perk> GetStringID = new Dictionary<string, Perk>();

    public void OnAfterDeserialize()
    {
        Array.Sort(allPerks, (x,y) => String.Compare(x.perkName, y.perkName));

    }

    private void OnEnable() 
    {
        GetStringID = new Dictionary<string, Perk>();
        for (int i = 0; i < allPerks.Length; i++)
        {
            if (!GetStringID.ContainsKey(allPerks[i].perkName))
            {
                GetStringID.Add(allPerks[i].perkName, allPerks[i]);
            }
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif

    }

    public void OnBeforeSerialize()
    {
        // 
    }
}

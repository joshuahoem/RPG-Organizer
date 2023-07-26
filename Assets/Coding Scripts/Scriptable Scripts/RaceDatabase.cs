using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "RaceDatabase", menuName = "ScriptableObject/Database/Race")]

public class RaceDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Race[] allRaces;
    public Dictionary<string, Race> GetRaceID = new Dictionary<string, Race>();

    public void OnAfterDeserialize()
    {
        Array.Sort(allRaces, (x,y) => String.Compare(x.name, y.name));
    }

    private void OnEnable() 
    {
        GetRaceID = new Dictionary<string, Race>();
        for (int i = 0; i < allRaces.Length; i++)
        {
            if (!GetRaceID.ContainsKey(allRaces[i].name))
            {
                GetRaceID.Add(allRaces[i].name, allRaces[i]);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        // 
    }
}

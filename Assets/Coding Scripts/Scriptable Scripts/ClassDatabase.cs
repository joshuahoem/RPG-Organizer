using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "ClassDatabase", menuName = "ScriptableObject/Database/Class")]

public class ClassDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Class[] allClasses;
    public Dictionary<string, Class> GetClassID = new Dictionary<string, Class>();

    public void OnAfterDeserialize()
    {
        Array.Sort(allClasses, (x,y) => String.Compare(x.name, y.name));

    }

    private void OnEnable() 
    {
        GetClassID = new Dictionary<string, Class>();
        for (int i = 0; i < allClasses.Length; i++)
        {
            if (!GetClassID.ContainsKey(allClasses[i].name))
            {
                GetClassID.Add(allClasses[i].name, allClasses[i]);
            }
        }

        UnityEditor.EditorUtility.SetDirty(this);

    }

    public void OnBeforeSerialize()
    {
        // 
    }
}

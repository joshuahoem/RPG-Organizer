using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Save();
        }
    }

    public void Test()
    {
        Debug.Log("Running");
        foreach (var kvp in CharacterRegistry.Instance.GetDictionary())
        {
            Debug.Log("here");
            Debug.Log(kvp.Value.nameOfCharacter);
            Debug.Log(kvp.Value.characterID);

        }
    }

    public void Save()
    {
        Debug.Log("Saved");
        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
    }
}

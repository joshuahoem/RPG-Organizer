using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
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
        foreach (var kvp in CharacterRegistry.Instance.GetDictionary())
        {
            Debug.Log(kvp.Value.nameOfCharacter);

        }
    }

    public void Save()
    {
        Debug.Log("Saved");
        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
    }
}

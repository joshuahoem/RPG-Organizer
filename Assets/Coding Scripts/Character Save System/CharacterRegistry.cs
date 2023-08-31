using System.Collections.Generic;
using UnityEngine;

public class CharacterRegistry : MonoBehaviour
{
    public static CharacterRegistry Instance;
    private Dictionary<string, SaveObject> characterDictionary = new Dictionary<string, SaveObject>();

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Start()
    {
        SaveManagerVersion3.LoadGame(this);
    }

    public void AddCharacter(SaveObject save)
    {
        characterDictionary.Add(save.characterID, save);
    }

    public void RemoveCharacter(SaveObject save)
    {
        characterDictionary.Remove(save.characterID);
    }

    public SaveObject GetCharacter(string _characterID)
    {
        if (characterDictionary.TryGetValue(_characterID, out SaveObject save))
        {
            return save;
        }
        else
        {
            return null;
        }
    }

    public Dictionary<string, SaveObject> GetDictionary()
    {
        return characterDictionary;
    }

    public void ClearCharacters()
    {
        characterDictionary.Clear();
    }

    private void OnDisable()
    {
        //Debug.Log("game disable save");
        //SaveManagerVersion3.SaveGame(this);
    }

    private void OnApplicationPause()
    {
        //Debug.Log("game pause save");
        //SaveManagerVersion3.SaveGame(this);

    }
}

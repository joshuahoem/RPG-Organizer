using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class SaveManagerVersion3
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath;

    public static void SaveGame(CharacterRegistry registry)
    {
        Dictionary<string, SaveObject> characterDictionary = new Dictionary<string, SaveObject>(registry.GetDictionary());

        string jsonData = JsonConvert.SerializeObject(characterDictionary);
        string savePath = Path.Combine(SAVE_FOLDER, "Saves", "Registry.txt");
        File.WriteAllText(savePath, jsonData);
    }

    public static void LoadGame(CharacterRegistry registry)
    {
        string savePath = Path.Combine(SAVE_FOLDER, "Saves", "Registry.txt");
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            Dictionary<string, SaveObject> characterDictionary = JsonConvert.DeserializeObject<Dictionary<string, SaveObject>>(jsonData);

            registry.ClearCharacters();

            foreach (KeyValuePair<string, SaveObject> kvp in characterDictionary)
            {
                registry.AddCharacter(kvp.Value);
            }
        }
    }

    public static Sprite LoadSprite(string path)
    {
        byte[] imageData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        bool success = tex.LoadImage(imageData);

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }
}

[System.Serializable]
public class CharacterDictionary
{
    public Dictionary<string, SaveObject> _characterDictionary;

    public IEnumerable<KeyValuePair<string, SaveObject>> Characters
    {
        get { return _characterDictionary;  }
        set { }
    }
}

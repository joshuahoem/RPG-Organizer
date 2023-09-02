using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor;

[CreateAssetMenu(fileName = "New Class", menuName = "ScriptableObject/Class")]
public class Class : ScriptableObject
{
    public new string name;
    public string description;
    [JsonIgnore] public Sprite picture;
    [JsonIgnore] public Color imageColor;
    
    public int health;
    public int stamina;
    public int magic;
    public int speed;
    public int strength;
    public int intelligence;

    public string pathToPicture;

    [SerializeField] public List<Perk> startingPerks = new List<Perk>();

    public void OnEnable()
    {
        if (picture != null)
        {
            pathToPicture = AssetDatabase.GetAssetPath(picture);
        }
        // else
        // {
        //     Debug.Log(name);
        //     byte[] imageData = File.ReadAllBytes(pathToPicture);
        //     Texture2D tex = new Texture2D(2, 2);
        //     bool success = tex.LoadImage(imageData);
        //     Debug.Log(success + " was successful or not");

        //     picture = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        
        // }
    }

}
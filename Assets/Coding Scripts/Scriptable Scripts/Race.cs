using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor;

[CreateAssetMenu(fileName = "New Race", menuName = "ScriptableObject/Race")]
public class Race : ScriptableObject
{
    public new string name;
    [JsonIgnore] public Sprite picture;
    public string history;
    [JsonIgnore] public Color imageColor;
    
    public int health;
    public int stamina;
    public int magic;
    public int intelligence;
    public int strength;
    public int speed;

    public string pathToPicture;

    [JsonIgnore] [SerializeField] public List<Perk> startingPerks = new List<Perk>();

    
    public void OnEnable()
    {
        if (picture != null)
        {
            pathToPicture = AssetDatabase.GetAssetPath(picture);
        }
    }
}

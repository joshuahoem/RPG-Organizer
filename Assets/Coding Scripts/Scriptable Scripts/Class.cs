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

    [JsonIgnore] [SerializeField] public List<Perk> startingPerks = new List<Perk>();

    // public void OnEnable()
    // {
    //     if (picture != null)
    //     {
    //         #if UNITY_EDITOR
    //         pathToPicture = AssetDatabase.GetAssetPath(picture);
    //         #endif
    //     }
    // }

}
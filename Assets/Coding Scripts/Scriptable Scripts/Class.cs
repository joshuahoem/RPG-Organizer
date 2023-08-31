using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "New Class", menuName = "ScriptableObject/Class")]
public class Class : ScriptableObject
{
    public new string name;
    public string description;
    [JsonIgnore] public Sprite logo;
    [JsonIgnore] public Color imageColor;
    
    public int health;
    public int stamina;
    public int magic;
    public int speed;
    public int strength;
    public int intelligence;

    public string pathToPicture;

    [SerializeField] public List<Perk> startingPerks = new List<Perk>();

}
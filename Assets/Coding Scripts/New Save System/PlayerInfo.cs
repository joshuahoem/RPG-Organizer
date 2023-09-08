using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInfo
{
    public List<UnlockObject> unlocks = new List<UnlockObject>(); 
}

[System.Serializable] public class UnlockObject
{
    [JsonIgnore] public Race unlockedRace;
    public string raceStringID;
    [JsonIgnore] public Class unlockedClass;
    public string classStringID;

    public UnlockObject(Race _unlockRace, Class _unlockClass, string _raceStringID, string _classStringID)
    {
        unlockedRace = _unlockRace;
        unlockedClass = _unlockClass;
        raceStringID = _raceStringID;
        classStringID = _classStringID;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public List<UnlockObject> unlocks = new List<UnlockObject>(); 
}

[System.Serializable] public class UnlockObject
{
    public Race unlockedRace;
    public string raceStringID;
    public Class unlockedClass;
    public string classStringID;

    public UnlockObject(Race _unlockRace, Class _unlockClass, string _raceStringID, string _classStringID)
    {
        unlockedRace = _unlockRace;
        unlockedClass = _unlockClass;
        raceStringID = _raceStringID;
        classStringID = _classStringID;
    }
}

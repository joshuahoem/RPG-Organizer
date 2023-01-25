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
    public Class unlockedClass;

    public UnlockObject(Race _unlockRace, Class _unlockClass)
    {
        unlockedRace = _unlockRace;
        unlockedClass = _unlockClass;
    }
}

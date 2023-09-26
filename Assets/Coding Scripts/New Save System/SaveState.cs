using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState
{
    public int numberOfCharacters;
    public int diceLastRolled;
    public string fileIndexString;
    public bool raceAbilityBool;
    public bool classAbilityBool;
    public bool learnedAbilityBool;
    public ScreenState screenState;
}

public enum ScreenState
{
    CharacterInfo,
    AbilityScreen
}

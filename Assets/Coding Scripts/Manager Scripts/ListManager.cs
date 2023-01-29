using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListManager : MonoBehaviour
{
    [SerializeField] bool raceSelect = false;
    [SerializeField] bool classSelect = false;


    void Start()
    {

        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();

        dropdown.options.Clear();

        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();

        foreach (UnlockObject unlock in playerInfo.unlocks)
        {
            if (raceSelect && unlock.unlockedRace != null)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = unlock.unlockedRace.name});
            }
            else if (classSelect && unlock.unlockedClass != null)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = unlock.unlockedClass.name});
            }
        }

        dropdown.RefreshShownValue();
        dropdown.SetValueWithoutNotify(0);
    }

}

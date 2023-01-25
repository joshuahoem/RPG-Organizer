using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlockItemInfo : MonoBehaviour
{
    [SerializeField] public Race raceToUnlock;
    [SerializeField] public Class classToUnlock;

    [SerializeField] GameObject lockImage;
    [SerializeField] Image characterImage;
    [SerializeField] Image bgOject;
    [SerializeField] Color bgColor;



    public void DisplayUnlocked()
    {
        lockImage.SetActive(false);
        if (raceToUnlock != null)
        {
            characterImage.sprite = raceToUnlock.picture;
        }

        bgOject.color = bgColor;
    }

}

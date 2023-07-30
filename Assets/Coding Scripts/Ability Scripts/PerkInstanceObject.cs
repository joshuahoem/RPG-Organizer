using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkInstanceObject : MonoBehaviour
{
    [Header("Perk Info")]
    [SerializeField] public Perk perk;
    public PerkObject perkObject;

    [SerializeField] Image perkImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] public Image borderImage;
    [SerializeField] TextMeshProUGUI perkNameTMP;
    [SerializeField] public TextMeshProUGUI perkCountTMP;
  
    public void DisplayPerk(PerkObject _perkObject)
    {
        perk = _perkObject.perk;
        perkObject = _perkObject;

        if (perk.perkImageIcon != null)
        {
            perkImage.sprite = perk.perkImageIcon;
        }
        borderImage.color = perk.borderColor;

        perkNameTMP.text = perk.perkName;
        perkCountTMP.text = perkObject.count.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityCardInstance : MonoBehaviour
{
    Ability abilty;
    AbilitySaveObject abilitySO;
    [SerializeField] Image abilityImage;
    [SerializeField] Image borderImage;
    [SerializeField] TextMeshProUGUI abilityNameTMP;

    public void DisplayInfo(AbilitySaveObject _abilitySO)
    {
        abilitySO = _abilitySO;

        // Debug.Log(_abilitySO);
        // Debug.Log(_abilitySO.ability);

        abilityImage.sprite = abilitySO.ability.picture;
        borderImage.color = abilitySO.ability.borderColor;
        abilityNameTMP.text = abilitySO.ability.abilityName;
    }

    public void ClickedButton()
    {
        FindObjectOfType<AbilityManager>().LoadAbilityPanel(abilitySO);
    }
}

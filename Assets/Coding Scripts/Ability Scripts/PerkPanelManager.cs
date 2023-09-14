using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PerkPanelManager : MonoBehaviour
{
    public event EventHandler<UnlockPerkEventArgs> onPerkUnlocked;
    [SerializeField] private ErrorMessageHandler errorMessageHandler;

    [SerializeField] public GameObject perkPanelObject;
    Perk perk;
    PerkObject perkObjectFromItem;

    #region Display Items
    [SerializeField] Image perkImageIcon;
    [SerializeField] Image borderIcon;

    [SerializeField] TextMeshProUGUI perkNameTMP;
    [SerializeField] TextMeshProUGUI bonusHealthTMP;
    [SerializeField] TextMeshProUGUI bonusStaminaTMP;
    [SerializeField] TextMeshProUGUI bonusMagicTMP;
    [SerializeField] TextMeshProUGUI bonusStrengthTMP;
    [SerializeField] TextMeshProUGUI bonusIntelligenceTMP;
    [SerializeField] TextMeshProUGUI bonusSpeedTMP;

    [SerializeField] TextMeshProUGUI unlockCostTMP;
    [SerializeField] TextMeshProUGUI perkCountTMP;
    [SerializeField] TextMeshProUGUI descriptionTMP;


    [SerializeField] GameObject perkButtonUnlock;
    #endregion

    private void Start() {
        perkPanelObject.SetActive(false);
    }

    public void DisplayPerkPanel(PerkObject _perkObject, bool canUnlock)
    {
        perk = _perkObject.perk;
        perkObjectFromItem = _perkObject;

        if (perkObjectFromItem.unlockedBool)
        {
            perkButtonUnlock.SetActive(false);
        }
        else
        {
            perkButtonUnlock.SetActive(true);
        }

        if (!canUnlock)
        {
            perkButtonUnlock.SetActive(false);
        }

        // perkImageIcon.sprite = SaveManagerVersion3.LoadSprite(perk.pathToPicture);
        perkImageIcon.sprite = perk.picture;
        borderIcon.color = perk.borderColor;

        perkNameTMP.text = perk.perkName;
        bonusHealthTMP.text = perk.bonusHealth.ToString();
        bonusStaminaTMP.text = perk.bonusStamina.ToString();
        bonusMagicTMP.text = perk.bonusMagic.ToString();
        bonusStrengthTMP.text = perk.bonusStrength.ToString();
        bonusIntelligenceTMP.text = perk.bonusIntelligence.ToString();
        bonusSpeedTMP.text = perk.bonusSpeed.ToString();

        unlockCostTMP.text = perk.unlockCost.ToString();

        if (perkCountTMP != null)
        {
            perkCountTMP.text = _perkObject.count.ToString();
        }

        descriptionTMP.text = _perkObject.perk.description;
    }

    public void UnlockPerk()
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        SaveState state = SaveManagerVersion3.FindSaveState();
        PerkObject foundPerkObject;

        if (state.raceAbilityBool && save.raceAbilityPoints < perk.unlockCost)
        {
            // Debug.Log("not enough points"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoRacePoints);
            return;
        }
        if (state.classAbilityBool && save.classAbilityPoints < perk.unlockCost)
        {
            // Debug.Log("not enough points"); //error
            errorMessageHandler.ReceivingOnErrorOccured(ErrorMessageHandler.ErrorType.NoClassPoints);
            return;
        }


        foreach (PerkObject _perkObject in save.perks)
        {
            if (_perkObject.ID == perkObjectFromItem.ID && _perkObject.perk.perkName == perkObjectFromItem.perk.perkName)
            {
                _perkObject.count++;
                foundPerkObject = _perkObject;
                return;
            }
        }

        PerkObject perkObject = new PerkObject(perk.perkName, perk, 1, true, perkObjectFromItem.ID);
        foundPerkObject = perkObject;
        save.perks.Add(perkObject);

        // Debug.Log("class " + state.classAbilityBool);
        // Debug.Log("race " + state.raceAbilityBool);

        if (state.classAbilityBool)
            { 
                // Debug.Log(save.classAbilityPoints);
                save.classAbilityPoints -= perk.unlockCost; 
                // Debug.Log(save.classAbilityPoints);
            }
        else if (state.raceAbilityBool)
            { save.raceAbilityPoints -= perk.unlockCost; }

        onPerkUnlocked?.Invoke(this, new UnlockPerkEventArgs { eventPerkObject = foundPerkObject });

        perkButtonUnlock.SetActive(false);
    }

    public class UnlockPerkEventArgs : EventArgs 
    {
        public PerkObject eventPerkObject;
    }
}

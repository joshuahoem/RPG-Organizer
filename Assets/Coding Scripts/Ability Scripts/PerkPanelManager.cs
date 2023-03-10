using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PerkPanelManager : MonoBehaviour
{
    public event EventHandler<UnlockPerkEventArgs> onPerkUnlocked;
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

    [SerializeField] GameObject perkButtonUnlock;
    #endregion

    private void Start() {
        perkPanelObject.SetActive(false);
    }

    public void DisplayPerkPanel(PerkObject _perkObject)
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

        perkImageIcon.sprite = perk.perkImageIcon;
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
    }

    public void UnlockPerk()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();
        PerkObject foundPerkObject;
        foreach (PerkObject _perkObject in save.perks)
        {
            if (_perkObject.ID == perkObjectFromItem.ID)
            {
                _perkObject.count++;
                NewSaveSystem.SaveChanges(save);
                foundPerkObject = _perkObject;
                return;
            }
        }

        PerkObject perkObject = new PerkObject(perk, 1, true, perkObjectFromItem.ID);
        foundPerkObject = perkObject;
        save.perks.Add(perkObject);
        NewSaveSystem.SaveChanges(save);
        onPerkUnlocked?.Invoke(this, new UnlockPerkEventArgs { eventPerkObject = foundPerkObject });

        perkButtonUnlock.SetActive(false);
    }

    public class UnlockPerkEventArgs : EventArgs 
    {
        public PerkObject eventPerkObject;
    }
}

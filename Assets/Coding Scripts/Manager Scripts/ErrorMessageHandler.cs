using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorMessageHandler : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorMessageTMP;

    [SerializeField] [TextArea(5,10)] string errorNotStrongEnough, errorNotSmartEnough, 
        errorNotEnoughGold, errorNotEnoughClassPoints, errorNotEnoughRacePoints, errorNoMagic,
        errorNoStamina, errorAbilityNoHealth, errorAbilityNoStamina, errorAbilityNoMagic,
        errorAbilityNoStrength, errorAbilityNoIntelligence, errorAbilityNoSpeed, errorTooHeavy;

    public enum ErrorType
    {
        NoGold,
        NoStrength,
        NoIntelligence,
        NoRacePoints,
        NoClassPoints,
        NoMagic,
        NoStamina,
        AbilityNoHealth,
        AbilityNoStamina,
        AbilityNoMagic,
        AbilityNoStrength,
        AbilityNoIntelligence,
        AbilityNoSpeed,
        TooHeavy
    }

    private void Start() {
        errorPanel.SetActive(false);
    }

    public void ReceivingOnErrorOccured(ErrorType _errorType)
    {
        errorPanel.SetActive(true);
        switch (_errorType)
        {
            case ErrorType.NoGold:
                errorMessageTMP.text = errorNotEnoughGold;
                break;
            case ErrorType.NoStrength:
                errorMessageTMP.text = errorNotStrongEnough;
                break;
            case ErrorType.NoIntelligence:
                errorMessageTMP.text = errorNotSmartEnough;
                break;
            case ErrorType.NoRacePoints:
                errorMessageTMP.text = errorNotEnoughRacePoints;
                break;
            case ErrorType.NoClassPoints:
                errorMessageTMP.text = errorNotEnoughClassPoints;
                break;
            case ErrorType.NoMagic:
                errorMessageTMP.text = errorNoMagic;
                break;
            case ErrorType.NoStamina:
                errorMessageTMP.text = errorNoStamina;
                break;
            case ErrorType.AbilityNoHealth:
                errorMessageTMP.text = errorAbilityNoHealth;
                break;
            case ErrorType.AbilityNoStamina:
                errorMessageTMP.text = errorAbilityNoStamina;
                break;
            case ErrorType.AbilityNoMagic:
                errorMessageTMP.text = errorAbilityNoMagic;
                break;
            case ErrorType.AbilityNoStrength:
                errorMessageTMP.text = errorAbilityNoStrength;
                break;
            case ErrorType.AbilityNoIntelligence:
                errorMessageTMP.text = errorAbilityNoIntelligence;
                break;
            case ErrorType.AbilityNoSpeed:
                errorMessageTMP.text = errorAbilityNoSpeed;
                break;
            case ErrorType.TooHeavy:
                errorMessageTMP.text = errorTooHeavy;
                break;
        }
    }

    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }
}

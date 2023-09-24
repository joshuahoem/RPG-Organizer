using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI buttonText;
    private Vector3 originalPos;
    [SerializeField] private Vector3 pressedPos;

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            originalPos = buttonText.rectTransform.localPosition;
        }
        else
        {
            Debug.LogError("button text does not exist for: " + gameObject.name);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.rectTransform.localPosition = pressedPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.rectTransform.localPosition = originalPos;
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI buttonText;
    private Image imageToMove;
    private Vector3 originalPos;
    private Button button;
    [SerializeField] private Vector3 pressedPos;

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();

        if (buttonText == null)
        {
            imageToMove = GetComponent<Image>();
        }

        if (imageToMove == null)
        {
            imageToMove = GetComponentInChildren<Image>();
        }

        if (buttonText != null)
        {
            originalPos = buttonText.rectTransform.localPosition;
        }
        else if (imageToMove != null)
        {
            originalPos = imageToMove.rectTransform.localPosition;
        }
        else
        {
            Debug.LogError("button text does not exist for: " + gameObject.name);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button != null) { if (button.interactable == false) { return; } }

        if (buttonText != null)
        {
            buttonText.rectTransform.localPosition = originalPos + pressedPos;
        }
        else if (imageToMove != null)
        {
            imageToMove.rectTransform.localPosition = originalPos + pressedPos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button != null) { if (button.interactable == false) { return; } }

        if (buttonText != null)
        {
            buttonText.rectTransform.localPosition = originalPos;
        }
        else if (imageToMove != null)
        {
            imageToMove.rectTransform.localPosition = originalPos;
        }
    }

}

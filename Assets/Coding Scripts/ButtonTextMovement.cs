using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform[] rects;
    private Vector3[] originalPos;
    private Button button;
    [SerializeField] private Vector3 pressedPos;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        rects = gameObject.GetComponentsInChildren<RectTransform>();
        originalPos = new Vector3[rects.Length];

        for (int i = 0; i < rects.Length; i++)
        {
            originalPos[i] = rects[i].localPosition;
        }      
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button != null) { if (button.interactable == false) { return; } }

        for (int i= 0; i < rects.Length; i++)
        {
            if (rects[i] == gameObject.GetComponent<RectTransform>()) { continue; }
            rects[i].localPosition = originalPos[i] + pressedPos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button != null) { if (button.interactable == false) { return; } }

        for (int i= 0; i < rects.Length; i++)
        {
            if (rects[i] == gameObject.GetComponent<RectTransform>()) { continue; }
            rects[i].localPosition = originalPos[i];
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformHandler : MonoBehaviour
{
    [SerializeField] RectTransform startingFocusObject;
    [SerializeField] Canvas canvas;
    [SerializeField] float bufferX;
    [SerializeField] float bufferY;

    void Start()
    {
        CheckForChanges();
        SetPosition();
    }

    public void CheckForChanges() 
    {
        RectTransform children = transform.GetComponentInChildren<RectTransform>();

        float min_x, max_x, min_y, max_y;
        min_x = max_x = 0;
        min_y = max_y = 0;

        foreach (RectTransform child in children) {
            Vector2 scale = child.sizeDelta;
            float temp_min_x, temp_max_x, temp_min_y, temp_max_y;

            temp_min_x = child.localPosition.x;
            temp_max_x = child.localPosition.x;
            temp_min_y = child.localPosition.y;
            temp_max_y = child.localPosition.y;

            if (temp_min_x < min_x)
                min_x = temp_min_x;
            if (temp_max_x > max_x)
                max_x = temp_max_x;

            if (temp_min_y < min_y)
                min_y = temp_min_y;
            if (temp_max_y > max_y)
                max_y = temp_max_y;
        }

        float width = canvas.GetComponent<RectTransform>().rect.width;
        float height = canvas.GetComponent<RectTransform>().rect.height;
        Debug.Log(min_x);
        Debug.Log(max_x);
        Debug.Log( min_y);
        Debug.Log( max_y);
        float newX = max_x - min_x + bufferX;
        float newY = max_y - min_y + bufferY;

        if (newX < width) { newX = width; Debug.Log("width");}
        if (newY < height) { newY = height; Debug.Log("height");}

        GetComponent<RectTransform>().sizeDelta = new Vector2(newX, newY);
    }

    private void SetPosition()
    {
        if (startingFocusObject != null)
        {
            GetComponent<RectTransform>().localPosition = 
                new Vector2(-startingFocusObject.localPosition.x, -startingFocusObject.localPosition.y);
        }
    }
}
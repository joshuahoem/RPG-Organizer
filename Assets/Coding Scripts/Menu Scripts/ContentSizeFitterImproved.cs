using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSizeFitterImproved : MonoBehaviour
{
    private void Start() 
    {
        GameObject contentToBeSized = this.gameObject;
        GameObject[] children = new GameObject[contentToBeSized.transform.childCount];
        float height = 0;

        for (int i=0; i < children.Length; i++)
        {
            children[i] = contentToBeSized.transform.GetChild(i).gameObject;

            if (children[i] != null)
            {
                height += children[i].GetComponent<RectTransform>().sizeDelta.y;
            }
        }

        float currentX = contentToBeSized.GetComponent<RectTransform>().sizeDelta.x;
        contentToBeSized.GetComponent<RectTransform>().sizeDelta = new Vector2(currentX, height);
    }

}

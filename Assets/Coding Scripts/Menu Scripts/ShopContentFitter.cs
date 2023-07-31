using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContentFitter : MonoBehaviour
{
    [SerializeField] public GameObject GOParent;
    [SerializeField] int minItemsWithoutChange;
    [SerializeField] float sizePerItem;
    [SerializeField] float constantSizeVariable;

    public void FitContent(int numberOfItems)
    {
        GameObject contentToBeSized = this.gameObject;

        float height = numberOfItems * sizePerItem + constantSizeVariable;

        if(numberOfItems <= minItemsWithoutChange)
        {
            // Debug.Log("min size");
            contentToBeSized.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            contentToBeSized.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
        else
        {
            // Debug.Log("changing size");
            contentToBeSized.GetComponent<RectTransform>().offsetMin = new Vector2(0, -height);
            contentToBeSized.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
        
    }

}

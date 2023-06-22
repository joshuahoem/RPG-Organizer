using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContentFitter : MonoBehaviour
{
    [SerializeField] GameObject parent;

    private void Start() {
        FitContent();
    }

    public void FitContent()
    {
        Debug.Log("fitting content...");
        GameObject contentToBeSized = this.gameObject;
        GameObject[] children = new GameObject[parent.transform.childCount];
        float height = 0;

        for (int i=0; i < children.Length; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;

            if (children[i] != null)
            {
                height += children[i].GetComponent<RectTransform>().sizeDelta.y;
            }
        }

        Debug.Log(height + " height of content");

        contentToBeSized.GetComponent<RectTransform>().offsetMin = new Vector2(0, -height);
        contentToBeSized.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        
    }
}

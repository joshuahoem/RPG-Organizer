using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LootDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameTMP;
    [SerializeField] TextMeshProUGUI amountTMP;
    [SerializeField] Image objectImage;

    [SerializeField] Vector2 scale;
    public Item item;
    int amount;

    public void DisplayLoot(Item _item)
    {
        item = _item;
        amount = 1;
        itemNameTMP.text = _item.name;
        amountTMP.text = amount.ToString();
        objectImage.sprite = _item.imageSprite;
        GetComponent<RectTransform>().localScale = scale;
    }

    public void UpdateLootDisplay()
    {
        Debug.Log("update");
        amount++;
        amountTMP.text = amount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LootDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI itemNameTMP;
    [SerializeField] public TextMeshProUGUI amountTMP;
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
        // objectImage.sprite = SaveManagerVersion3.LoadSprite(_item.pathToPicture);
        objectImage.sprite = _item.picture;
        GetComponent<RectTransform>().localScale = scale;
    }

    public void UpdateLootDisplay()
    {
        amount++;
        amountTMP.text = amount.ToString();
    }

    public void DisplayGoldLoot(int gold, Sprite goldSprite)
    {
        if (gold < 1)
        {
            gameObject.SetActive(false);
        }
        itemNameTMP.text = "Gold";
        amountTMP.text = gold.ToString();
        objectImage.sprite = goldSprite;
        GetComponent<RectTransform>().localScale = scale;
    }
}

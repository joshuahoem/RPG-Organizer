using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Color commonColor;
    [SerializeField] Color uncommonColor;
    [SerializeField] Color rareColor;
    [SerializeField] Color epicColor;
    [SerializeField] Color legendaryColor;

    public Color GetRarityColor(ItemRarity rarity)
    {
        Color colorToReturn = new Color(0,0,0,1);
        switch (rarity)
        {
            case ItemRarity.PeasantCraft:
                colorToReturn = commonColor;
                break;
            case ItemRarity.ApprenticeForged:
                colorToReturn = uncommonColor;
                break;
            case ItemRarity.ImperialForged:
                colorToReturn = rareColor;
                break;
            case ItemRarity.ArtisanCraft:
                colorToReturn = epicColor;
                break;
            case ItemRarity.MasterForged:
                colorToReturn = legendaryColor;
                break;
        }

        return colorToReturn;
    }
}

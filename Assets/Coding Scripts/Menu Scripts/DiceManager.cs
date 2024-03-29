using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] TMP_InputField diceNumberTMP;
    [SerializeField] GameObject diceObjectPrefab;
    [SerializeField] Transform diceParentTransform;
    [SerializeField] GameObject contentArea;
    [SerializeField] GameObject diceRollAreaObject;
    [SerializeField] GameObject buttonRollDice;


    [Header("Tuning Numbers")]
    [SerializeField] float startingHeight;
    [SerializeField] float growthInHeight;
    [SerializeField] int objectCountBeforeGrowth;
    [SerializeField] float contentGrowthInHeight;
    [SerializeField] float contentStartingHeight;
    [SerializeField] int contentObjectCountBeforeGrowth;
    [SerializeField] int contentGrowthEveryXObjects;
    [SerializeField] private float diceTimeToWait;
    [SerializeField] Color interactableColor;
    [SerializeField] Color disabledColor;
    [SerializeField] Color textColor;
    [SerializeField] Color disabledTextColor;


    private List<GameObject> diceObjectsList = new List<GameObject>(); 

    private void Start() 
    {
        SaveState state = SaveManagerVersion3.FindSaveState();    

        if (state.diceLastRolled > 0)
        {
            diceNumberTMP.text = state.diceLastRolled.ToString();
            SpawnDice();
        }
    }


    public void SpawnDice()
    {
        foreach (GameObject dice in diceObjectsList)
        {
            Destroy(dice);
        }
        diceObjectsList.Clear();

        if (diceNumberTMP.text == String.Empty) { return; }
        int diceNumber = Convert.ToInt32(diceNumberTMP.text);
        
        for ( int i=0; i < diceNumber; i++ )
        {
            GameObject diceTempObject = Instantiate(diceObjectPrefab, transform.position, Quaternion.identity);
            diceTempObject.transform.SetParent(diceParentTransform, false);
            diceObjectsList.Add(diceTempObject);
        }

        SaveState state = SaveManagerVersion3.FindSaveState();    
        state.diceLastRolled = diceNumber;
        SaveManagerVersion3.SaveStateOfGame(state);

        ChangeRollArea();
        ChangeContentArea();
    }

    private void ChangeRollArea()
    {
        int listCount = diceObjectsList.Count - objectCountBeforeGrowth;

        float currentHeight = startingHeight;
        float currentWidth = diceRollAreaObject.GetComponent<RectTransform>().sizeDelta.x;

        while (listCount > 0)
        {
            currentHeight += growthInHeight;
            listCount -= objectCountBeforeGrowth;
        }

        diceRollAreaObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currentWidth, currentHeight);

    }

    private void ChangeContentArea()
    {
        int listCount = diceObjectsList.Count - contentObjectCountBeforeGrowth;

        float startingX = contentArea.GetComponent<RectTransform>().sizeDelta.x;
        float startingY = contentStartingHeight;

        while(listCount > 0)
        {
            startingY += contentGrowthInHeight;
            listCount -= contentGrowthEveryXObjects;
        }

        contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2 (startingX, startingY);

    }

    public void RollDice()
    {
        buttonRollDice.GetComponent<Button>().interactable = false;
        buttonRollDice.GetComponent<Image>().color = disabledColor;
        buttonRollDice.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = disabledTextColor;

        StartCoroutine(DiceTimer());
        int attack = 0;
        int defense = 0;
        foreach (GameObject dice in diceObjectsList)
        {
            if (dice.GetComponent<DiceRoll>().RollDice())
            {
                attack++;
            }
            else
            {
                defense++;
            }
        }

    }

    IEnumerator DiceTimer()
    {
        yield return new WaitForSeconds(diceTimeToWait);
        buttonRollDice.GetComponent<Button>().interactable = true;
        buttonRollDice.GetComponent<Image>().color = interactableColor;
        buttonRollDice.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = textColor;

    }
    
}

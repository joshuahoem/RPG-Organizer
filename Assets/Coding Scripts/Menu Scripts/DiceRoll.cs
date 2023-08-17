using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject attackSide;
    [SerializeField] private GameObject defenseSide; 
    [SerializeField] private float rollTime; 
    [SerializeField] private float timeBetweenDiceFaceChange; 

    bool rolling = false;
    float timer;
    int value;

    public bool RollDice()
    {
        rolling = true;
        timer = 0;
        StartCoroutine(RollDiceCoroutine());

        value = Random.Range(1,7);

        if (value < 5) { return true; } 
        else { return false; }
    }

    IEnumerator RollDiceCoroutine()
    {
        int randStart = Random.Range(1,3);
        if (randStart == 1)
        {
            attackSide.SetActive(true);
            defenseSide.SetActive(false);
        }
        else
        {
            attackSide.SetActive(false);
            defenseSide.SetActive(true);
        }

        yield return new WaitForSeconds(rollTime);

        rolling = false;
        if (value < 5)
        {
            //rolled a 1,2,3,4 - attack
            attackSide.SetActive(true);
            defenseSide.SetActive(false);
        }
        else
        {
            //rolled a 5,6 - defense
            attackSide.SetActive(false);
            defenseSide.SetActive(true);
        }
    }

    private void Update() 
    {
        if (rolling == false) { return; }

        timer += Time.deltaTime;

        if ( timer <= timeBetweenDiceFaceChange) { return; }

        if (defenseSide.activeSelf)
        {
            attackSide.SetActive(true);
            defenseSide.SetActive(false);
        }
        else
        {
            attackSide.SetActive(false);
            defenseSide.SetActive(true);
        }

        timer = 0;
    }

}

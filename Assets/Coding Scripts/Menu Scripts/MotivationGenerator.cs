using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotivationGenerator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI motivationTMP;
    [TextArea(5,10)] [SerializeField] private string[] motivationReasons;

    public void GenerateMotivationReason()
    {
        int count = motivationReasons.Length;
        int randomInt = Random.Range(0,count);

        motivationTMP.text = motivationReasons[randomInt];
    }

}

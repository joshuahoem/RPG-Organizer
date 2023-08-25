using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Singleton : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Awake() 
    {    
        int numSingleton = FindObjectsOfType<Singleton>().Length;
        if (numSingleton>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

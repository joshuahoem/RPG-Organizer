using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInfo : MonoBehaviour
{
    [SerializeField] public float globalScale;
    [SerializeField] public float masterWidth;
    [SerializeField] public GameObject arrowObject;

    private void Awake() {
        globalScale = (globalScale/masterWidth) * Screen.width; 
    }
}

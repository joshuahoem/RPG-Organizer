using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    
    [SerializeField] public GameObject playerPrefab;

    private void Start() 
    {
        //PhotonNetwork.Instantiate(playerPrefab.name, new Vector2 (0,0), Quaternion.identity); 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    void Start()
    {
       // PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(-1, -2, 0), Quaternion.identity , 0);

        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);

        myPlayer.GetComponentInChildren<Camera>().enabled = true;

}
}


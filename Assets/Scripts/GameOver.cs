using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
public class GameOver : MonoBehaviour
{
    public void Respawn()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
    

    public void MainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}

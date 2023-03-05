using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Respawn() // bug
    {
        PhotonNetwork.LoadLevel("MainGame");
    }

    public void MainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}

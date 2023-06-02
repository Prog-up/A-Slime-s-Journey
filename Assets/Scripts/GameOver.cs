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
        Debug.Log("respawn");
        Debug.Log(GameManager.GM.ShouldSpawn);
        GameManager.GM.Spawn();
        GameManager.GM.dead --;
    }
    
    public void MainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}

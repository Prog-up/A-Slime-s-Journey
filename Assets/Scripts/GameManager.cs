using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private bool Off = false;
    public GameObject disconnectUI;
    public GameObject PlayerFeed;
    public GameObject FeedGrid;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public (float, float)[] pos1 = new (float, float)[2] {(27.94f, -2.44f), (53.84f, 1.52f)};
    public (float, float)[] pos2 = new (float, float)[1] {(43.09f, -0.89f)};
    private bool ShouldSpawn = true;

    private void Awake()
    {
        SpawnPlayer();
        Debug.Log("0PlayerList = " + PhotonNetwork.playerList.Length);
        Debug.Log("0PlayerCount = " + PhotonNetwork.room.PlayerCount);
        Debug.Log("0ShouldSpawn = " + ShouldSpawn);
    }

    private void Update()
    {
        CheckInput();
        Debug.Log("PlayerList = " + PhotonNetwork.playerList.Length);
        Debug.Log("PlayerCount = " + PhotonNetwork.room.PlayerCount);
        Debug.Log("ShouldSpawn = " + ShouldSpawn);
    }   

    private void CheckInput()
    {
        if (Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            Off = false;
        }
        else if (!Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            Off = true;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y), Quaternion.identity, 0);
    }

    private void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " joined the game";
        obj.GetComponent<Text>().color = Color.green;
        Debug.Log("test = " + PhotonNetwork.room.PlayerCount);
        if (PhotonNetwork.room.PlayerCount == 1)
        {
            ShouldSpawn = false;
        }
        if (ShouldSpawn)
        {
            for (int i = 0; i < pos1.Length; i++)
            {
                PhotonNetwork.InstantiateSceneObject(Enemy1.name, new Vector2(pos1[i].Item1, pos1[i].Item2), Quaternion.identity, 0, null);
            }
            for (int i = 0; i < pos2.Length; i++)
            {
                PhotonNetwork.InstantiateSceneObject(Enemy2.name, new Vector2(pos2[i].Item1, pos2[i].Item2), Quaternion.identity, 0, null);
            }
        }
        // ShouldSpawn = false;
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " left the game";
        obj.GetComponent<Text>().color = Color.red;
    }
}

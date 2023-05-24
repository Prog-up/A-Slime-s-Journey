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
    public (float, float)[] pos = new (float, float)[2] {(27.94f, -2.44f), (53.84f, 1.52f)};

    private void Awake()
    {
        SpawnPlayer();
    }

    private void Start()
    {
        if (PhotonNetwork.playerList.Length == 1)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                PhotonNetwork.InstantiateSceneObject(Enemy1.name, new Vector2(pos[i].Item1, pos[i].Item2), Quaternion.identity, 0, null);
            }
        }
    }

    private void Update()
    {
        CheckInput();
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
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " left the game";
        obj.GetComponent<Text>().color = Color.red;
    }
}

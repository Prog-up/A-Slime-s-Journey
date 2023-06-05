using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public bool InOptions = false;
    public GameObject GameCanvas;
    public GameObject disconnectUI;
    public GameObject PlayerFeed;
    public GameObject FeedGrid;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public (float, float)[] pos1 = new (float, float)[4] {(27.94f, -2.44f), (53.84f, 1.52f), (204f,-1.4f), (141.5f,-1.45f)};
    public (float, float)[] pos2 = new (float, float)[4] {(43.09f, -0.89f), (152.4f,13f), (212.58f,-3.9f), (107.0791f,4.06f)};
    public bool ShouldSpawn = true;
    public int dead = 0;
    public int nbAlive => PhotonNetwork.room.PlayerCount - dead; 
    private float SpawnPlayerMinPos = -1f;
    private float SpawnPlayerMaxPos = 1f;
    public bool IsDead = false;
    public bool InGame;
    
    //Used for singleton
    public static GameManager GM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode jump {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}
    public KeyCode power {get; set;}
    public KeyCode transfo {get; set;}
    public KeyCode pause {get; set;}
    public bool GetShouldSpawn()
    {
        return ShouldSpawn;
    }

    private void Awake()
    {
        SpawnPlayer();
        Spawn();
        InGame = true;
        
        //Singleton pattern
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }	
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        /*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game, 
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
        jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "Q"));
        right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        power = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("powerKey", "Z"));
        transfo = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("transfoKey", "S"));
        pause = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pauseKey", "Escape"));
        GameCanvas.transform.Find("RoomName").gameObject.GetComponent<Text>().text = "Room name = " + PhotonNetwork.room.Name;
    }

    private void Update()
    {
        PauseButton();
        Spawn();
        if (InGame)
        {
            GameCanvas.transform.Find("GameOver").gameObject.SetActive(IsDead);
        }
    }   

    private void PauseButton()
    {
        if (Input.GetKeyDown(GameManager.GM.pause) && !MenuScript.MS.waitingForKey)
        {transform.Find("UI").gameObject.SetActive(true);
            PauseMenu();
        }
    }


    public void Spawn()
    {
        if (ShouldSpawn && PhotonNetwork.room.PlayerCount == 1 )
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

        ShouldSpawn = false;
    }

    public void PauseMenu()
    {
        
        if (GameCanvas.transform.Find("OptionMenuCanvas").gameObject.activeSelf || GameCanvas.transform.Find("DisconnectMenu").gameObject.activeSelf)
        {
            GameCanvas.transform.Find("DisconnectMenu").gameObject.SetActive(false);
            GameCanvas.transform.Find("OptionMenuCanvas").gameObject.SetActive(false);
            InOptions = false;
        }
        else
        {
            GameCanvas.transform.Find("DisconnectMenu").gameObject.SetActive(!GameCanvas.transform.Find("DisconnectMenu").gameObject.activeSelf);
            InOptions = !InOptions;
        }
    }

    private void GameOverMenu()
    {
         GameCanvas.transform.Find("GameOver").gameObject.SetActive(true);
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
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " left the game";
        obj.GetComponent<Text>().color = Color.red;
    }

    public void OptionsButton()
    {
        GameCanvas.transform.Find("DisconnectMenu").gameObject.SetActive(!GameCanvas.transform.Find("DisconnectMenu").gameObject.activeSelf);
        GameCanvas.transform.Find("OptionMenuCanvas").gameObject.SetActive(!GameCanvas.transform.Find("OptionMenuCanvas").gameObject.activeSelf);
    }
    
    public void Respawn()
    {
        IsDead = false;
        GameManager.GM.dead --;
    }
    
    public void MainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
        InGame = false;
    }
}

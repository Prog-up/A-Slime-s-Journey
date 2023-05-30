using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    private bool Off = false;
    private bool ShouldSpawn = true;
    public GameObject disconnectUI;
    public GameObject PlayerFeed;
    public GameObject FeedGrid;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public (float, float)[] pos = new (float, float)[2] {(33f, -3f), (278f, 7.4512f)};
    public (float, float)[] pos2 = new (float, float)[4] {(191.8088f, 10.6205f), (166.94f, 7.99f), (98.65985f, 7.866865f), (255f, 7.866865f)};
	public GameObject Enemy3;
	public (float, float)[] pos3 = new (float, float)[3] {(227.7694f, 10.87978f), (207f, 13.57f), (123.22f,10.48f)};



    //Used for singleton
    public static GameManager2 GM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode jump {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}
    public KeyCode power {get; set;}
    public KeyCode transfo {get; set;}
    public KeyCode pause {get; set;}
    private void Awake()
    
    {
        SpawnPlayer();

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
        CheckInput();
        Spawn();
    }
    private void PauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            PhotonNetwork.InstantiateSceneObject(Enemy1.name, new Vector2(pos[i].Item1, pos[i].Item2), Quaternion.identity, 0, null);
        }
        for (int i = 0; i < pos2.Length; i++)
        {
            PhotonNetwork.InstantiateSceneObject(Enemy2.name, new Vector2(pos2[i].Item1, pos2[i].Item2), Quaternion.identity, 0, null);
        }
		for (int i = 0; i < pos3.Length; i++)
        {
            PhotonNetwork.InstantiateSceneObject(Enemy3.name, new Vector2(pos3[i].Item1, pos3[i].Item2), Quaternion.identity, 0, null);
        }
    }
    public void PauseMenu()
    {
        if (GameCanvas.transform.Find("OptionMenuCanvas").gameObject.activeSelf || GameCanvas.transform.Find("DisconnectMenu").gameObject.activeSelf)
        {
            GameCanvas.transform.Find("DisconnectMenu").gameObject.SetActive(false);
            GameCanvas.transform.Find("OptionMenuCanvas").gameObject.SetActive(false);
        }
        else
        {
            GameCanvas.transform.Find("DisconnectMenu").gameObject.SetActive(!GameCanvas.transform.Find("DisconnectMenu").gameObject.activeSelf);
        }
    }
    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
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


    public void OptionsButton()
    {
        GameCanvas.transform.Find("DisconnectMenu").gameObject.SetActive(!GameCanvas.transform.Find("DisconnectMenu").gameObject.activeSelf);
        GameCanvas.transform.Find("OptionMenuCanvas").gameObject.SetActive(!GameCanvas.transform.Find("OptionMenuCanvas").gameObject.activeSelf);
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
}

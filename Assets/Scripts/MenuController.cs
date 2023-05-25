using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPannel;
    [SerializeField] private InputField UsernameInput;
    [SerializeField] public InputField JoinGameInput;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject JoinButton;
    private RoomOptions roomOptions;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    public string Getgamename()
    {
        return JoinGameInput.text;
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) // Input Button Enter is not setup
        {
            JoinGame();
        }
    }
    public void ChangeUserNameInput()
    {
        if(UsernameInput.text.Length >= 3)
        {
            for(int i = 0; i < UsernameInput.text.Length; i++)
                if(UsernameInput.text[i] != ' ')
                {
                    StartButton.SetActive(true);
                }
        }
        else
        {
            StartButton.SetActive(false);
        }
    }
    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        ConnectPannel.SetActive(true);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void ChangeRoomNameInput()
    {
        if(JoinGameInput.text.Length > 0)
        { 
            for(int i = 0; i < JoinGameInput.text.Length; i++)
                if(JoinGameInput.text[i] != ' ')
            JoinButton.SetActive(true);
        }
        else
        {
            JoinButton.SetActive(false);
        }
    }

    public void JoinGame()
    {
        if(JoinGameInput.text.Length > 0)
        {
            roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text.ToUpper(), roomOptions, TypedLobby.Default);
        }
    }

    private void OnJoinedRoom()
    {
        bool pres = false;
        foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
        {
            if (roomInfo.Name == JoinGameInput.text)
            {
                pres = true;
                break;
            }
        }
        
        if (false && roomOptions.MaxPlayers == 2 && !pres)
        {
            PhotonNetwork.LoadLevel("Lobby");
        }
        else
        {
            PhotonNetwork.LoadLevel("MainGame");
        }
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void Back()
    {
        ConnectPannel.SetActive(false);
        UsernameMenu.SetActive(true);
    }
    
    public void OnJoinRoomFailed(short returnCode, string message)
    {
        if (returnCode == ErrorCode.GameFull)
        {
            Debug.Log("La room est pleine. Impossible de rejoindre."); // TODO: test
            // Affichez un message à l'utilisateur ou déclenchez une action appropriée.
        }
    }
}

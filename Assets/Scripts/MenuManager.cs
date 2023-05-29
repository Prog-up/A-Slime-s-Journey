using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform Canvas;
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPannel;
    [SerializeField] private InputField UsernameInput;
    [SerializeField] public InputField JoinGameInput;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject JoinButton;
    //[SerializeField] private GameObject OptionButton;
    [SerializeField] private GameObject OptionMenu;
    public Animator transition;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    
    private void Start()
    {
        Canvas = transform;
        Title = Canvas.Find("Title").gameObject;
        UsernameMenu = Canvas.Find("UserNameCanvas").gameObject;
        UsernameInput = UsernameMenu.transform.Find("UserNameInput").gameObject.GetComponent<InputField>();
        StartButton = UsernameMenu.transform.Find("StartButton").gameObject;
        //OptionButton = UsernameMenu.transform.Find("OptionButton").gameObject;
        ConnectPannel = Canvas.Find("MainMenuCanvas").gameObject;
        JoinGameInput = ConnectPannel.transform.Find("JoinInput").gameObject.GetComponent<InputField>();
        JoinButton = ConnectPannel.transform.Find("JoinButton").gameObject;
        OptionMenu = Canvas.Find("OptionMenuCanvas").gameObject;
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
            StartCoroutine(NextLevel());
        }
    }

    private IEnumerator NextLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text.ToUpper(), new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
    public void ExitGame() 
    {
        Application.Quit();
    }

    public void InOption()
    {
        Title.SetActive(false);
        UsernameMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void Back()
    {
        Title.SetActive(true);
        ConnectPannel.SetActive(false);
        OptionMenu.SetActive(false);
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class MyLauncher : MonoBehaviourPunCallbacks
{
    public Button btn;
    public Text feedbackText;
    private byte maxPlayersPerRoom = 2;
    private bool isConnecting;
    private string gameVersion = "1";
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    public void Connect()
    {
        
        feedbackText.text = "";

        
        isConnecting = true;


        btn.interactable = false;
        

        
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            
            PhotonNetwork.JoinRandomRoom();
        }else{

            LogFeedback("Connecting...");
				
            
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }
    void LogFeedback(string message)
    {
        if (feedbackText == null) {
            return;
        }
        feedbackText.text += System.Environment.NewLine+message;
    }
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
            PhotonNetwork.JoinRandomRoom();
        }
        
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom});
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("<Color=Red>OnDisconnected</Color> "+cause);
        Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");

        isConnecting = false;
        btn.interactable = true;

    }
    public override void OnJoinedRoom()
    {
        LogFeedback("<Color=Green>OnJoinedRoom</Color> with "+PhotonNetwork.CurrentRoom.PlayerCount+" Player(s)");
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");
		
        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");

            // #Critical
            // Load the Room Level. 
            PhotonNetwork.LoadLevel("MyRoom");

        }
    }
}

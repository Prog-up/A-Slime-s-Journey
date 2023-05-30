using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class Teleporteur : MonoBehaviour
{
    public Animator transition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transition.SetTrigger("Start");
            //PhotonNetwork.LoadLevel("Level2Test");
            Damage.life = 3;
            // GameManager.GM.ChangeLevel("Level2Test");
            // PhotonView.RPC("RPC_ChangeLevel", PhotonTargets.All, "Level2Test");
        }
    }
}

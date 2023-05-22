using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporteur : MonoBehaviour
{
    // public GameObject Player1;
    // public GameObject Player2;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PhotonNetwork.LoadLevel("Level2Test");
        }
    }
}

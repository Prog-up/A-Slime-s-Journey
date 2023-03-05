using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject player;
    private int life = 3;
    
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
        {
            life--;
            Debug.Log("Damage");
            switch (life)
            {
                case 2:
                    heart3.SetActive(false);
                    break;
                case 1:
                    heart2.SetActive(false);
                    break;
                case 0:
                    heart1.SetActive(false);
                    PhotonNetwork.LeaveRoom();
                    PhotonNetwork.LoadLevel("GameOver");
                    break;
                default:
                    break;
            }
        }
	}

    void Update()
    {
        if (transform.position.y < -5)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("GameOver");
        }
    }
}

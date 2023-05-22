using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private int life = 3;
    public bool IsAvailable = true;
    public float CooldownDuration = 2.0f;
    public PhotonView photonView;

    void OnTriggerEnter2D(Collider2D collision)
	{
        if (IsAvailable == false)
        {
            return;
        }
        if (collision.CompareTag("Enemy") && photonView.isMine)
        {
            life--;
            Debug.Log("Damage");
            switch (life)
            {
                case 2:
                    heart3.SetActive(false);
                    StartCoroutine(StartCooldown());
                    break;
                case 1:
                    heart2.SetActive(false);
                    StartCoroutine(StartCooldown());
                    break;
                case 0:
                    heart1.SetActive(false);
                    Debug.Log(transform.parent.name);
                    PhotonNetwork.LeaveRoom();
                    PhotonNetwork.LoadLevel("GameOver");
                    break;
                default:
                    break;
            }
        }
	}
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
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

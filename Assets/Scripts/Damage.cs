using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public static int life = 3;
    public bool IsAvailable = true;
    public float CooldownDuration = 2.0f;
    public PhotonView photonView;
	public GameObject deathScreenUI;
    public Button restartButton;
    public Button mainMenuButton;

    void OnTriggerEnter2D(Collider2D collision)
	{
        if (IsAvailable == false)
        {
            if (collision.CompareTag("Heal") && photonView.isMine)
            {
                switch (life)
                {
                    case 2:
                        life += 1;
                        heart3.SetActive(true);
                        break;
                    case 1:
                        life += 1;
                        heart2.SetActive(true);
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            
            }
            return;
        }
        if (collision.CompareTag("Enemy") && photonView.isMine)
        {
            life--;
            switch (life)
            {
                case 2:
                    heart3.SetActive(false);
                    StartCoroutine(StartCooldown());
                    break;
                case 1:
                    heart3.SetActive(false);
                    heart2.SetActive(false);
                    StartCoroutine(StartCooldown());
                    break;
                case 0:
                    heart3.SetActive(false);
                    heart2.SetActive(false);
                    heart1.SetActive(false);
                    life--;
                    GameManager.GM.dead ++;
                    Debug.Log("nb alive = " + GameManager.GM.nbAlive);
                    PhotonNetwork.LoadLevel("GameOver");
                    break;
                case  < 0:
                    heart3.SetActive(true);
                    heart2.SetActive(true);
                    heart1.SetActive(true);
                    life = 3;
                    
                    break;
                default:
                    break;
            }
        }
        if (collision.CompareTag("Heal") && photonView.isMine)
        {
            switch (life)
            {
                case 2:
                    life += 1;
                    heart3.SetActive(true);
                    break;
                case 1:
                    life += 1;
                    heart2.SetActive(true);
                    break;
                case 3:
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
            if (photonView.isMine)
            {
                PhotonNetwork.LoadLevel("GameOver");
                GameManager.GM.dead ++;
            }
                
        }
    }
}

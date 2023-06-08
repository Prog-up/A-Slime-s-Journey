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

    public bool admin;
    public AudioSource healing;
	/*public GameObject deathScreenUI;
    public Button restartButton;
    public Button mainMenuButton;*/

    void Start()
    {
        if(PhotonNetwork.playerName.ToLower() == "admin")
        {
            admin = true;
        }
        else
        {
            admin = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
	{
        if (IsAvailable == false && !admin)
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
                healing.Play();
        
            }
            return;
        }
        if (((collision.CompareTag("Enemy") && photonView.isMine) || (collision.CompareTag("boss") && photonView.isMine)) && !admin)
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
                    Death();
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
            healing.Play();
        
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
                Death();
            }
                
        }
    }

    private void Death()
    {
        
        heart3.SetActive(true);
        heart2.SetActive(true);
        heart1.SetActive(true);
        transform.parent.position = new Vector3(-100f, -1f, 0f);
        life = 3;
        GameManager.GM.IsDead = true;
        GameManager.GM.dead ++;
    }
}

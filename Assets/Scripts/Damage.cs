using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private static int life = 3;
    public bool IsAvailable = true;
    public float CooldownDuration = 2.0f;
    public PhotonView photonView;
	public GameObject deathScreenUI;
    public Button restartButton;
    public Button mainMenuButton;

    public static int GetLife()
    {
        return life;
    }
    
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
                    heart3.SetActive(false);
                    heart2.SetActive(false);
                    StartCoroutine(StartCooldown());
                    break;
                case 0:
                    heart3.SetActive(false);
                    heart2.SetActive(false);
                    heart1.SetActive(false);
                    ShowDeathScreen();
                    break;
                default:
                    break;
            }
            
        }
        if(life == 0)
        {
            if(this.photonView.isMine)
            {
                Destroy(this);
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
            PhotonNetwork.LoadLevel("GameOver");
        }
    }
	

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowDeathScreen()
    {
        deathScreenUI.SetActive(true);
    }

    private void RestartGame()
    {
        deathScreenUI.SetActive(false);
    }

    private void ReturnToMainMenu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;
    public Button restartButton;
    public Button mainMenuButton;

    private void Start()
    {
        deathScreenUI.SetActive(false);
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

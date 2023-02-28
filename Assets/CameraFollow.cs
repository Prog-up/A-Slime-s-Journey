using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;

public class CameraFollow : MonoBehaviour
{
    public GameObject cameraholder;
    public Vector3 offSet;

    public void OnStartAuthority()
    {
       cameraholder.SetActive(true);
    }

    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "MylauncherScene")
        {
            cameraholder.transform.position = transform.position + offSet;
        } 
    }
}
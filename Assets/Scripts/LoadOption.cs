using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOption : MonoBehaviour
{
    public void ChangeToOptionScene()
    {
        PhotonNetwork.LoadLevel("Options");
    }
}

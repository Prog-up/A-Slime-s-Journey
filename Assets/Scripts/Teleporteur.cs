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
            StartCoroutine(WaitTeleport(collision.transform));
            
        }
    }
    
    private IEnumerator WaitTeleport(Transform Player)
    {
        yield return new WaitForSeconds(1);
        //Player.position = new Vector3(380f, Player.position.y, Player.position.z);
        transition.SetTrigger("Restart");
    }
}

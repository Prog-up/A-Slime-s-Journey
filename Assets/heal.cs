using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartCooldown());
        }
        
    }

	public IEnumerator StartCooldown()
    {
	    yield return new WaitForSeconds(0.5f);
	    PhotonNetwork.Destroy(gameObject);
    }
}

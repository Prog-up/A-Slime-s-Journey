using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
	public Animator animator;
	
	private float timer;

	public AudioSource mortcochon;

	public bool alive = true;

	public bool IsAlive()
	{
		return alive;
	}
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			animator.SetTrigger("Die");
			alive = false;
			mortcochon.Play();
			StartCoroutine(StartCooldown());
			
		}
		if (collision.CompareTag("Cailloux"))
		{
			animator.SetTrigger("Die");
			alive = false;
			mortcochon.Play();
			StartCoroutine(StartCooldown());

		}
	}

    public IEnumerator StartCooldown()
    {
	    yield return new WaitForSeconds(0.5f);
	    PhotonNetwork.Destroy(transform.parent.gameObject);
    }
}

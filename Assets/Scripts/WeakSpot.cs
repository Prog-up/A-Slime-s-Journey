using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
	public Animator animator;

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
			PhotonNetwork.Destroy(transform.parent.gameObject);
			mortcochon.Play();
		}
		if (collision.CompareTag("Cailloux"))
		{
			animator.SetTrigger("Die");
			alive = false;
			PhotonNetwork.Destroy(transform.parent.gameObject);
			mortcochon.Play();
		}
	}
}

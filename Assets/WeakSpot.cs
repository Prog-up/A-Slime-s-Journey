using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
	public Animator animator;

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
				Destroy(transform.parent.gameObject, 0.5f);
			}
	}
}

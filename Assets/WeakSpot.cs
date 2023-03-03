using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
	public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			{
				animator.SetTrigger("Die");
				Destroy(transform.parent.gameObject, 1f);
			}
	}
}

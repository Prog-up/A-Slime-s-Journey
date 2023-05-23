using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
	private GameObject player;
	public Animator animator;
	private bool Activated;
	public GameObject Hitbox;
	
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		Activated = false;
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("activated");
			Destroy(Hitbox, 0.3f);
        }
    }
}
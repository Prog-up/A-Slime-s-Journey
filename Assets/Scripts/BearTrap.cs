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
        if(Activated == false)
        {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Cailloux")))
        {
            animator.SetTrigger("activated");
            Activated = true;
            PhotonNetwork.Destroy(Hitbox);
            Destroy(Hitbox, 0.5F);
            Debug.Log("activated = " + Activated );
        }
        }
    }
}

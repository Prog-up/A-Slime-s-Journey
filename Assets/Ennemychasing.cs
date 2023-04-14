using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemychasing : MonoBehaviour
{
	public float speed;
	public Transform[] waypoints;
	public Transform target;
	public Rigidbody2D rb;
	public bool Alive;
	public SpriteRenderer graphic;
	public GameObject player;
	public LayerMask collisionLayers;
	public bool IsGrounded = false;
	public float JumpForce;
	public Transform Capteur;
	public Transform Capteur2;


    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

	void Update()
	{
		MoveEnnemy();
		
        
	}


    // Update is called once per frame
    
	void MoveEnnemy()
	{
		var pos = target.position;
		var villager = transform.position;

        if (villager.x-player.transform.position.x > 0)
		{
			if (Vector2.Distance(transform.position, player.transform.position) < 8f) //(transform.position.x-pos.x < 10)
			{
				rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
			}
			graphic.flipX = true;
		}
		else
		{
			
			if (Vector3.Distance(transform.position, player.transform.position) < 8f)//(villager.x-pos.x > -10)
			{
				rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
			}
			graphic.flipX = false;
		}

		IsGrounded = (Physics2D.OverlapCircle(Capteur.position, 0.4f, collisionLayers) || (Physics2D.OverlapCircle(Capteur2.position, 0.4f, collisionLayers)));
        if (IsGrounded)
        {
           rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
	}
		
}

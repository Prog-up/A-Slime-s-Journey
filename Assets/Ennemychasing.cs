using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemychasing : MonoBehaviour
{
	public float speed;
	public Transform[] waypoints;
	public Transform target;
	private Vector3 pos;
	public Rigidbody2D rb;
	public bool Alive;
	public SpriteRenderer graphic;

	public LayerMask collisionLayers;
	public bool IsGrounded = false;
	public float JumpForce;
	public Transform Capteur;
	public Transform Capteur2;


    // Start is called before the first frame update
    void Start()
    {
       
    }

	void Update()
	{
		MoveEnnemy();
		IsGrounded = (Physics2D.OverlapCircle(Capteur.position, 1f, collisionLayers) || (Physics2D.OverlapCircle(Capteur2.position, 0.5f, collisionLayers)));
        if (IsGrounded)
        {
           rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        
	}


    // Update is called once per frame
    
	void MoveEnnemy()
	{
		pos = target.position;
		var villager = transform.position;

        if (villager.x-pos.x > 0)
		{
			if (Vector3.Distance(transform.position, target.position) < 50f) //(transform.position.x-pos.x < 10)
			{
				rb.AddForce(Vector3.left * speed, ForceMode2D.Force);
			}
		}
		else
		{
			if (Vector3.Distance(transform.position, target.position) < 50f)//(villager.x-pos.x > -10)
			{
				rb.AddForce(Vector3.right * speed, ForceMode2D.Force);
			}
		}
	}
		
}

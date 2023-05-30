using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss : MonoBehaviour
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

    public Animator anim;


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
            if (Vector2.Distance(transform.position, player.transform.position) < 7f) //(transform.position.x-pos.x < 10)
            {
                rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
                if (Vector2.Distance(transform.position, player.transform.position) < 2f) //(transform.position.x-pos.x < 10)
                {
                    anim.SetBool("punch", true);
                }
                else
                {
                    anim.SetBool("punch", false);
                }
            }
            graphic.flipX = true;
        }
        else
        {
			
            if (Vector3.Distance(transform.position, player.transform.position) < 7f)//(villager.x-pos.x > -10)
            {
                rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
                if (Vector2.Distance(transform.position, player.transform.position) < 2f) //(transform.position.x-pos.x < 10)
                {
                    anim.SetBool("punch", true);
                }
                else
                {
                    anim.SetBool("punch", false);
                }
            }
            graphic.flipX = false;
        }

		
    }
}

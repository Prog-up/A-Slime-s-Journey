using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float Speed = 4.5f;
    public float force;
    private Rigidbody2D rb;
    // Update is called once per frame
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            rb.velocity = new Vector2(5,0).normalized * force * -1;
        }
        else
        {
            rb.velocity = new Vector2(5, 0).normalized * force;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed;
	private bool alive = true;
    private int dir = -1;
    private Rigidbody2D rb;
    // public Animator animator;
	// public AudioSource mortcochon;
    private int timer = 0;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2((alive ? speed * dir : 0), rb.velocity.y);
        // rb.velocity = new Vector2(0, rb.velocity.y);
        // Debug.Log("Alive = " + alive);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Timer = " + timer);
        if (other.CompareTag("EnnemyArea") && timer - (int)Time.timeSinceLevelLoad != 0)
        {
            // Debug.Log("Exit");
            dir = -dir;
            transform.rotation = new Quaternion(0, ((transform.rotation.y == 0)? 180 : 0), 0, 0);
            timer = (int)Time.timeSinceLevelLoad; // Empêche le monstre de se retourner une 2nd fois trop rapidement et de sortir de sa zone
        }
        // else if (other.CompareTag("Player"))
        // {
        //     Debug.Log("Killed by Player");
        //     animator.SetTrigger("Die");
        //     alive = false;
        //     Destroy(transform.gameObject, 0.5f);
        //     mortcochon.Play();
        // }
    }
}

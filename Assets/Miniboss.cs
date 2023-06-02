using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Rigidbody2D rb;
    public bool Alive;
    public SpriteRenderer graphic;
    public GameObject player;
    public bool IsGrounded = false;
    public float JumpForce;
    public Transform Capteur;
    public Transform Capteur2;

    public Animator anim;

    public GameObject WeakSpotH;
	
	public GameObject WeakSpotS;
	
	public float timer;

	public AudioSource degats;

    public float life;
  

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        life = 10;
    }

    void Update()
    {
		MoveEnnemy();
		timer += Time.deltaTime;
		if (timer % 2 != 0)
		{
			MoveEnnemy();
		}
        if (life == 0)
		{
			Destroy(gameObject);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			anim.SetBool("Hurt",true);
			degats.Play();
			StartCoroutine(StartCooldown());
		}
		if (collision.CompareTag("Cailloux"))
		{
			anim.SetBool("Hurt",true);
			degats.Play();
			StartCoroutine(StartCooldown());
		}
	}
	IEnumerator StartCooldown()
    {
		life -=1 ;
	    yield return new WaitForSeconds(0.5f);
		anim.SetBool("Hurt",false);
    }

    // Update is called once per frame
    
    void MoveEnnemy()
    {
        var pos = target.position;
        var villager = transform.position;

        if (villager.x-player.transform.position.x > 0)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 9f) //(transform.position.x-pos.x < 10)
            {
				if (Vector2.Distance(transform.position, player.transform.position) <8f && Vector2.Distance(transform.position, player.transform.position) >7.5f )
                {
                    rb.AddForce(new Vector2(1,4));

                }
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
			
            if (Vector3.Distance(transform.position, player.transform.position) < 10f)//(villager.x-pos.x > -10)
            {
                if (Vector2.Distance(transform.position, player.transform.position) <8f && Vector2.Distance(transform.position, player.transform.position) >7.5f )
                {
                    rb.AddForce(new Vector2(1,4));

                }
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


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
    public float JumpForce;
    public Transform Capteur;
    public Transform Capteur2;

    public Animator anim;
    public float timer;

	public AudioSource degats;
	public AudioSource death;
	
	public float life;
	public float maxlife = 20;
	public bool shot;

	public bool shot2;
	public GameObject arrow;

	public Transform arrowPos;

	[SerializeField] FloatHealth healthbar;
    
	
	private void Awake()
	{
		healthbar = GetComponentInChildren<FloatHealth>();
	}
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        life = maxlife;
        shot = false;
        shot2 = false;
        healthbar.UpdateHealthBar(life, maxlife);
    }

    
    void Update()
    {
	    player = GameObject.FindGameObjectWithTag("Player");
	    MoveEnnemy();
		if (life <= 0)
		{
			anim.SetTrigger("death");
			death.Play();
			StartCoroutine(StartCooldown3());
		}
		if (life <= 5 && shot==false)
		{
			shot = true;
			Angry();
		}
		if (life <= 2 && shot2==false)
		{
			shot2 = true;
			Angry();
		}
    }
    
    IEnumerator StartCooldown3()
    {
	    yield return new WaitForSeconds(0.5f);
	    Destroy(gameObject);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			anim.SetBool("Hurt",true);
			if (life >= 1)
			{
				degats.Play();
			}
			healthbar.UpdateHealthBar(life, maxlife);
			StartCoroutine(StartCooldown());
		}
		if (collision.CompareTag("Cailloux"))
		{
			anim.SetBool("Hurt",true);
			if (life >= 1)
			{
				degats.Play();
			}
			healthbar.UpdateHealthBar(life, maxlife);
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
            if (Vector2.Distance(transform.position, player.transform.position) < 10f) //(transform.position.x-pos.x < 10)
            {
				if (Vector2.Distance(transform.position, player.transform.position) <8f && Vector2.Distance(transform.position, player.transform.position) >7.5f )
                {
                    rb.AddForce(new Vector2(2,3));

                }
                rb.AddForce(Vector2.left * speed*2, ForceMode2D.Force);
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
                    rb.AddForce(new Vector2(2,3));

                }
                rb.AddForce(Vector2.right * speed*2, ForceMode2D.Force);
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

    void Angry()
    {
	    anim.SetTrigger("angry");
	    rb.AddForce(new Vector2(1,100));
	    StartCoroutine(StartCooldown2());
	    
    }
    
    IEnumerator StartCooldown2()
    {
	    yield return new WaitForSeconds(2f);
	    var c = transform.position.y+4;
	    transform.position = new Vector3(transform.position.x,c,transform.position.z);
	    PhotonNetwork.InstantiateSceneObject(arrow.name, arrowPos.position, Quaternion.identity, 0, null);
    }
}


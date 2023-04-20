using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{

    public PhotonView photonView;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

   //Deplacements
    public bool IsGrounded = false;
    private bool IsWalledRight = false;
    private bool IsWalledLeft = false;
    public float MoveSpeed;
    public float JumpForce;
    private float MoveForce;
    public Transform GroundCheck;
    public float GroundCheckRadius;

    //public Transform WallCheckRight;
    //public Transform WallCheckLeft;

    // Apparence + son
    public LayerMask collisionLayers;
    public AudioSource jumpsound;
    public SpriteRenderer Destination; //TODO : Fix me

    //Permet de connaitre la forme actuelle
    public bool IsDefault = true;
    public bool IsRock = false;


    //Permet de destroy
    public GameObject ToDestroy;
    public float vitesseEscalade = 2f; // Vitesse d'escalade
    public float distanceDetectionMur = 5; // Distance pour détecter la collision avec le mur

    private bool isTouchingWall;
    public Transform WallCheckRight;
    public Transform WallCheckLeft;

    private float climbSpeed = 3f;
    private float verticalInput;
    public ProjectileBehaviour projectile;
    public Transform LaunchOffset;

    private bool Isrolling = false;

    private void Escalade()
    {
        if (IsRock)
        {

            isTouchingWall = Physics2D.OverlapCircle(WallCheckRight.position, GroundCheckRadius-0.1f, collisionLayers)||Physics2D.OverlapCircle(WallCheckLeft.position, GroundCheckRadius, collisionLayers);
            verticalInput = Input.GetAxis("Vertical");

            if (isTouchingWall && verticalInput>0)
            {
                // disable gravity
                rb.gravityScale = 0f;

                // move the character up
                transform.position += new Vector3(0f, climbSpeed * Time.deltaTime, 0f);
                Isrolling = true;
                anim.SetBool("Isrolling", Isrolling);
            }
            else
            {
                // enable gravity
                rb.gravityScale = 1f;
                Isrolling = false;
            }
            anim.SetBool("Isrolling", Isrolling);
        }

    }

    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerCamera.SetActive(false);
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.red;
        }
    }

    private void CheckInput()
    {
        Hor();
        Jump();
        Escalade();
        Tir();
    }

    private void Hor()
    {
        if (Input.GetKeyDown(KeyCode.Q) || (int)Input.GetAxisRaw("Horizontal") == -1)
        {
            photonView.RPC("FlipTrue",PhotonTargets.AllBuffered);
            MoveForce = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || (int)Input.GetAxisRaw("Horizontal") == 1)
        {
            photonView.RPC("FlipFalse",PhotonTargets.AllBuffered);
            MoveForce = 1;
        }
        else
        {
            MoveForce = 0;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(MoveForce*MoveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
    private void Jump()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, collisionLayers);
        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                jumpsound.Play();
                anim.SetBool("Isjumping",!IsGrounded);
            }
        }
        anim.SetBool("Isjumping",!IsGrounded);
    }
    void ChangeSprite()
    {

       if(Input.GetKeyDown(KeyCode.T) && IsDefault)
       {
            IsRock = true;
            IsDefault = false;
            anim.SetBool("IsRock", IsRock);
       }
       if(Input.GetKeyDown(KeyCode.T) && IsRock)
       {
            IsRock = false;
            IsDefault = true;
       }
       anim.SetBool("IsRock", IsRock);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            IsRock = true;
        }

    }

    private void Tir()
    {
        if (IsRock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(projectile, LaunchOffset.position, transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine && photonView.gameObject.activeSelf)
        {
            CheckInput();
            ChangeSprite();
        }
    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }


    //void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (collision.gameObject.tag == "GG")
      //  {
        //    PhotonNetwork.LoadLevel("GameOver");
          //  PhotonNetwork.LeaveRoom();
            //Destroy(ToDestroy);
        //}
    //}

}

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
    public float MoveSpeed;
    public float JumpForce;
    private float MoveForce;
    public Transform GroundCheck;
    public float GroundCheckRadius;
   
    // Apparence + son
    public LayerMask collisionLayers;
    public AudioSource jumpsound;
    public SpriteRenderer Destination; //TODO : Fix me

    //Permet de connaitre la forme actuelle
    public bool IsDefault = true;
    public bool IsRock = false;


    //Permet de destroy
    public GameObject ToDestroy;
    
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
       if(Input.GetKeyDown(KeyCode.X) && IsDefault)
       {                
            IsRock = true;
            IsDefault = false;
       }
       if(Input.GetKeyDown(KeyCode.X) && IsRock)
       {
            IsRock = false;
            IsDefault = true;
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


    void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "GG")
        {
            PhotonNetwork.LoadLevel("GameOver");
            PhotonNetwork.LeaveRoom();
            Destroy(ToDestroy);
        }
    }

}
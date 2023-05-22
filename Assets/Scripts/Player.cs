using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{

    public PhotonView photonView;
    public Rigidbody2D rb;
    private Animator anim;
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
    public float CooldownDuration = 1.5f;
    public bool IsAvailable = true;

    private bool Hurt1 = false;
    private bool Hurt2 = false;
    private float timer;
    private float timer2;

    private void Escalade()
    {
        if (IsRock)
        {

            isTouchingWall = Physics2D.OverlapCircle(WallCheckRight.position, 0.1f, collisionLayers)||Physics2D.OverlapCircle(WallCheckLeft.position, 0.1f, collisionLayers);
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

    public string getplayername()
    {
        return PlayerNameText.text;
    }

    private void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
            anim = GetComponent<Animator>();
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
            anim.SetBool("IsRock", IsDefault);
       }
       anim.SetBool("IsRock", IsRock);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            IsRock = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (IsRock)
            {
                anim.SetBool("Hurt2", true);
                Hurt2 = true;
            }
            else
            {
                anim.SetBool("Hurt1", true);
                Hurt1 = true;
            }
            
        }
        

    }

    private void Tir()
    {
        if (IsRock)
        {
            if (!IsAvailable)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                PhotonNetwork.InstantiateSceneObject(projectile.name, LaunchOffset.position, Quaternion.identity, 0, null);
                StartCoroutine(StartCooldown());
            }
        }
    }
    
    
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine && photonView.gameObject.activeSelf)
        {
            CheckInput();
            ChangeSprite();
        }

        if (Hurt1 == true)
        {
            timer += Time.deltaTime;
        }

        if (Hurt2)
        {
            timer2 += Time.deltaTime;
        }
        if (timer > 2)
        {
            timer = 0;
            anim.SetBool("Hurt1", false);
            Hurt1 = false;
        }
        if (timer2 > 2)
        {
            timer2 = 0;
            anim.SetBool("Hurt2", false);
            Hurt2 = false;
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

}

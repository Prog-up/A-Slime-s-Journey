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
    public GameObject Life;
    public GameObject Indicator;

    //Permet de connaitre la forme actuelle
    public bool IsDefault = true;
    public bool IsRock = false;
    public bool IsFlame = false;

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
    public Transform LaunchOffset2;

    private bool Isrolling = false;
    public float CooldownDuration = 1.5f;
    public bool IsAvailable = true;

    private bool Hurt1 = false;
    private bool Hurt2 = false;
    private float timer;
    private float timer2;
    
    // Menu
    private bool Off;

    private void Escalade()
    {
        if (IsRock)
        {
            isTouchingWall = Physics2D.OverlapCircle(WallCheckRight.position, 0.1f, collisionLayers)||Physics2D.OverlapCircle(WallCheckLeft.position, 0.1f, collisionLayers);
            verticalInput = Input.GetKey(GameManager.GM.power) ? 1 : 0;

            if (isTouchingWall && verticalInput>0)
            {
                // disable gravity
                rb.gravityScale = 0f;

                // move the character up
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
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

    public string Getplayername()
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
            Life.SetActive(false);
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
        if (Input.GetKey(GameManager.GM.right))
        {
            photonView.RPC("FlipFalse",PhotonTargets.AllBuffered);
            MoveForce = 1;
        }
        else if (Input.GetKey(GameManager.GM.left))
        {
            photonView.RPC("FlipTrue",PhotonTargets.AllBuffered);
            MoveForce = -1;
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
            if (Input.GetKey(GameManager.GM.jump))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                jumpsound.Play();
                anim.SetBool("Isjumping",!IsGrounded);
                Debug.Log("Jump!");
            }
        }
        anim.SetBool("Isjumping",!IsGrounded);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.CompareTag("Rock") && Input.GetKey(GameManager.GM.transfo))
        {
            IsRock = true;
            IsDefault = false;
            IsFlame = false;
            anim.SetBool("IsRock", IsRock);
        }*/

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
        if (IsFlame)
        {
            if (!IsAvailable)
            {
                return;
            }
            if (Input.GetKey(GameManager.GM.power)) // TODO: Power en forme de feu (à la place de la roulade)
            {
                if (sr.flipX)
                {
                    PhotonNetwork.InstantiateSceneObject(projectile.name, LaunchOffset2.position, Quaternion.identity, 0, null);
                }
                else
                {
                    PhotonNetwork.InstantiateSceneObject(projectile.name, LaunchOffset.position, Quaternion.identity, 0, null);
                }
                
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

    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.CompareTag("Rock") && Input.GetKey(GameManager.GM.transfo))
        {
            IsRock = true;
            IsDefault = false;
            IsFlame = false;
            anim.SetBool("IsRock", IsRock);
        }
        else if (other.gameObject.CompareTag("Flame") && Input.GetKey(GameManager.GM.transfo))
        {
            IsRock = false;
            IsDefault = false;
            IsFlame = true;
            anim.SetBool("IsFlame", IsFlame);
        }
        else if (other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Flame") && !(IsRock || IsFlame) )
        {
            // Debug.Log("Press Transfo !");
            Indicator.GetComponent<Text>().text = "Press " + GameManager.GM.transfo.ToString() + " !";
            Indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Flame"))
        {
            Indicator.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine && photonView.gameObject.activeSelf)
        {
            CheckInput();
            //ChangeSprite();
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

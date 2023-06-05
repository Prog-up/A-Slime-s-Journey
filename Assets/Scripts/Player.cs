using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    public AudioSource MusicLvl1;
    public AudioSource MusicLvl2;
    public AudioSource MusicBoss;
    public Animator transition;

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
    public Transform LaunchInMenuset;
    public Transform LaunchInMenuset2;

    private bool Isrolling = false;
    public float CooldownDuration = 1.5f;
    public bool IsAvailable = true;

    private bool Hurt1 = false;
    private bool Hurt2 = false;
    private float timer;
    private float timer2;
    private bool InGameOverMenu = false;
    
    // Menu
    private bool InMenu;

    private void Escalade()
    {
        if (IsRock)
        {
            isTouchingWall = Physics2D.OverlapCircle(WallCheckRight.position, 0.1f, collisionLayers)||Physics2D.OverlapCircle(WallCheckLeft.position, 0.1f, collisionLayers);
            verticalInput = (Input.GetKey(GameManager.GM.power) && !InMenu)? 1 : 0;

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
        if (Input.GetKey(GameManager.GM.right) && !InMenu)
        {
            photonView.RPC("FlipFalse",PhotonTargets.AllBuffered);
            MoveForce = 1;
        }
        else if (Input.GetKey(GameManager.GM.left) && !InMenu)
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
            if (Input.GetKey(GameManager.GM.jump) && !InMenu)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                jumpsound.Play();
                anim.SetBool("Isjumping",!IsGrounded);
            }
        }
        anim.SetBool("Isjumping",!IsGrounded);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Level1")
        {
            MusicLvl2.Stop();
            MusicBoss.Stop();
            MusicLvl1.Play();
        }
        else if (other.tag == "Level2")
        {
            MusicLvl1.Stop();
            MusicBoss.Stop();
            MusicLvl2.Play();
        }
        else if (other.tag == "BossArea")
        {
            MusicLvl1.Stop();
            MusicLvl2.Stop();
            MusicBoss.Play();
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
        else if (other.tag == "GG")
        {
            IsRock = false;
            IsDefault = true;
            IsFlame = false;
            Damage.life = 3;
            transition.SetTrigger("Start");
            StartCoroutine(StartCooldown(true));
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
            if (Input.GetKey(GameManager.GM.power) && !InMenu)
            {
                Debug.Log("Tir !");
                if (sr.flipX)
                {
                    PhotonNetwork.InstantiateSceneObject(projectile.name, LaunchInMenuset2.position, new Quaternion(0f, 180f, 0f, 0f), 0, null);
                    //Instantiate(projectile, LaunchInMenuset.position, new Quaternion(0f, 180f, 0f, 0f));
                }
                else
                {
                    PhotonNetwork.InstantiateSceneObject(projectile.name, LaunchInMenuset.position, Quaternion.identity, 0, null);
                    //Instantiate(projectile, LaunchInMenuset2.position, Quaternion.identity);
                }
                
                StartCoroutine(StartCooldown());
            }
        }
    }
    
    public IEnumerator StartCooldown(bool telep = false)
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
        if (telep)
        {
            transform.position = new Vector3(380f, transform.position.y, transform.position.z);
            transition.SetTrigger("Restart");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.CompareTag("Rock") && Input.GetKey(GameManager.GM.transfo) && !InMenu)
        {
            IsRock = true;
            IsDefault = false;
            IsFlame = false;
            anim.SetBool("IsRock", IsRock);
        }
        else if (other.gameObject.CompareTag("Flame") && Input.GetKey(GameManager.GM.transfo) && !InMenu)
        {
            IsRock = false;
            IsDefault = false;
            IsFlame = true;
            anim.SetBool("IsFlame", IsFlame);
        }
        else if (!InMenu && ((other.gameObject.CompareTag("Rock") && !IsRock) ||
                             (other.gameObject.CompareTag("Flame") && !IsFlame)))
        {
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
    
    void Update()
    {
        if (transform.position.x < -80)
        {
            if (GameManager.GM.IsDead)
            {
                InMenu = true;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.Find("UI").gameObject.SetActive(false);
            }
            else
            {
                InMenu = false;
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                GetComponent<SpriteRenderer>().enabled = true;
                transform.Find("UI").gameObject.SetActive(true);
            }
        }
        else
        {
            InMenu = false;
        }
        
        if (photonView.isMine && photonView.gameObject.activeSelf)
        {
            CheckInput();
            InMenu = GameManager.GM.InOptions;
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

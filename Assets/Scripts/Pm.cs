using UnityEngine;
using Photon.Pun;

public class Pm : MonoBehaviourPunCallbacks
{

    //Declaration de ce qui permet au joueur de se deplacer
    private float horizontal;
    public float moveSpeed;
    public float jumpforce;

    //Declaration des booleans pour savoir si le perso est au sol
    private bool isJumping;

    //On declare le boolean qui gere le double jump
    private bool doubleJump;

    public Rigidbody2D rb;
    //private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;




    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

         if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() )
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

         private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}

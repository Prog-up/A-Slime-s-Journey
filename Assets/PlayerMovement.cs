using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    //Declaration de ce qui permet au joueur de se deplacer
    private float horizontal;
    private float Move;
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


    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            
        }
    }

    private void Update()
    {
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveSpeed * Move, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpforce));
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}

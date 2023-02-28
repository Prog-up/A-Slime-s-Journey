using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    //Declaration de ce qui permet au joueur de se deplacer
    private float horizontal;
    private float Move;
    public float moveSpeed;
    public float jumpforce;
    public float groundCheckRadius;

    public Transform groundCheck;
    public LayerMask collisionLayers;

    //Declaration des booleans pour savoir si le perso est au sol
    private bool isJumping;

    public Rigidbody2D rb;
  




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
            if(isGrounded())
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpforce));
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,collisionLayers);
    }
}

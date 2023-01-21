using UnityEngine;
using Photon.Pun;

public class Pm : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public float jumpforce;

    private bool isJumping;
    private bool IsGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;


    void FixedUpdate()
    {
        if(photonView.IsMine)
		    {
			       ProcessInput();
   		  }
    }
    void ProcessInput()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        IsGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        if (Input.GetButtonDown("Jump") && IsGrounded)
            {
                isJumping = false;
                rb.AddForce(new Vector2(0f, jumpforce));
            }
        Moveplayer(horizontalMovement);
    }
        
    }

    void Moveplayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity,ref velocity,.05f);

    }
}

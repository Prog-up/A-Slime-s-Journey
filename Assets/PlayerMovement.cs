using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public float jumpforce;

    private bool isJumping;
    private bool IsGrounded;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;


    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            ProcessInput();
        }
    }

    void ProcessInput()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;

        }

        Moveplayer(horizontalMovement);
    }



    void Moveplayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if (isJumping == true)
        {
            rb.AddForce(new Vector3(0f, jumpforce));
            isJumping = false;
        }
    }
}
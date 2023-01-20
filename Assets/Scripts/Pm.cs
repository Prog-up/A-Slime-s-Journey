using UnityEngine;

public class Pm : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;

    public bool isJumping = false;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Moveplayer(horizontalMovement);
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    void Moveplayer(float _horizontalMovement)
    {
        
        int count = 2;
        bool Isontheground = count == 2;
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity,ref velocity,.05f);
        if (count!=0)
        {
            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpforce));
                isJumping = false;
                count--;
            }

            if (Isontheground)
            {
                count = 2;
            }
            //TODO : Fix double jump
        }
        
    }
}

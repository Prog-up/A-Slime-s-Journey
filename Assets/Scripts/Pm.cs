using UnityEngine;

<<<<<<< HEAD:Assets/Scripts/Player.cs
public class Player : MonoBehaviour
=======
public class Pm : MonoBehaviour
>>>>>>> 0e6fbbd1e439064597f42543944f042a5f3ba686:Assets/Scripts/Pm.cs
{
    public float moveSpeed;


    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Moveplayer(horizontalMovement);
    }

    void Moveplayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
<<<<<<< HEAD:Assets/Scripts/Player.cs
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity,ref velocity,.05f);
=======
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity,ref velocity, .05f);
>>>>>>> 0e6fbbd1e439064597f42543944f042a5f3ba686:Assets/Scripts/Pm.cs
    }
}

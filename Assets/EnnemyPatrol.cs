using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
	public float speed;
	// public Transform[] waypoints;
	// private Transform target;
	// private int destPoint = 0;
	public bool Alive;
	private Quaternion target = Quaternion.Euler(0, 0, 0);

	public SpriteRenderer graphic;
	private int x = 1;
    // Start is called before the first frame update
    void Start()
    {
        // target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
		if (Alive)
		{
        // 	Vector3 dir = target.position - transform.position;
		// 	transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		// 	if (Vector3.Distance(transform.position, target.position) < 0.3f)
		// 	{
		// 		destPoint = (destPoint + 1)% waypoints.Length;
		// 		target = waypoints[destPoint];
		// 		graphic.flipX = !graphic.flipX;
		// 	}
			transform.position = new Vector3(transform.position.x + x * speed * Time.deltaTime, transform.position.y);
			// GetComponent<Rigidbody2D>().velocity = new Vector2(x*speed, GetComponent<Rigidbody2D>().velocity.y);
		}
    }

	// void OnTriggerEnter(Collision collision)
	// {
	// 	if (collision.collider.CompareTag("Waypoint"))
	// 	{
	// 		Debug.Log("ici");
    //         if (transform.rotation.y == 0)
    //         {
    //             // target = Quaternion.Euler(0, 180, 0);
	// 			x = -x;
	// 			graphic.flipX = true;
    //         }
    //         else
    //         {
    //             // target = Quaternion.Euler(0, 0, 0);
	// 			x = -x;
	// 			graphic.flipX = false;
    //         }
    //         // transform.rotation = target;
	// 	}
	// }
}

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
    

   
    void Update()
    {
		if (Alive)
		{
      
			transform.position = new Vector3(transform.position.x + x * speed * Time.deltaTime, transform.position.y);
		}
    }
}

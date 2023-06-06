using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShooting : MonoBehaviour
{
    public GameObject arrow;

    public Transform arrowPos;
	
	public Transform arrowPos2;

	public SpriteRenderer graphic;

    private float timer;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		var villager = transform.position;
		if (villager.x-player.transform.position.x > 0)
		{
			graphic.flipX = false;
		}
		else
		{
			graphic.flipX = true;
		}
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 25)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        var archer = transform.position;
		if (archer.x-player.transform.position.x > 0)
		{
			PhotonNetwork.InstantiateSceneObject(arrow.name, arrowPos.position, Quaternion.identity, 0, null);
		}
		else
		{
			PhotonNetwork.InstantiateSceneObject(arrow.name, arrowPos2.position, Quaternion.identity, 0, null);
		}
        
    }
}

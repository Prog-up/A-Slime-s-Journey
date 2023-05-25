using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 0.1f;
    public float minX;
    public float maxX;
    private Vector3 origin;

    void Start()
    {
        origin = new Vector3(minX, transform.position.y, transform.position.z);
    }

    void Update() // use instead .Lerp()
    {
        transform.position += transform.right * Time.deltaTime * speed;
        if (transform.position.x > maxX)
        {
            transform.position = origin;
        }
    }
}

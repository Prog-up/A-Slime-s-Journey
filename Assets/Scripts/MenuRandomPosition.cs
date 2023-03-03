using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRandomPosition : MonoBehaviour
{
    public float PosMin;
    public float PosMax;

    void Start()
    {
        transform.position = new Vector3(Random.Range(PosMin, PosMax), transform.position.y, transform.position.z);
    }
}

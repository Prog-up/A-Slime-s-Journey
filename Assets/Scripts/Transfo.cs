using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfo : MonoBehaviour
{
    
    public GameObject ToDestroy;
    public GameObject ToInstanciate;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToDestroy.SetActive(false);
            ToInstanciate.SetActive(true);
        }
    }
}

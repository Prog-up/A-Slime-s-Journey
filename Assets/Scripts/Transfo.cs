using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfo : MonoBehaviour
{
    
    public GameObject ToDestroy;
    public GameObject ToInstanciate;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Destroy(ToDestroy);
            Instantiate(ToInstanciate);
        }
    }
}

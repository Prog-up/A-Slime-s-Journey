using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDuplicator : MonoBehaviour
{
    public int numberObject;
    public GameObject OriginalObject;
    public GameObject ObjectContainer;

    void Start()
    {
        for (int i = 0; i < numberObject; i++)
        {
            GameObject CloneObject = Instantiate(OriginalObject);
            CloneObject.transform.parent = ObjectContainer.transform;
            CloneObject.name = OriginalObject.name + "-" + i;
        }
    }

}

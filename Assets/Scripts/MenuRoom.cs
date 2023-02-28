using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuRoom : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject button1;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    public void ButtonClicked() 
    {
        cameraTransform.Translate(7, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

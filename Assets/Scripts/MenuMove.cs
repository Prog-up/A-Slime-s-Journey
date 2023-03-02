using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMove : MonoBehaviour
{
    public Transform cameraTransform;
    public float move;
    public GameObject button0_1;
    public GameObject button0_2;
    public GameObject button0_3;
    public GameObject button0_4;
    public GameObject button1_1;
    public GameObject button1_2;
    public GameObject button1_3;
    public GameObject button1_4;
    private bool end = false;
    private bool right = false;
    private bool left = false;

    void Start()
    {
    // button1_1.SetActive(false);
    // button1_3.SetActive(false);
    // button1_2.SetActive(false);
    // button1_4.SetActive(false);
    }

    private void ButtonClicked()
    {
    button0_1.SetActive(false);
    button0_3.SetActive(false);
    button0_2.SetActive(false);
    button0_4.SetActive(false);
    }

    public void MoveToRight()
    {
        ButtonClicked();
        right = true;
    }

    public void MoveToLeft()
    {
        ButtonClicked();
        left = true;
    }

    void Update()
    {
        if (right)
        {
            if (cameraTransform.position.x < move)
            {
                cameraTransform.position += cameraTransform.right * Time.deltaTime * 10f;
            }
            else
            {
                right = false;
                end = true;
            }
        }
        else if (left)
        {
            if (cameraTransform.position.x > move)
            {
                cameraTransform.position -= cameraTransform.right * Time.deltaTime * 10f;
            }
            else
            {
                left = false;
                end = true;
            }
        }

        if (end)
        {
            button1_1.SetActive(true);
            button1_3.SetActive(true);
            button1_2.SetActive(true);
            button1_4.SetActive(true);
            end = false;
        }
    }
}

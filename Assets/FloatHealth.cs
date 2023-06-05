using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatHealth : MonoBehaviour
{

    public Slider slider;
    

    public void UpdateHealthBar(float current, float maxv)
    {
        slider.value = current / maxv;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}

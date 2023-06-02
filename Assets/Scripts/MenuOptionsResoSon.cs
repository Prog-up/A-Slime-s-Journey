using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuOptionsResoSon : MonoBehaviour
{

    public Dropdown resolution;

    public AudioSource audiosource;

    public Slider slider;
    public Text txtVolume;


    public bool IsFullScreen = true;
    public Text FScreen;

    void Start()
    {
        SetResolution();
        SliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(GameManager.GM.pause))
        {
            PhotonNetwork.LoadLevel("MainMenu");
        }
    }

    public void SetResolution()
    {
        switch(resolution.value)
        {
            case 0:
                Screen.SetResolution(640,360,IsFullScreen);
                break;
            case 1:
                Screen.SetResolution(1920,1080,IsFullScreen);
                break;
        }
    }

    public void ChangeFscreen()
    {
        IsFullScreen = !IsFullScreen;
        if(IsFullScreen)
        {
            FScreen.text = "Fullscreen";
        }
        else
        {
            FScreen.text = "Windowed";
        }
    }


    public void SliderChange()
    {
        audiosource.volume = slider.value;
        txtVolume.text = "Volume " + (audiosource.volume * 100).ToString("00") + "%";
    }
}

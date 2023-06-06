using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool TimeIsRunning;
    public float TimeRemaining;

    public TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        TimeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeIsRunning)
        {
            if (TimeRemaining >= 0)
            {
                TimeRemaining += Time.deltaTime;
                DisplayTime(TimeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text= string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerBar;
    float maxTime = 15f;
    public float timeLeft;
    public GameObject timesupText;


    void Start()
    {
        timesupText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft> 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {

            timesupText.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
}

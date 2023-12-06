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

    SceneSwitch sceneSwitch;


    void Start()
    {
        timesupText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;

        sceneSwitch= GetComponent<SceneSwitch>();
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
            
            Invoke("TimeUp", 2.0f);
            
        }
        
    }

    void TimeUp()
    {
        sceneSwitch.GoToMainScene();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    private float start, timer, timePaused;
    private bool paused;
    [SerializeField] private TextMeshProUGUI TimerDisplay;

    void Start()
    {
        timePaused = 0;
        start = Time.time;
        paused = false;
    }

    void Update()
    {
        if(!paused)
        {
            timer = Time.time - start - timePaused;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            TimerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timePaused = timePaused + (Time.time - timer);
        }
    }

    public void PauseTimer()
    {
        paused = true;
    }
}
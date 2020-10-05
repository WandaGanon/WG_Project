using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour
{
    public Text TimeText;
    public string TimeString;
    void Start()
    {
        
    }

    void Update()
    {
       Timer();
    }
    
    public void Timer()
    {
        DateTime now = DateTime.Now;
        TimeString = DateTime.Now.ToString("hh:mm:ss");
        TimeText.text = TimeString;
    }
    public void Pause ()
    {
        if (Time.timeScale == 1)
        {
        Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void ExitGame()
    {
        UnityEngine.Debug.LogError("Exit Game");
        Application.Quit(); 
    }
}

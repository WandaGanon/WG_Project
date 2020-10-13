using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [Header("Tiempo Opciones")]
    [Tooltip("Asignar Text para mostrar la hora local")]
    public Text TimerText;
    string TimerString;
    private void Update() 
    {
        LocalTimer();
    }
    public void LocalTimer()
    {
        DateTime now = DateTime.Now;
        TimerString = DateTime.Now.ToString("hh:mm:ss");
        TimerText.text = TimerString;
    }
}

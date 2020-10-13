using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour
{
    //ELEMENTOS Generales
    [Header("Ejecutar Opciones")]
    public KeyCode PauseKey = KeyCode.Escape;
    public GameObject PauseMenu;
    private void Start() 
    {
        if (PauseMenu.activeSelf == true && Time.timeScale == 0) 
        {
            PauseMenu.gameObject.SetActive (false);
            Time.timeScale = 1;
        }
        else
        {
            PauseMenu.gameObject.SetActive (false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(PauseKey) && PauseMenu.activeSelf == false )
            {
                Pause();
                PauseMenu.gameObject.SetActive (true);
                //activar cursor
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;
            }
        else if (Input.GetKeyDown(PauseKey) && PauseMenu.activeSelf == true)
            {
                Pause();
                PauseMenu.gameObject.SetActive (false);
                //desactivar cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
    }
    public void Pause()
    {
        // 1 es igual al tiempo normal que funciona unity
        // 0 es cuando se detiene el tiempo, se detiene todo lo que sea animado tambien
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void Continue()
    {
        Pause();
        PauseMenu.gameObject.SetActive (false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

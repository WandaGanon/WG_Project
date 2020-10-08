using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /* MENU
    public string play;
    public string settings;
    public string exit;
    public string menu;
    */
    [Header("Tiempo Opciones")]
    [Tooltip("Asignar Text para mostrar la hora local")]
    public Text Tiempo;
    string TiempoString;
    //ELEMENTOS Generales
    [Header("Ejecutar Opciones")]
    public KeyCode PauseKey = KeyCode.T;
    public GameObject PuaseMenu;
    private void Start() 
    {
        PuaseMenu.gameObject.SetActive (false);
    }
    void Update()
    {
        Timer();

        if (Input.GetKeyDown(PauseKey) && PuaseMenu.activeSelf == false )
            {
                Pause();
                PuaseMenu.gameObject.SetActive (true);
            }
        else if (Input.GetKeyDown(PauseKey) && PuaseMenu.activeSelf == true)
            {
                Pause();
                PuaseMenu.gameObject.SetActive (false);
            }
    }
    public void Timer()
    {
        DateTime now = DateTime.Now;
        TiempoString = DateTime.Now.ToString("hh:mm:ss");
        Tiempo.text = TiempoString;
    }
    public void Pause()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
  
    public void ExitGame()
    {
        UnityEngine.Debug.LogError("Exit Game");
        Application.Quit(); 
    }
    /* MENU
   public void LoadPlay()
    {
        SceneManager.LoadScene(play);
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene(settings);
    }
    public void LoadExit()
    {
        SceneManager.LoadScene(exit);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menu);
    }
*/
}

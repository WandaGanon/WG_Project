using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [Header("Cooldown")]
    [Tooltip("Tiempo de duracion del Cooldown")]
    public float TimeCD = 10;
    float currentTimeCD = 0f;
    [Tooltip("Tecla para Testear el CD")]
    public KeyCode CooldownKey = KeyCode.C;
    [SerializeField]
    [Tooltip("Insertar Texto que servira como valor numerico visual de CD")]
    Text countdownText;
    [Tooltip("Insertar Imagen de fondo para CD")]
    public Image cooldownImage;
    bool isCooldown = false;
    [Tooltip("Activar y Desactivar el GameObject, 'no puede ser a si mismo'")]
    public GameObject CooldownObj;

    private void Start() 
    {
        currentTimeCD = TimeCD;
        CooldownObj.gameObject.SetActive (false);
    }
    void Update() 
    {

        if (Input.GetKeyDown(CooldownKey) && currentTimeCD == TimeCD)
        {
            isCooldown = true;
            CooldownObj.gameObject.SetActive (true);
        }
        if (isCooldown == true)
        {
            CountdownF();
            CooldownF();
        }
        
    }
    void CooldownF()
    {
        cooldownImage.fillAmount +=1 / TimeCD * Time.deltaTime;
        if (cooldownImage.fillAmount >=1)
        {
            cooldownImage.fillAmount = 0;
            isCooldown = false;
        }

    }
    void CountdownF()
    {
        var TimeNow = currentTimeCD;
        //El codigo impide que el numero sea menor a 0, tambien detiene la cuenta regresiva en 0
        currentTimeCD -= 1 * Time.deltaTime;
        countdownText.text = currentTimeCD.ToString("0"); 
        var data1 = TimeNow.ToString("F0");// sin decimales
        var data2 = TimeNow.ToString("F1");// 1 decimales

        if(TimeNow <= 1)
        {
            countdownText.text = data2.ToString();
        }
        else
        {
            countdownText.text = data1.ToString();
        }
        if (currentTimeCD <= 0)
        {
            currentTimeCD = TimeCD;
            isCooldown = false;
            CooldownObj.gameObject.SetActive (false);
        }
    }
}

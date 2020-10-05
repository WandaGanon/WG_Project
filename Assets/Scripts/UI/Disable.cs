using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    [Header("Compass Option")]
    //ELEMENTOS DEL COMPASS CLASSIC
    [Tooltip("Tecla para activar o Desactivar Compass Classic")]
    public KeyCode CompassClassicKey = KeyCode.U;
    public GameObject CompassClassic;
    //ELEMENTOS DEL COMPASS QUEST
    [Tooltip("Tecla para activar o Desactivar Compass Quest")]
    public KeyCode CompassQuestKey = KeyCode.I;
    public GameObject CompassQuest;
    //
    void Start()
    {
        CompassClassic.gameObject.SetActive (false);
        CompassQuest.gameObject.SetActive (false);
    }
    void Update()
    {
        // ACTIVATE/DESACTIVATE COMPASS CLASSIC
          if (Input.GetKeyDown(CompassClassicKey) && CompassClassic.activeSelf == false)
        {
            CompassClassic.gameObject.SetActive (true);
            CompassQuest.gameObject.SetActive (false);
        }
        else if (Input.GetKeyDown(CompassClassicKey) && CompassClassic.activeSelf == true)
        {
            CompassClassic.gameObject.SetActive (false);
        }
        // ACTIVATE/DESACTIVATE COMPASS QUEST
        if (Input.GetKeyDown(CompassQuestKey) && CompassQuest.activeSelf == false)
        {
            CompassQuest.gameObject.SetActive (true);
            CompassClassic.gameObject.SetActive (false);
        }
        else if (Input.GetKeyDown(CompassQuestKey) && CompassQuest.activeSelf == true)
        {
            CompassQuest.gameObject.SetActive (false);
        }
    }
}

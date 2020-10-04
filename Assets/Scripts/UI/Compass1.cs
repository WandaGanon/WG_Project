using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass1 : MonoBehaviour
{
    public Transform player;
    public bool CompassC = false;
    public bool CompassQ = false;
    public GameObject Brujula1;
    public GameObject Brujula2;
    

    //CompassClassic elementos
    Vector3 vector;
    //CompassQuest elementos
    public RawImage compassImage;
    void Start()
    {
            //Brujula1.active = false;
            //Brujula2.active = false;
    }
    void Update()
    {

        CompastQuest();
        
        //CompastQuest();
    }
    public void CompassClassic()
    {
        if (CompassQ == false)
        {
            CompassC = true;
        };
        vector.z = player.eulerAngles.y;
        transform.localEulerAngles = vector;
    }
    public void CompastQuest()
    {
        if (CompassC == false)
        {
            CompassQ = true;
        };
        //uvRect usa los parametros seteados del imageRaw del objeto UI para manejar sus uvs
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
}

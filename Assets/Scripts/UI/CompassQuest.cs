using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassQuest : MonoBehaviour
{
    //CompassQuest elementos
    public RawImage compassImage;
    public Transform player;
    void Update()
    {
        CompastQuestF();
    }
    public void CompastQuestF()
    {
        //uvRect usa los parametros seteados del imageRaw del objeto UI para manejar sus uvs
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
}

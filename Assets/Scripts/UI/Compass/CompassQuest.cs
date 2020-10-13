using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassQuest : MonoBehaviour
{
    [Header("CompassQuest elementos basicos")]
    
    //TUTORIAL https://www.youtube.com/watch?v=MRAVwaGrmrk

    //-----CompassQuest elementos-----
    [Tooltip("ImagenRaw del compass que se movera")]
    public RawImage compassImage;
    [Tooltip("Posicion del Player")]
    public Transform player;
    //-----QUESTMARKERS ELEMENTOS-----
    [Tooltip("Posicion del IconPrefab o QuestIcon")]
    //aqui va el QuestIcon o IconPrefab que es la base donde se mostraran las imagenes de la libreria 
    public GameObject iconPrefab;
    float compassUnit;
    //Distancia maxima que percibe la brujula
    [Range(10,50)]
    [Tooltip("Rango de vision captado por el player")]
    public float maxDistance = 20f;
    List<QuestMarker> questMarkers = new List<QuestMarker>();
    //-----libreria de objetos que puede reconocer la brujula-----
    [Header("Libreria de objetos")]
    [Tooltip("Agregar QuestMarkers a la brujula")]
    //public string itemsTag = "Item";
    public QuestMarker Item1;
    public QuestMarker Item2;
    public QuestMarker Item3;
    public QuestMarker Item4;  
    //ArrayItems Pibote;
    private void Start() 
    {
        // es la unidad que usaran los questmarkers basandose en el tamaño del compass
        compassUnit = compassImage.rectTransform.rect.width / 360f;
/*         var all = Pibote.allchildren;
        Debug.Log(all);
        Debug.Log(all[1]); */

        // Lista de objetos que reconoce la brujula
        AddQuestMarker(Item1);
        AddQuestMarker(Item2);
        AddQuestMarker(Item3);
        AddQuestMarker(Item4);   
    }

    private void Update()
    {   
        //ItemsQuest = GameObject.FindGameObjectsWithTag("item");
        
        //----- BRUJULA QUEST SIN ICONOS SOLO NECESITA DEL CompastQuestF() y nada mas-----
        CompastQuestF();
         //----- CODIGO DE QUESTMARKER-----
        foreach (QuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
            float dst = Vector2.Distance (new Vector2(player.transform.position.x, player.transform.position.z), marker.position);
            float scale = 0f;
            if (dst < maxDistance)
                scale = 1f - (dst / maxDistance);
            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    //----- CODIGO ESCENCIAL DE LA BRUJUGA QUEST -----
    public void CompastQuestF()
    {
        //uvRect usa los parametros seteados del imageRaw del objeto UI para manejar sus uvs
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
    // CODIGO DE QUESTMARKER
    public void AddQuestMarker (QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questMarkers.Add(marker);
    }
    Vector2 GetPosOnCompass (QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
       
}

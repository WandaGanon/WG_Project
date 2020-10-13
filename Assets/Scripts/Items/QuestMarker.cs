using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour
{
    // el icono se le puede dar una imagen atraves del inspector
    [Tooltip("Posicion del Icono")]
    public Sprite icon;
    //se setea en codigo por lo que no hace falta que se utilice, pero debe ser publica
    [Tooltip("No hay necesidad de rellenar")]
    public Image image;
    
    //
    public Vector2 position
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }
}

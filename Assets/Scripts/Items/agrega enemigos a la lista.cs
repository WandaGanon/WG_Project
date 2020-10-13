using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item1 : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>(); //The enemies player detection collider is touching
    public GameObject closestEnemyInDirection; // The closest enemy in the direction the player is facing
    public Transform body; //The body the player using to find as the player

    private void Start()
    {
        
    }

    public void FindClosestGameObject()
    {
        GameObject closest = null;
        float distance = 41.0f;
        Vector3 position = body.transform.position;
        foreach (GameObject go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        closestEnemyInDirection = closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (other.gameObject == enemies[i])
                {
                    return;
                }
            }
            enemies.Add(other.gameObject);
            FindClosestGameObject();
            Debug.Log("ye");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (other.gameObject == enemies[i])
                {
                    if(enemies[i] == closestEnemyInDirection)
                    {
                        closestEnemyInDirection = null;
                    }
                    enemies.RemoveAt(i);
                }
            }
        }
    }
}


/* void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            for(int i = 0; i < Item.Count; i++)
            {
                if(other.gameObject == Item[i])
                {
                    return;
                }
            }
        }
    }
    void ItemCount(Collider other)
    {
        if (other.tag == "Item")
        {
            for(int i = 0; i < Item.Count; i++)
            {
                if(other.gameObject == Item[i])
                {
                    if (Item[i] == closetItemInDirection)
                    {
                        closetItemInDirection = null;
                    }
                    Item.RemoveAt(i);
                }
            }
        }
    } */
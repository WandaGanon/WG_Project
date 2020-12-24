using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    public int openSide;
    private int rand;

    private bool spawned = false;

    // si es 1 es N
    // si es 2 es O
    // si es 3 es S
    // si es 4 es E

private RoomTemplate Template;

    private void Start() {
        Template = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


    void Spawn(){

        if (spawned == false)
        {
            if (openSide == 1)
            {
                rand = Random.Range(0 , Template.S.Length);
                Instantiate(Template.S[rand], transform.position, Template.S[rand].transform.rotation);
            }
            else if(openSide == 2){
                rand = Random.Range(0 , Template.E.Length);
                Instantiate(Template.E[rand], transform.position, Template.E[rand].transform.rotation);
            }
            else if(openSide == 3){
                rand = Random.Range(0 , Template.N.Length);
                Instantiate(Template.N[rand], transform.position, Template.N[rand].transform.rotation);
            }
            else if(openSide == 4){
                rand = Random.Range(0 , Template.O.Length);
                Instantiate(Template.O[rand], transform.position, Template.O[rand].transform.rotation);
            }    
        }
        spawned = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour{
    
    public GameObject player;
    Vector3 position;

    void Start(){
        
    }

    void Update(){
    	gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent; 
    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        // agent.destination = goal.position; 
        // print(PlayerController.instance);
    }

    // Update is called once per frame
    void Update() {
        print("player transform: " + PlayerController.instance.transform.position);
        agent.destination = PlayerController.instance.transform.position; 
    }

}
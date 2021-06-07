using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //NAV Stuff
    [SerializeField] float keepAwayDist = 15f;
    [SerializeField] float speed = 3.5f;
    NavMeshAgent agent; 

    //ATTACK stuff
    [SerializeField] float attackRate = 3f;
    float nextAttackTime = 0;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = keepAwayDist;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update() {
        agent.destination = PlayerController.instance.transform.position;

        if(Time.time > nextAttackTime) {
            nextAttackTime = Time.time + attackRate;
            // print("ATTACK!");
        }
    }

}
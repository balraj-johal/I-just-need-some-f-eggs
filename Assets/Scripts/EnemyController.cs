using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //NAV Stuff
    [SerializeField] float keepAwayDist = 15f;
    [SerializeField] float speed = 3.5f;
    // NavMeshAgent agent; 

    //ATTACK stuff
    [SerializeField] float attackRate = 3f;
    float nextAttackTime = 0;

    //ENEMY stuff
    public int hp = 100;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        // agent = GetComponent<NavMeshAgent>();
        // agent.stoppingDistance = keepAwayDist;
        // agent.speed = speed;
    }

    // Update is called once per frame
    void Update() {
        DeathCheck();

        //move navmesh agent
        // agent.destination = PlayerController.instance.transform.position;

        //attack timer
        if(Time.time > nextAttackTime) {
            nextAttackTime = Time.time + attackRate;
            // print("ATTACK!");
        }
    }

    //DAMAGE FUNCTIONS
    public void TakeDamage(int damage) {
        hp -= damage;
        Debug.Log("NEW HP: " + hp);
    }
    public void DeathCheck() {
        if (hp <= 0) {
            Destroy(gameObject);
        }
    }
    public void ApplyForce(Vector3 bulletVelocity) {
        print("applyingforce");
        bulletVelocity = bulletVelocity;
        rb.AddForce(bulletVelocity.x, 0, bulletVelocity.z);
    }
}
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
    NavMeshAgent agent;
    //ATTACK stuff
    [SerializeField] float attackRate = 3f;
    float nextAttackTime = 0;

    //ENEMY stuff
    public int hp = 100;
    Rigidbody rb;
    EnemyShoot shooter;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        shooter = GetComponent<EnemyShoot>();
        agent.destination = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update() {
        DeathCheck();

        //move navmesh agent
        agent.destination = PlayerController.instance.transform.position;

        //attack timer
        if(Time.time > nextAttackTime) {
            nextAttackTime = Time.time + attackRate;
            shooter.Shoot();
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
        rb.AddForce(bulletVelocity.x, 0, bulletVelocity.z);
    }
}
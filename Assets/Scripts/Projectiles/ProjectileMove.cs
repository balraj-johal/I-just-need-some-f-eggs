using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    Rigidbody rb;
    public float shotSpeed = 600.0f;
    public int damage = 10;

    private string[] enemyTags = {
        "Cereal",
        "Milk"
    };
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Player") && collider.gameObject.tag != "PlayerProj")
        {
            // Debug.Log("NOT PLAYER " + collider.gameObject.tag);
            HitSomething(collider);
        } else {
            // Debug.Log("PLAYER");
        }
    }

    void HitSomething(Collider collider) {
        // ContactPoint contact = collision.contacts[0];
        // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        // Vector3 position = contact.point;
        // Instantiate(explosionPrefab, position, rotation);
        Vector3 bulletVelocity = rb.velocity;
        if (System.Array.Exists(enemyTags, elem => elem == collider.transform.tag)) {
            collider.gameObject.GetComponent<EnemyController>().ApplyForce(bulletVelocity);
            collider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        Destroy(gameObject); //destroy Self
    }

    public void InitialRelativeForce(Vector3 force) {
        
    }
}
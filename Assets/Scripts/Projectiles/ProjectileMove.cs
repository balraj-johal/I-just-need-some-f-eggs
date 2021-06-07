using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    Rigidbody rb;
    public float shotSpeed = 600.0f;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // rb.AddForce(transform.forward * speed);
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Debug.Log("NOT PLAYER");
            HitSomething(collider);
        } else {
            Debug.Log("PLAYER");
        }
    }

    void HitSomething(Collider collider) {
        // ContactPoint contact = collision.contacts[0];
        // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        // Vector3 position = contact.point;
        // Instantiate(explosionPrefab, position, rotation);
        Vector3 bulletVelocity = rb.velocity;
        if (collider.transform.tag == "Cereal") {
            collider.gameObject.GetComponent<EnemyController>().ApplyForce(bulletVelocity);
            collider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        Destroy(gameObject); //destroy Self
    }
}
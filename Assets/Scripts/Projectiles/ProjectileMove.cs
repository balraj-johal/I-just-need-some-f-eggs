using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    Rigidbody rb;
    public float shotSpeed = 600.0f;

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

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Debug.Log("NOT PLAYER");
            HitSomething(collision);
        } else {
            Debug.Log("PLAYER");
        }
    }

    void HitSomething(Collision collision) {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        // Instantiate(explosionPrefab, position, rotation);
        Destroy(gameObject);
    }
}

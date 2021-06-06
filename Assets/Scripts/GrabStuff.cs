using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStuff : MonoBehaviour
{

    public float distance = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrab();
    }

    void CheckGrab() {
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Linecast(ray.origin, ray.origin + ray.direction * distance, out hit))
            {
                BoxCollider bc = hit.collider as BoxCollider;
                GameObject target = bc.gameObject;
                if (target.tag == "Cereal") {
                    Destroy(target);
                    PlayerController.instance.ChangeWeapon(target.tag);
                    // other.GetComponent<EnemyController>().CallYourFunctionHere();
                }
             }
        }
    }
}

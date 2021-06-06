using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStuff : MonoBehaviour
{
    private string currentWeapon;

    public float distance = 10;
    private int enemyLayerMask = 1 << 3;

    // Start is called before the first frame update
    void Start() {
        currentWeapon = PlayerController.instance.GetCurrentWeapon();
    }

    // Update is called once per frame
    void Update() {
        if (currentWeapon != PlayerController.instance.GetCurrentWeapon()) {
            currentWeapon = PlayerController.instance.GetCurrentWeapon();
        }
        CheckGrab();
    }

    void CheckGrab() {
        if (Input.GetButtonDown("Fire1") && currentWeapon == "None") {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, new Color(1.0f, 0.5f, 1.0f));
            if (Physics.Linecast(ray.origin, ray.origin + ray.direction * distance, out hit, enemyLayerMask)) {
                BoxCollider bc = hit.collider as BoxCollider;
                GameObject target = bc.gameObject;
                Destroy(target);
                PlayerController.instance.ChangeWeapon(target.tag);
            }
        }
    }
}

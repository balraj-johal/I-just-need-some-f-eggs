using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkShoot : MonoBehaviour
{
    public int ammo;
    public int maxAmmo = 12;
    public float rateOfFire = 1; //per second
    private float nextTimeToFire = 0f;
    public float gravRate;
    public float shotSpeed = 100000f;
    public int dmg;

    public Transform projectile;
    public Transform endPoint;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo > 0) {
            nextTimeToFire = Time.time + 1f/rateOfFire;
            Shoot();
        }
        if (ammo <= 0) {
            PlayerController.instance.ChangeWeapon("None");
        }
    }

    void Shoot () {
        // Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 12);
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Transform bullet = Instantiate(projectile, endPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(endPoint.transform.forward * 1000f);

        ammo --;
        // print("SHOOT");
    }

    
}

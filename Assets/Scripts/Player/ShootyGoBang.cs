using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGoBang : MonoBehaviour
{
    public string gunType;
    public int bulletsPerShot = 5;
    public float bulletSpread = 0.1f;
    public int ammo;
    public int maxAmmo = 12;
    public float rateOfFire = 1; //per second
    private float nextTimeToFire = 0f;
    public float gravRate;
    public float shotSpeed = 10000f;
    public int dmg;

    public Transform projectile;
    public Transform endPoint;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        print("onenabel");
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
        Transform bullet;
        
        for (int i = 0; i < bulletsPerShot; i++)
        {
            // bullet = Instantiate(projectile, endPoint.transform.position, Quaternion.identity);
            bullet = Instantiate(projectile, endPoint.transform.position, Quaternion.Lerp(Quaternion.identity, Random.rotation, bulletSpread));
            bullet.GetComponent<Rigidbody>().AddRelativeForce(endPoint.transform.forward * 1000f);
            ammo --;
        }
        // for (int i = 0; i < bulletsPerShot; i++)
        // {
        //     var offset = Random.Range(0.0f, 1f);
        //     bullet = Instantiate(projectile, endPoint.transform.position, Quaternion.identity * Quaternion.Lerp(Random.rotation, Quaternion.identity, bulletSpread));
        //     bullet.GetComponent<Rigidbody>().AddForce(endPoint.transform.forward * shotSpeed);
        //     ammo --;
        // }
    }

    public void ReloadAmmo() {
        ammo = maxAmmo;
    }
    
}

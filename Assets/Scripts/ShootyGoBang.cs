using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGoBang : MonoBehaviour
{
    public CameraShake cameraShake;
    public float camShakeMagnitude = 1f;

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

    Rigidbody rb;

    public Transform projectile;
    public Transform endPoint;
    public Transform emptyPrefab;
    public float throwForce = 1250f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ammo = maxAmmo;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        // print("onenabel");
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
            Transform thrownEmpty = Instantiate(emptyPrefab, endPoint.transform.position, Quaternion.identity);
            thrownEmpty.GetChild(0).GetComponent<Rigidbody>().AddForce(endPoint.transform.forward * throwForce);

            cameraShake.ReturnCam();
        }
    }

    void Shoot () {
        // Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 12);
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);#
        StartCoroutine(cameraShake.Shake(.15f, camShakeMagnitude));
        Transform bullet;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            // print("player vel: " + rb.velocity.magnitude);
            bullet = Instantiate(projectile, endPoint.transform.position, Quaternion.Lerp(Quaternion.identity, Random.rotation, bulletSpread));
            bullet.GetComponent<Rigidbody>().AddRelativeForce(endPoint.transform.forward * 1000f);
            ammo --;
        }
    }

    public void ReloadAmmo() {
        ammo = maxAmmo;
    }

    // IEnumerator FinishWeapon(string currentWeapon) {
    //     yield return new WaitForSeconds(2);
    // }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int bulletsPerShot = 5;
    public float bulletSpread = 0.1f;
    public float gravRate;
    public float shotSpeed = 10000f;
    public int dmg;

    public Transform projectile;
    public Transform endPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot () {
        // print("SHOOT");
        Transform bullet;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            bullet = Instantiate(projectile, endPoint.transform.position, Quaternion.Lerp(Quaternion.identity, Random.rotation, bulletSpread));
            bullet.GetComponent<Rigidbody>().AddRelativeForce(endPoint.transform.forward * 1000f);
        }
    }
    
}
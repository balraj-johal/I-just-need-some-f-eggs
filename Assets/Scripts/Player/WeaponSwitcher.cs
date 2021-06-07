using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private string currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = PlayerController.instance.GetCurrentWeapon();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != PlayerController.instance.GetCurrentWeapon()) {
            currentWeapon = PlayerController.instance.GetCurrentWeapon();
            SelectWeapon();
        }
    }

    void SelectWeapon() {
        print("current weapin is " + currentWeapon);
        int targetIndex = 0;
        switch (currentWeapon) {
            case "None":
                targetIndex = 0;
                break;
            case "Cereal":
                targetIndex = 1;
                break;
            case "Milk":
                targetIndex = 2;
                break;
            default:
                break;
        }

        int index = 0;
        foreach (Transform weapon in transform) {
            if (index == targetIndex) {
                weapon.gameObject.SetActive(true);
                // weapon.gameObject.GetComponent<ShootyGoBang>().ReloadAmmo();
            } else {
                weapon.gameObject.SetActive(false);
            }
            index++;
        }
    }
}

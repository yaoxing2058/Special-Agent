using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public GameObject currentWeapon;
    public Ammo ammoNumber;
    private TextMeshProUGUI ammoNum;

    // Start is called before the first frame update
    void Start()
    {
        ammoNum = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void GetCurrentWeapon() {
        GameObject weapons = GameObject.FindWithTag("Weapon Holder");
        if (weapons != null) {
        Transform weaponHolder = weapons.transform;

        for (int i = 0; i < weaponHolder.childCount; i++) {
            GameObject wp = weaponHolder.GetChild(i).gameObject;
            if (wp.activeSelf) {
                currentWeapon = wp;
            }
        }
        if (currentWeapon != null) {
            ammoNumber = currentWeapon.GetComponent<Ammo>();
        }
        if (ammoNum != null && ammoNumber != null) {
        ammoNum.SetText("Ammo: " + ammoNumber.ammo.ToString() + " / " + ammoNumber.remainingAmmo.ToString());
        }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentWeapon();
    }
}

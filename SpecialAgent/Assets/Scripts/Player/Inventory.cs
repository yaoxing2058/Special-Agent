using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int pistolAmmo;
    [SerializeField]
    private int rifleAmmo;
    // Inventory HUD
    private TextMeshProUGUI inventory;
    private bool opened;

    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponent<TextMeshProUGUI>();
        opened = false;
    }

    // Get Current Weapon
     public GameObject GetCurrentWeapon() {

        GameObject currentWeapon = null;

        GameObject weapons = GameObject.FindWithTag("Weapon Holder");

        if (weapons == null) {
            return null;
        }
       
        Transform weaponHolder = weapons.transform;

        for (int i = 0; i < weaponHolder.childCount; i++) {
            GameObject wp = weaponHolder.GetChild(i).gameObject;
            if (wp.activeSelf) {
                currentWeapon = wp;
            }
        }

        return currentWeapon;
    }

    public GameObject FindWeapon(string weaponName) {

        GameObject result = null;
        GameObject weapons = GameObject.FindWithTag("Weapon Holder");
        Transform weaponHolder = weapons.transform;
        
        for (int i = 0; i < weaponHolder.childCount; i++) {
            GameObject wp = weaponHolder.GetChild(i).gameObject;
            if (wp.name == weaponName) {
                result = wp;
                break;
            }
        }

        return result;

    }

    public void addItem(string itemName, int itemValue) {

        if (itemName == "Pistol Ammo") {

            Ammo pistolAmmo = FindWeapon("Pistol").GetComponent<Ammo>();
            pistolAmmo.remainingAmmo += itemValue;

        }
        
        if (itemName == "Rifle Ammo") {

            Ammo rifleAmmo = FindWeapon("Assault Rifle").GetComponent<Ammo>();
            rifleAmmo.remainingAmmo += itemValue;

        }

    }

    void ToggleInventory() {

        if (!opened && Time.timeScale != 0) {
            if (Input.GetKeyDown(KeyCode.I)) {
                inventory.SetText("Inventory"+ "\n\nPistol Ammo: " + pistolAmmo + "\n\nRifle Ammo: " + rifleAmmo);
                opened = true;
            }
        }

        else if (opened && Time.timeScale != 0) {
            if (Input.GetKeyDown(KeyCode.I)) {
                inventory.SetText("");
                opened = false;
            }
        }

    }

    void UpdateInventory() {
        if (opened && Time.timeScale != 0) {
            inventory.SetText("Inventory"+ "\n\nPistol Ammo: " + pistolAmmo + "\n\nRifle Ammo: " + rifleAmmo);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        Transform weaponHolder = GameObject.FindWithTag("Weapon Holder").transform;

        for (int i = 0; i < weaponHolder.childCount; i++) {
            GameObject wp = weaponHolder.GetChild(i).gameObject;
            if (wp.name == "Pistol") {
                pistolAmmo = wp.GetComponent<Ammo>().remainingAmmo;
            }
            if (wp.name == "Assault Rifle") {
                rifleAmmo = wp.GetComponent<Ammo>().remainingAmmo;
            }
        }
        ToggleInventory();
        UpdateInventory();
    }
}

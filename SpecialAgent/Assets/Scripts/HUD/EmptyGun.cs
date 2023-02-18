using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGun : MonoBehaviour
{
    // Current Weapon
    private GameObject currWeapon;
    private Ammo bullets;
    private AudioSource emptyGunSound;
    public GameObject reloadText;

   

    // Start is called before the first frame update
    void Start()
    {
        emptyGunSound = gameObject.GetComponent<AudioSource>();
    }

    void GetCurrentWeapon() {

        GameObject weapons = GameObject.FindWithTag("Weapon Holder");

        if (weapons != null) {

            Transform weaponHolder = weapons.transform;

            for (int i = 0; i < weaponHolder.childCount; i++) {
                GameObject wp = weaponHolder.GetChild(i).gameObject;
                if (wp.activeSelf) {
                    currWeapon = wp;
                }
                else if (wp == null) {
                    return;
                }
            }
            if (currWeapon != null) {
                bullets = currWeapon.GetComponent<Ammo>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.GetCurrentWeapon();
       
        if (bullets != null && bullets.isEmpty) {
            if (currWeapon.name != "Minigun") {
                reloadText.SetActive(true);
            }
            
            if (Input.GetMouseButtonDown(0)) {
                emptyGunSound.PlayOneShot(emptyGunSound.clip);
            }
        }
        else {
            reloadText.SetActive(false);
        }
    }
}

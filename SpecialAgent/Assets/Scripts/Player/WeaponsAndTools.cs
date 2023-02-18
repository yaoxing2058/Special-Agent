using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsAndTools : MonoBehaviour
{
    private bool switchWeapon;
    private bool torchOn;

    [SerializeField]
    private GameObject currentWeapon;
    [SerializeField]
    private int remainingAmmo;
    private ArrayList weaponInventory = new ArrayList();
    [Header("Pick-Up Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    
    [Header("Non Pick-Up Weapons")]
    public GameObject weapon3;
    public GameObject weapon4;

    private GameObject wp1_icon, wp2_icon, wp3_icon, wp4_icon;

    [Header("Flashlight")]
    public GameObject torch;

    // disable ammo display while using knife
    [Header("Display Remaining Ammo")]
    public GameObject AmmoDisplay;

    // Start is called before the first frame update
    void Start()
    {
      weapon1.SetActive(true);
      weapon2.SetActive(false);
      weapon3.SetActive(false);
      weapon4.SetActive(false);

      // Weapon Icon Name Format: WeaponName_Icon
      wp1_icon = GameObject.Find(weapon1.name + "_" + "Icon");
      wp2_icon = GameObject.Find(weapon2.name + "_" + "Icon");
      wp3_icon = GameObject.Find(weapon3.name + "_" + "Icon");
      wp4_icon = GameObject.Find(weapon4.name + "_" + "Icon");

      wp1_icon.SetActive(true);
      wp2_icon.SetActive(false);
      wp3_icon.SetActive(false);
      wp4_icon.SetActive(false);

      torchOn = false;
      torch.SetActive(false);
      AmmoDisplay.SetActive(true);
    }

    // Get Current Weapon
     public GameObject GetCurrentWeapon() {

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

    // Return true/false whether specific state is playing in specific animator
    // Get the animator from weapon gameobject tagged as "WeaponName Functions" as the first argument
    // Check this animator for specific state in the animator controller
    private bool AnimatorIsPlaying(Animator anim, string stateName) {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime 
        && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public bool isReloading() {

        if (GetCurrentWeapon().name == "Knife" || GetCurrentWeapon().name == "Minigun") {
            return false;
        }

        string currentWeaponName = GetCurrentWeapon().name;
        Animator reloader = GameObject.FindWithTag(currentWeaponName + " " + "Functions").GetComponent<Animator>();
        if (reloader == null) {
            return false;
        }
        return AnimatorIsPlaying(reloader, "Reload");
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

   

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentWeapon() == null) {
            return;
        }

        weaponInventory = new ArrayList() {new ArrayList() {weapon1, remainingAmmo}, new ArrayList() {weapon2, remainingAmmo}};

        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) 
        && !Input.GetMouseButton(1) && !isReloading() && !AnimatorIsPlaying(FindWeapon("Knife").GetComponent<Animator>(), "Attack")
        && Time.timeScale != 0) {
            
            if (Input.GetKeyDown(KeyCode.Q)) {
                switchWeapon = !switchWeapon;
                weapon1.SetActive(!switchWeapon);
                wp1_icon.SetActive(!switchWeapon);
                weapon2.SetActive(switchWeapon);
                wp2_icon.SetActive(switchWeapon);
                weapon3.SetActive(false);
                wp3_icon.SetActive(false);
                weapon4.SetActive(false);
                wp4_icon.SetActive(false);
                AmmoDisplay.SetActive(true);
               
            }
            if (Input.GetKeyDown(KeyCode.C)) {
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(true);
                wp3_icon.SetActive(true);
                weapon4.SetActive(false);
                wp1_icon.SetActive(false);
                wp2_icon.SetActive(false);
                wp4_icon.SetActive(false);
                AmmoDisplay.SetActive(true);
               
            }
            if (Input.GetKeyDown(KeyCode.F)) {
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(false);
                wp1_icon.SetActive(false);
                wp2_icon.SetActive(false);
                wp3_icon.SetActive(false);
                weapon4.SetActive(true);
                wp4_icon.SetActive(true);
                AmmoDisplay.SetActive(false);
                
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                torchOn = !torchOn;
                torch.SetActive(torchOn);
            }
        }
    }
}

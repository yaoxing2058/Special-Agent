using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo;
    public int remainingAmmo;
    private int fullAmmo;
    public bool isEmpty;
    public bool isReloading = false;
    private AudioSource reloadSound;
    [Header("Gun functions to turn on/off (Pistol Only)")]
    public GameObject[] func;

    public enum Mode {
        SemiAuto, FullAuto
    }

    [SerializeField]
    Mode firingMode = new Mode();
    private Recoil recoil;
   

    // Start is called before the first frame update
    void Start()
    {
        fullAmmo = ammo;
        reloadSound = GameObject.FindWithTag("Reload").GetComponent<AudioSource>();
        isEmpty = false;
        

    }

    void CheckFunc() {
        // Check whether the gun can fire

        if (firingMode == Mode.SemiAuto && GameObject.FindWithTag("Semi Auto") != null) {
            recoil = GameObject.FindWithTag("Semi Auto").GetComponent<Recoil>();
        }

        if (firingMode == Mode.FullAuto && GameObject.FindWithTag("Full Auto") != null) {
            recoil = GameObject.FindWithTag("Full Auto").GetComponent<Recoil>();
        }

        if (recoil != null) {

            if (ammo >= 1) {
                for (int i = 0; i < func.Length; i++) {
                    func[i].SetActive(true);
                }
                recoil.enabled = true;
                isEmpty = false;
            }

            if (ammo == 0) {  // Disable Firing, Recoil and other gun functions when current gun doesn't have ammo
                for (int i = 0; i < func.Length; i++) {
                    func[i].SetActive(false);
                }
                recoil.enabled = false;
                isEmpty = true;
            }

        }
    }

    // Return true/false whether specific state is playing in specific animator
    private bool AnimatorIsPlaying(Animator anim, string stateName) {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime 
        && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    // Get Animator by GameObject tag
    private Animator GetAnimator(string animatorTag) {
        return GameObject.FindWithTag(animatorTag).GetComponent<Animator>();
    }

    void Reload() {

         // Reference: Pistol Reload Animation Components
        // GameObject pistolFuncs = GameObject.FindWithTag("Pistol Functions");
        // Gun_Trigger gtr = pistolFuncs.GetComponent<Gun_Trigger>();
        // Animator pistolReload = gtr.GetComponent<Animator>();

        // Get Current Weapon
        WeaponsAndTools wat = GameObject.Find("WeaponHolder").GetComponent<WeaponsAndTools>();
        GameObject currentWeapon = wat.GetCurrentWeapon();

        // No Reload for Minigun
        if (currentWeapon.name == "Minigun" || currentWeapon.name == "Knife") {
            return;
        }


        if (ammo < fullAmmo) {
            if (Input.GetKeyDown(KeyCode.R)) {

                // This part is for pistol (including reload animation)
                if (firingMode == Mode.SemiAuto) {

                    // Pistol Reload Animation Components
                    GameObject pistolFuncs = GameObject.FindWithTag("Pistol Functions");
                    Gun_Trigger gtr = pistolFuncs.GetComponent<Gun_Trigger>();
                    Animator pistolReload = gtr.GetComponent<Animator>();
                    if (remainingAmmo > 0) {
                        if (remainingAmmo >= fullAmmo) {
                            int refill = fullAmmo - ammo;
                            ammo += refill;
                            remainingAmmo -= refill;
                        }
                        else if (ammo + remainingAmmo <= fullAmmo) {
                            ammo = ammo + remainingAmmo;
                            remainingAmmo -= remainingAmmo;
                        }
                        else if (remainingAmmo < fullAmmo) {
                            int fill = fullAmmo - remainingAmmo;
                            while (ammo + fill > fullAmmo) {
                                fill -= 1; 
                            }
                            ammo += fill;
                            remainingAmmo -= fill;
                        }
                        pistolReload.CrossFade("Reload", 0.5f);
                        reloadSound.PlayOneShot(reloadSound.clip);
                        isEmpty = false;
                    }
                } 
  

                // This part is for Full Auto Weapons Only
                else if (firingMode == Mode.FullAuto) {


                    if (remainingAmmo > 0) {
                        if (remainingAmmo >= fullAmmo) {
                            int refill = fullAmmo - ammo;
                            ammo += refill;
                            remainingAmmo -= refill;
                            MachineGunReload(ammo);
                        }
                        else if (ammo + remainingAmmo <= fullAmmo) {
                            ammo = ammo + remainingAmmo;
                            remainingAmmo -= remainingAmmo;
                            MachineGunReload(ammo);
                        }
                        else if (remainingAmmo < fullAmmo) {
                            int fill = fullAmmo - remainingAmmo;
                            while (ammo + fill > fullAmmo) {
                                fill -= 1; 
                            }
                            ammo += fill;
                            remainingAmmo -= fill;
                            MachineGunReload(ammo);
                        }
                        // Get and Play AssaultRifle Reload Animation
                        if (GameObject.FindWithTag("Assault Rifle Functions") != null) {
                            Animator arReload = GameObject.FindWithTag("Assault Rifle Functions").GetComponent<Animator>();
                            arReload.Play("Reload");
                        }
                        reloadSound.PlayOneShot(reloadSound.clip);
                        isEmpty = false;
                    }
                }
                
                
            }
        }
    }

    // A function reloads single/multi-barrel automatic weapons
    void MachineGunReload(int reloadAmount) {

        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Automatic");
        for (int i = 0; i < barrels.Length; i++) {
            barrels[i].GetComponent<MachineGunFiring>().ammo = reloadAmount;
            Debug.Log("Reloaded! " + barrels[i].GetComponent<MachineGunFiring>().ammo.ToString());
            } 

    }

    // Update is called once per frame
    void Update()
    {
        if (remainingAmmo < 0) {
            remainingAmmo = 0;
        }
        Reload();
        CheckFunc();

        // Check whether the game is paused
        if (Time.timeScale != 0) {

            if (firingMode == Mode.SemiAuto) {
                if (Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0)) {
                    if (ammo > 0) {
                        ammo -= 1;
                    }
                }
            }

            if (firingMode == Mode.FullAuto) {
                if (Input.GetButton("Fire1") || Input.GetMouseButton(0)) {
                    if (ammo > 0) {
                        ammo = GameObject.FindWithTag("Automatic").GetComponent<MachineGunFiring>().ammo;
                    }
                }
            }
        }
    }

}

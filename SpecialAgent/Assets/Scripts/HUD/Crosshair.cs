using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private CanvasGroup cg;
    private GameObject currentWeapon;
    public bool isHidden;
    public GameObject[] noCrosshairWeapons;

    // Start is called before the first frame update
    void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        currentWeapon = GameObject.Find("WeaponHolder").GetComponent<WeaponsAndTools>().GetCurrentWeapon();
        isHidden = false;
    }

    private void HideCrosshair() {
        if (Input.GetKeyDown(KeyCode.O)) {
            isHidden = !isHidden;
        }
    }

    private bool noCrosshair() {

        // returns true if current weapon doesn't have crosshair

        if (currentWeapon == null || noCrosshairWeapons == null) {
            Debug.Log("Current Weapon: null");
            return false;
        }

        for (int i = 0; i < noCrosshairWeapons.Length; i++) {

            if (noCrosshairWeapons[i] == currentWeapon) {
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = GameObject.Find("WeaponHolder").GetComponent<WeaponsAndTools>().GetCurrentWeapon();

        // Let Player turns on/off Crosshair
        HideCrosshair();

        if (Input.GetMouseButton(1) || noCrosshair() || isHidden) {
            cg.alpha = 0;
        }
        else if (!Input.GetMouseButton(1) && !noCrosshair() && !isHidden) {
            cg.alpha = 1;
        }
    }
}

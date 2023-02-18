using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponNameUI : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    [SerializeField]
    public GameObject weaponHolder;
    private GameObject currentWeapon;
    private string displayName;


    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = weaponHolder.GetComponent<WeaponsAndTools>().GetCurrentWeapon();

        if (currentWeapon == null) {
            displayName = "Hidden";
        }

        else
        {
            displayName = currentWeapon.name;
        }
       
        textBox.SetText(displayName);
    }
}

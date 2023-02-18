using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Trigger : MonoBehaviour
{
    private Animator anim;
    public bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isReloading = false;
    }

    public void PlayReloadAnimation() {
        isReloading = true;
        Debug.Log("Reloading...");
    }

    public void ReloadComplete() {
        isReloading = false;
        Debug.Log("Reloaded !");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0) {
            anim.CrossFade("Fire", 0.1f);
        }
        
    }
}

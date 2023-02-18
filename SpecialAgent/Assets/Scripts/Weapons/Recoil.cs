using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
   private Animator anim;
   private GameObject cameraEffect;
   // Add negative X value to get vertical recoil
   [Header("Recoil Amount")]
   public Vector3 amount;
   [Header("Recoil Animation Speed (float)")]
   public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        cameraEffect = GameObject.Find("Main Camera");
    }

    void MachineGunRecoil() {
       
       
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Full Auto") {
            if (Input.GetMouseButton(0) && Time.timeScale != 0) {
                anim.CrossFade("Fire", speed);
                cameraEffect.transform.eulerAngles += amount;
            }
            else {
                anim.CrossFade("Not Fire", speed);
            }
        }

        else if (gameObject.tag == "Semi Auto") {
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0) {
                anim.CrossFade("Fire", speed);
                cameraEffect.transform.eulerAngles += amount;
            }
            else {
                anim.CrossFade("Not Fire", speed);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSCamera : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) {
            if (Input.GetKey(KeyCode.Mouse1)) {
                anim.Play("ADS On");
            }
            if (Input.GetKeyUp(KeyCode.Mouse1)) {
                anim.Play("ADS Off");
            }
        }
    }
}

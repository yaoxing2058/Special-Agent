using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCase : MonoBehaviour
{
    private ParticleSystem shellCase;

    // Start is called before the first frame update
    void Start()
    {
        shellCase = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("Fire1")) {
            shellCase.Emit(1);
       } 
    }
}

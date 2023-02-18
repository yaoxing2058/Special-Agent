using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloOff : MonoBehaviour
{
    public Behaviour halo;
    // Start is called before the first frame update
    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
    }

    void OnCollisionEnter(Collision collision) {
        halo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFiring : MonoBehaviour
{

    public Rigidbody projectile;
    public float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("RightTrigger")) {
            Rigidbody instantiateProjectile = Instantiate(projectile, transform.position, transform.rotation)
            as Rigidbody;
            instantiateProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
       } 
    }
}

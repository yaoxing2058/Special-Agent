using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFiring : MonoBehaviour
{

    public Rigidbody projectile;
    public float speed = 20;
    private AudioSource gunSound;
    public GameObject throwCase;
    private ParticleSystem shellCase;
    private GameObject crosshair;
    private LineRenderer lr; // LineRenderer is for caliberation of crosshair

    // Start is called before the first frame update
    void Start()
    {
        gunSound = gameObject.GetComponent<AudioSource>();
        shellCase = throwCase.GetComponent<ParticleSystem>();
        crosshair = GameObject.FindWithTag("Crosshair");
        lr = gameObject.GetComponent<LineRenderer>();
    }

    /* void AlignCrosshair() {

        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 20)) {
            if (hit.collider) {
                crosshair.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(hit.point);
            }
        } 
    } */

    // Update is called once per frame
    void Update()
    {
       // AlignCrosshair();

       if (Input.GetButtonDown("Fire1") && Time.timeScale != 0) {
            Rigidbody instantiateProjectile = Instantiate(projectile, transform.position, transform.rotation)
            as Rigidbody;
            instantiateProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            gunSound.PlayOneShot(gunSound.clip);
            shellCase.Emit(1);
       } 
    }
}

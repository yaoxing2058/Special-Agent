using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunFiring : MonoBehaviour
{

    public Rigidbody projectile;
    public float speed = 20;
    private float timeBeforeShooting;
    [SerializeField] private float rateOfFire;
    public Vector3 upRecoil;
    private Vector3 originalRotation;
    private AudioSource fire;
    private AudioClip gunSound;
    public GameObject throwCase;
    public GameObject muzzleEffect;
    public GameObject firingBolt;
    private ParticleSystem shellCase;
    private ParticleSystem muzzleSmoke;
    private Animator anim;
    public int ammo;
    private GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        fire = gameObject.GetComponent<AudioSource>();
        gunSound = fire.clip;
        timeBeforeShooting = 1 / rateOfFire;
        shellCase = throwCase.GetComponent<ParticleSystem>();
        muzzleSmoke = muzzleEffect.GetComponent<ParticleSystem>();
        anim = firingBolt.GetComponent<Animator>();
        crosshair = GameObject.FindWithTag("Crosshair");
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

       if (Input.GetMouseButton(0) && Time.timeScale != 0) {
        if (timeBeforeShooting <= 0f && ammo > 0) {
            anim.CrossFade("Fire", 0.1f);
            fire.PlayOneShot(gunSound);
            Shoot();
            shellCase.Emit(1);
            muzzleSmoke.Emit(3);
            timeBeforeShooting = 1 / rateOfFire;
            ammo -= 1;
       }
        else {
        timeBeforeShooting -= Time.deltaTime;
       }
       }
       else {
        timeBeforeShooting = 0f;
       } 
}

void Shoot() {
    Rigidbody instantiateProjectile = Instantiate(projectile, transform.position, transform.rotation)
    as Rigidbody;
    instantiateProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
}
}




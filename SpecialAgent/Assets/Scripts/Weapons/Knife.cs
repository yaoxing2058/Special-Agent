using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Animator anim;
    private AudioSource knifeSound;
    public GameObject knifeHit;
    [SerializeField]
    private int hitScore = 5;
    [SerializeField]
    private int damage = 30;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        knifeSound = gameObject.GetComponent<AudioSource>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0) {
            anim.CrossFade("Attack", 0.1f);
            knifeSound.PlayOneShot(knifeSound.clip);
        }

        isAttacking = AnimatorIsPlaying(anim, "Attack");
    }

    // Return true/false whether specific state is playing in specific animator
    // Get the animator from weapon gameobject tagged as "WeaponName Functions" as the first argument
    // Check this animator for specific state in the animator controller
    private bool AnimatorIsPlaying(Animator anim, string stateName) {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime 
        && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void OnTriggerEnter(Collider collider) {

            if (collider.gameObject.tag == "Zombie" && isAttacking || (collider.gameObject.tag == "Zombie" && Input.GetButtonDown("Fire1") && Time.timeScale != 0)) {
                Debug.Log("Hit!");
                collider.GetComponent<Monster>().Hurt(damage);
                knifeHit.GetComponent<AudioSource>().PlayOneShot(knifeHit.GetComponent<AudioSource>().clip);
                GameObject.FindWithTag("Score").GetComponent<Score>().score += hitScore;
            }

    }
}

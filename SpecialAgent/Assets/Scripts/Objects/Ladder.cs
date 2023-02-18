using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IActivatable
{
    private Animator anim;
    public GameObject activateObject;
    public float resetTime;
    public string activateAction;
    // Start is called before the first frame update
    void Start()
    {
        anim = activateObject.GetComponent<Animator>();
    }

    // Return true/false whether specific state is playing in specific animator
    // Get the animator from weapon gameobject tagged as "WeaponName Functions" as the first argument
    // Check this animator for specific state in the animator controller
    private bool AnimatorIsPlaying(Animator anim, string stateName) {
        return anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime 
        && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public void Activate() {
        anim.Play("Activate");
        Invoke("Deactivate", resetTime);
    }

    public void Deactivate() {
        anim.Play("Deactivate");
    }

    public GameObject GetActivateObject() {
        return activateObject;
    }

    public string GetAction() {
        return activateAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

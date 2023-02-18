using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAndRunSounds : MonoBehaviour
{
    public AudioSource moveSound, runSound;

    private float walkTime;
    private float walkTimeDelay;
    private float runTime;
    private float runTimeDelay;
    private bool isWalking;
    private bool isRunning;
    private CharacterController player;

    [SerializeField]
    private AudioClip[] walkSounds;
    [SerializeField]
    private AudioClip[] runSounds;

    // Start is called before the first frame update
    void Start()
    {
        moveSound.enabled = false;
        runSound.enabled = false;
        walkTime = 0f;
        walkTimeDelay = 0.5f;
        runTime = 0f;
        runTimeDelay = 0.3f;
        isWalking = false;
        isRunning = false;
        player = GameObject.Find("Special Agent").GetComponent<CharacterController>();
    }

    private AudioClip Randomizer(AudioClip[] clips) {

        return clips[Random.Range(0, clips.Length)];

    }

    void movingSound() {
        
        // Walk Sound
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            
            if (!isRunning && player.isGrounded) {
                moveSound.enabled = true;
                isWalking = true;
                moveSound.clip = Randomizer(walkSounds);
                moveSound.PlayOneShot(moveSound.clip);
            }
        }

        else {
            moveSound.enabled = false;
            runSound.enabled = false;
            isWalking = false;
            isRunning = false;
        }
    }

     void runningSound() {

        // Run Sound
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {

            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) {

                if (player.isGrounded) {
                    runSound.enabled = true;
                    moveSound.enabled = false;
                    isRunning = true;
                    runSound.clip = Randomizer(runSounds);
                    runSound.PlayOneShot(runSound.clip);
                }
            }

        }

        else {
            moveSound.enabled = false;
            runSound.enabled = false;
            isWalking = false;
            isRunning = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) {

            walkTime += 1f * Time.deltaTime;
            runTime += 1f * Time.deltaTime;

            if (walkTime >= walkTimeDelay) {
                walkTime = 0f;
                movingSound();
            }

            if (runTime >= runTimeDelay) {
                runTime = 0f;
                runningSound();
            }
       }
    }
}

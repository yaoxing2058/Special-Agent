using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Tank : MonoBehaviour
{
    public GameObject player;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private float attackRange = 40f;
    public int maxHealth;
    private int currHealth;
    private Animator animator;
    private Rigidbody shell;
    public int damage = 10;
    public GameObject bullet;
    public GameObject firing;
    private float speed = 11f;
    private int temp;
    public GameObject scriptObject;
    public Player name;

    private AudioSource explosion;
    private AudioClip explosionSound;
    public Transform explosionPrefab;

    public enum State {
        ALIVE, DEAD
    }
    public State tankState = State.ALIVE;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currHealth = maxHealth;
        shell = bullet.GetComponent<Rigidbody>();
        temp = 1;
        explosion = gameObject.GetComponent<AudioSource>();
        explosionSound = explosion.clip;
        scriptObject = GameObject.Find("ScriptManager");
        name = scriptObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tankState == State.ALIVE) {
            navMeshAgent.SetDestination(player.transform.position);
            Vector3 distanceVector = transform.position - player.transform.position;
            distanceVector.y = 0;
            float distance = distanceVector.magnitude;
            if (distance <= attackRange) {
                animator.SetBool("Active", true);
                if (temp % 500 == 0) {
                    fire();
                    fire();
                    fire();
                    fire();
                    fire();
                }
                temp ++;
                
            }
            else {
                animator.SetBool("Active", false);
            }

        }
        
        void fire() {
            
            Rigidbody instantiateProjectile = Instantiate(shell, firing.transform.position, firing.transform.rotation)
            as Rigidbody;
            instantiateProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        
            
        }
    }
    public void Hurt(int damage) {
    if (tankState == State.ALIVE) {
        currHealth -= damage;
        if (currHealth <= 0)
            die();
    }
     
    }
    void die() {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.Play();
        tankState = State.DEAD;
        Destroy(gameObject, 0.1f);
        navMeshAgent.isStopped = true;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit!");
            Hurt(damage);

            //Debug.Log("minus10");
            //this.onHit();
        }
    }
}


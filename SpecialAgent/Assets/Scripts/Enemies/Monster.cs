using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public float attackRange;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public int maxHealth;
    private int currHealth;
    public int damage;
    public GameObject scriptObject;
    public Player name;
    public GameObject ScoreKeeper;
    public int hitScore = 10;
    public int killScore = 100;
    public enum State {
        ALIVE, DEAD
    }
    public State zombieState = State.ALIVE;
    public Transform zombieDead;
    public int disappearTime = 5;
    private AudioSource zombieSound;
    public AudioClip[] zombieSounds;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Main Camera");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currHealth = maxHealth;
        scriptObject = GameObject.Find("ScriptManager");
        name = scriptObject.GetComponent<Player>();
        ScoreKeeper = GameObject.FindWithTag("Score");
    }

    void Awake() {
        zombieSound = GetComponent<AudioSource>();
        zombieSound.clip = RandomSoundPicker();
        zombieSound.Play();
    }

    public AudioClip RandomSoundPicker() {
        return zombieSounds[Random.Range(0, zombieSounds.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1) {
            zombieSound.enabled = true;
        }
        else if (Time.timeScale == 0) {
            zombieSound.enabled = false;
        }
        
        if (zombieState == State.ALIVE) {
            navMeshAgent.SetDestination(player.transform.position);
            Vector3 distanceVector = transform.position - player.transform.position;
            distanceVector.y = 0;
            float distance = distanceVector.magnitude;
            if (distance <= attackRange) {
                Debug.Log("before atk");
                animator.SetBool("Attack", true);
                
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
    }
       

    public void Attack()
    {
        
        Debug.Log("in atk");
        name.Hurt(damage);
    }
    public void Hurt(int damage)
    {
        if (zombieState == State.ALIVE)
        {
            currHealth -= damage;
            if (currHealth <= 0)
                Die();
        }
    }
    void Die()
    {
        zombieState = State.DEAD;
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Dead");
        navMeshAgent.enabled = false;
        Instantiate(zombieDead, transform.position, transform.rotation);
        Destroy(gameObject, disappearTime);
        ScoreKeeper.GetComponent<Score>().score += killScore;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit!");
            ScoreKeeper.GetComponent<Score>().score += hitScore;
            Hurt(name.damage);

            //Debug.Log("minus10");
            //this.onHit();
        }
    }
}

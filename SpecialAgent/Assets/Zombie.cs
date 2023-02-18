using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject player;
    public float attackRange;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;
    public int maxHealth;
    private int currHealth;
    public enum State {
        ALIVE, DEAD
    }
    public State zombieState = State.ALIVE;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieState == State.ALIVE) {
            navMeshAgent.SetDestination(player.transform.position);
            Vector3 distanceVector = transform.position - player.transform.position;
            distanceVector.y = 0;
            float distance = distanceVector.magnitude;
            if (distance <= attackRange) {
                animator.SetBool("Attack", true);
            }
            else {
                animator.SetBool("Attack", false);
            }
        }
    }
}

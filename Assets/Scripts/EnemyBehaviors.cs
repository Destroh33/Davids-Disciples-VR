using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.AI;
using UnityEngine.AI;

public class EnemyBehaviors : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Transform tr;
    private Transform t;
    private bool playerContact = false;
    private bool canAttack = true;
    [SerializeField] GameObject player;
    private float chaseRange = 5f;
    private bool isChasing = false;
    [SerializeField] Transform[] walkPoints;
    EnemySpawner enemySpawner; 
    [SerializeField] NavMeshAgent agent; 

    public int currentIndex; 
  
    void Update() 
    {  
        if (playerContact && canAttack)
        {
            Attack();
        }
        if(player != null && Vector3.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            if(!isChasing)
            {
                isChasing = true;
                agent.destination = player.transform.position;

            }
        }
        if(isChasing && Vector3.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            agent.destination = player.transform.position;  
        }
        if(isChasing && Vector3.Distance(transform.position, player.transform.position) > chaseRange)
        {
            Debug.Log("in isChasing");
            isChasing = false; 
            agent.destination = walkPoints[currentIndex].transform.position;
        }
        else
        {
            if(!isChasing)
            {
                Patrol();
            }
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        t = FindObjectOfType<PlayerMovement>().transform;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        // Debug.Log(agent);

    
        agent.destination = walkPoints[currentIndex].position;

    }

    void Attack()
    {
        player.GetComponent<EntityHealthAndDmg>().TakeDamage(100);
        canAttack = false;
        Invoke("Cooldown", 2f);
    }
    void Cooldown()
    {
        canAttack = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerContact = true;
        }
        // else if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     Debug.Log("enemy collide");
        //     Collider otherEnemyCollider = collision.gameObject.GetComponent<Collider>();
        //     if (otherEnemyCollider != null)
        //         {
        //             Physics.IgnoreCollision(otherEnemyCollider, GetComponent<Collider>());
        //         }
        // }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = false;
        }
    }

    void Patrol()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, walkPoints[currentIndex].position);
        if (distanceToWaypoint < 6f)  
        {
            currentIndex = Random.Range(0, walkPoints.Length-1);  
            Debug.Log("new index:" + currentIndex);
            agent.destination = walkPoints[currentIndex].position; 
        }
    }

    public void OnDeath()
    {
        enemySpawner.RespawnEnemies();
    }
}

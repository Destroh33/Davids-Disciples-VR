using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bossBehavior : MonoBehaviour
{
    [SerializeField] int health = 20;
    int walkDir = 1;
    Animator anim;
    Rigidbody rb;
    bool dead = false;
    public AudioSource enemyHit;
    bool attacking = false;
    //EntityHealthAndDmg health;
    public bool playerIn = false;
    [SerializeField] List<Collider> dmgBox;
    private void Awake()
    {
        //health = GetComponent<EntityHealthAndDmg>();
        //forward = transform.forward;
        //StartCoroutine(Behavior());
        //tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        enemyHit = GameObject.Find("Sword Slash").GetComponent<AudioSource>();
        walkDir = 1;
    }
    private void Update()
    {
        if (health >= 0)
        {
            Walk();
            
            if(playerIn && !attacking)
            {
               Attack();
            }

        }
        else if (!dead)
        {
            Die();
        }
        
    }
    private void Begin()
    {
        walkDir = 1;
    }

    private void Walk()
    {
        if (!dead)
            rb.MovePosition(rb.position + new Vector3(0,0,(0.005f * walkDir) ));
        
    }
    public void Die()
    {
        //health = -22;
        anim.SetTrigger("crabDie");
        walkDir = 0;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.freezeRotation = true;
        dead = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("GHHGAA");
        if (collision.gameObject.CompareTag("Wall") && !dead)
        {
            if (walkDir == 1)
            {
                walkDir = -1;
            }
            else
            {
                walkDir = 1;
            }
        }

    }

    public void decHealth()
    {
        Debug.Log("owowwa");
        if(enemyHit != null)    
            enemyHit.Play();
        health--;
    }

    private void Attack()
    {
        attacking = true;
        int attack = Random.Range(0, 3);
        switch (attack)
        {
            case 0:
                anim.SetTrigger("attackL"); 
                dmgBox[0].isTrigger = true;
                dmgBox[1].isTrigger = true;
                break;
            case 1:
                anim.SetTrigger("attackR");
                dmgBox[2].isTrigger = true;
                dmgBox[3].isTrigger = true;
                break;
        }
        Invoke("triggerReset", 0.75f);
        Invoke("attackTimer", 1f);
        
    }
    private void attackTimer() {
        attacking = false;
        
    }
    private void triggerReset()
    {
        dmgBox[0].isTrigger = false;
        dmgBox[1].isTrigger = false;
        dmgBox[2].isTrigger = false;
        dmgBox[3].isTrigger = false;
    }
}

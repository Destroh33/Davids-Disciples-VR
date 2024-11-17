using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bossBehavior : MonoBehaviour
{
    int bossHealth = 100;
    int walkDir,attack;
    bool playerIn;
    Animator anim;
    Rigidbody rb;
    [SerializeField] List<Collider> dmgBox; 

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        StartCoroutine(Behavior());
    }
    private void Update()
    {
        Walk();

    }
    IEnumerator Behavior()
    {
        while (bossHealth > 0)
        {
            switch (Random.Range(0, 4))
            {
                case 0: //move left
                    walkDir = 1;
                    Debug.Log("move left");
                    //rb.AddForce(0, 0, 30);
                    break;
                case 1: //move right
                    walkDir = 2;
                    Debug.Log("move right");
                    //rb.AddForce(0, 0, -30);
                    break;
                case 2: //attack left
                    attack = 1;
                    walkDir = 0;
                    Attack();
                    Debug.Log("atk left");
                    break;
                case 3: //attack right
                    attack = 2;
                    walkDir = 0;
                    Attack();
                    Debug.Log("atk right");
                    break;
                //case 4: //idle
                //    walkDir = 0;
                //    Debug.Log("idle");
                //    break;

                //5 random behaviors -
                //move left
                //move right
                //attack left
                //attack right
                //idle 
            }
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            
        }
    }

    private void Walk()
    {
        anim.SetInteger("walkDir", walkDir);
        switch (/*Random.Range(0, 1)*/walkDir)
        {
            case 0:
                break;
            case 1:
                rb.MovePosition(rb.position + new Vector3(0, 0, -0.02f));
                break;
            case 2:
                //rb.AddForce(0, 0, -15 * Time.deltaTime);
                rb.MovePosition(rb.position + new Vector3(0, 0, 0.02f));
                break;

        }

    }
    private void Attack()
    {
        if (playerIn)
        {
            if(attack == 1) //
            {
                anim.SetTrigger("attackL");
                dmgBox[0].tag = "Damage";
                dmgBox[1].tag = "Damage";
                
            }
            else if(attack == 2) {
            
                anim.SetTrigger("attackR");
            }
        }
        attack = 0;
        Invoke("clearDamage", 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIn = false;
        }
    }

    void clearDamage()
    {
        
    }
}

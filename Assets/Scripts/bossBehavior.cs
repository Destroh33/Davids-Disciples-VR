//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class bossBehavior : MonoBehaviour
//{
//    int bossHealth = 100;
//    int walkDir,attack;
//    bool playerIn;
//    Animator anim;
//    Rigidbody rb;
//    [SerializeField] List<Collider> dmgBox; 

//    private void Awake()
//    {

//        rb = GetComponent<Rigidbody>();
//        anim = GetComponent<Animator>();
//        StartCoroutine(Behavior());
//    }
//    private void Update()
//    {
//        Walk();

//    }
//    IEnumerator Behavior()
//    {
//        while (bossHealth > 0)
//        {
//            if (playerIn)
//            {
//                Attack();
//                yield return null;
//            }
//            switch (Random.Range(0, 4))
//            {
//                case 0: //move left
//                    walkDir = 1;
//                    Debug.Log("move left");
//                    //rb.AddForce(0, 0, 30);
//                    break;
//                case 1: //move right
//                    walkDir = 2;
//                    Debug.Log("move right");
//                    //rb.AddForce(0, 0, -30);
//                    break;
//                case 2: //attack left
//                    attack = 1;
//                    walkDir = 0;
//                    Attack();
//                    Debug.Log("atk left");
//                    break;
//                case 3: //attack right
//                    attack = 2;
//                    walkDir = 0;
//                    Attack();
//                    Debug.Log("atk right");
//                    break;
//                //case 4: //idle
//                //    walkDir = 0;
//                //    Debug.Log("idle");
//                //    break;

//                //5 random behaviors -
//                //move left
//                //move right
//                //attack left
//                //attack right
//                //idle 
//            }
//            yield return new WaitForSeconds(Random.Range(1.5f, 4f));

//        }
//    }

//    private void Walk()
//    {
//        anim.SetInteger("walkDir", walkDir);
//        switch (/*Random.Range(0, 1)*/walkDir)
//        {
//            case 0:
//                break;
//            case 2:
//                rb.MovePosition(rb.position + new Vector3(0, 0, -0.02f));
//                break;
//            case 1:
//                //rb.AddForce(0, 0, -15 * Time.deltaTime);
//                rb.MovePosition(rb.position + new Vector3(0, 0, 0.02f));
//                break;

//        }

//    }
//    private void Attack()
//    {
//        if (playerIn)
//        {
//            if(attack == 1) //
//            {
//                anim.SetTrigger("attackL");
//                dmgBox[0].isTrigger = true;
//                dmgBox[1].isTrigger = true;
//                dmgBox[0].tag = "Damage";
//                dmgBox[1].tag = "Damage";

//            }
//            else if(attack == 2) {

//                anim.SetTrigger("attackR");
//                dmgBox[2].isTrigger = true;
//                dmgBox[3].isTrigger = true;
//                dmgBox[2].tag = "Damage";
//                dmgBox[3].tag = "Damage";
//            }
//        }
//        attack = 0;
//        Invoke("clearDamage", 0.5f);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            playerIn = true;
//        }
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.CompareTag("Player"))
//        {
//            playerIn = false;
//        }
//    }

//    void clearDamage()
//    {
//            dmgBox[0].tag = "Untagged";
//            dmgBox[1].tag = "Untagged";
//            dmgBox[2].tag = "Untagged";
//            dmgBox[3].tag = "Untagged";
//            dmgBox[0].isTrigger = false;
//            dmgBox[1].isTrigger = false;
//            dmgBox[2].isTrigger = false;
//            dmgBox[3].isTrigger = false;
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bossBehavior : MonoBehaviour
{
    int health = 25;
    int walkDir = 1;
    Animator anim;
    Rigidbody rb;
   // Vector3 forward;
    private bool wallCollide = false;
    bool dead = false;
    public AudioSource enemyHit;
    bool attacking;
    //EntityHealthAndDmg health;
    [SerializeField] Collider playerIn;
    //bool hasPlayedDead = false;
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
        Turn();
    }
    private void Update()
    {
        if (health >= 0 && !attacking)
        {
            Walk();
            //if (enemyHit != null)
            //{
            //    enemyHit.Play();
            //    //hasPlayedDead = true;
            //}
            //Die();
        }
        //Walk();



    }
    private void Begin()
    {
        walkDir = 1;
    }

    private void Walk()
    {
        if (!dead)
            rb.MovePosition(rb.position + new Vector3(0,0,(0.005f * walkDir) ));
        //if (walkDir == 0 && !dead)
        //    Turn();

    }
    public void Die()
    {
        Destroy(anim);
        walkDir = 0;
        transform.localEulerAngles = new Vector3(180, transform.rotation.y, transform.rotation.z);
        rb.isKinematic = false;
        gameObject.layer = 3;
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
    //IEnumerator Behavior()
    //{
    //    while (!dead)
    //    {
    //        //walkDir = Random.Range(-1, 2);
    //        //switch (Random.Range(0, 3))
    //        //{
    //        //    case 0: //move left
    //        //        walkDir = 1;
    //        //        Debug.Log("move left");
    //        //       //rb.MovePosition(rb.position + transform.forward * 5 * walkDir);
    //        //        //bh = 0;
    //        //        break;
    //        //    case 1: //move right
    //        //        walkDir = -1;
    //        //        Debug.Log("move right");
    //        //        //bh = 1;
    //        //        //rb.MovePosition(rb.position + transform.forward * 5 * walkDir);
    //        //        break;
    //        //    case 2:
    //        //        walkDir = 0;
    //        //        //bh = 2;
    //        //        Debug.Log("turn");
    //        //        //Turn();
    //        //        //yield return null;
    //        //        break;

    //        //}
    //        yield return new WaitForSeconds(Mathf.Abs(walkDir) * Random.Range(1.5f, 2f));

    //    }
    //}
   
    public bool isWallCollide()
    {
        return wallCollide;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            wallCollide = false;
        }
    }
    private void Turn()
    {
        //float tVal = (Random.Range(1, 4) * 90);
        Quaternion rot = Quaternion.Euler(0, Random.Range(1, 4) * 90, 0);
        //Debug.Log("ROTATTEEEEE");
        //transform.Rotate();
        //rb.rotation.SetEulerAngles(0, tVal, 0); /*(Quaternion.Euler(new Vector3(0, tVal, 0)))*/
        rb.rotation.Set(rot.x, rot.y, rot.z, rot.w);
        transform.Rotate(rot.eulerAngles);
        //Debug.Log("this is the end");
        //Debug.Log(transform.forward);

        // new Quaternion g = Quaternion.Euler(0, 0, 0);
    }
}

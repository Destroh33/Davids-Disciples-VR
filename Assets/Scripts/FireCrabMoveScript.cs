using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireCrabMoveScript : MonoBehaviour
{
    int health = 5;
    int walkDir;
    Animator anim;
    Rigidbody rb;
    Vector3 forward;
    private bool wallCollide = false;
    bool dead = false;
    public AudioSource enemyHit;
    bool hasPlayedDead = false; 
    private void Awake()
    {
        forward = transform.forward;
        StartCoroutine(Behavior());
        //tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        enemyHit = GameObject.Find("Sword Slash").GetComponent<AudioSource>();
        walkDir = 1;
        Turn();
    }
    private void Update()
    {
        if (health <= 0)
        {
            if(enemyHit != null && !hasPlayedDead)
            {
                enemyHit.Play();
                hasPlayedDead = true;
            }
             Die();
        }
        Walk();
        


    }

    private void Walk()
    {
        if (!dead)
        rb.MovePosition(rb.position + transform.forward * 0.02f * walkDir);
        if (walkDir == 0 && !dead)
            Turn();
        
    }
    public void Die()
    {
        Destroy(anim);
        walkDir = 0;
        transform.localEulerAngles = new Vector3 (180, transform.rotation.y, transform.rotation.z);
        rb.isKinematic = false;
        gameObject.layer = 3;
        dead = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("GHHGAA");
        if (collision.gameObject.CompareTag("Player"))
            Turn();
        else if (!collision.gameObject.CompareTag("Ground") && !dead)
        {
            //Debug.Log("collided");
            if (walkDir == 1)
            {
                walkDir = -1;
            }
            else
            {
                walkDir = 1;
            }
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            wallCollide = true;
        }

    }

    IEnumerator Behavior()
    {
        while (!dead)
        {
            walkDir = Random.Range(-1, 2);
            //switch (Random.Range(0, 3))
            //{
            //    case 0: //move left
            //        walkDir = 1;
            //        Debug.Log("move left");
            //       //rb.MovePosition(rb.position + transform.forward * 5 * walkDir);
            //        //bh = 0;
            //        break;
            //    case 1: //move right
            //        walkDir = -1;
            //        Debug.Log("move right");
            //        //bh = 1;
            //        //rb.MovePosition(rb.position + transform.forward * 5 * walkDir);
            //        break;
            //    case 2:
            //        walkDir = 0;
            //        //bh = 2;
            //        Debug.Log("turn");
            //        //Turn();
            //        //yield return null;
            //        break;

            //}
            yield return new WaitForSeconds(Mathf.Abs(walkDir) * Random.Range(1.5f, 2f));

        }
    }
    public void decHealth()
    {
        health--;
    }
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
        rb.rotation.Set(rot.x,rot.y,rot.z,rot.w);
        transform.Rotate(rot.eulerAngles);
        //Debug.Log("this is the end");
        //Debug.Log(transform.forward);

       // new Quaternion g = Quaternion.Euler(0, 0, 0);
    }
}

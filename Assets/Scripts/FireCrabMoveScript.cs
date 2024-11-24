using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireCrabMoveScript : MonoBehaviour
{
    int health = 10;
    int walkDir;
    Animator anim;
    Rigidbody rb;
    int bh;
    private bool wallCollide = false;
    bool dead = false;
    private void Awake()
    {
        StartCoroutine(Behavior());
        //tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        walkDir = 1;
    }
    private void Update()
    {
        switch (bh) {
            case 0:
            case 1:
                Walk();
                break;
            case 2:
                Turn();
                break;
        
        }


    }

    private void Walk()
    {
        anim.SetInteger("walkDir", walkDir);
        //for (int i = 0; i < Random.Range(0, 180); i++)
        //{
         
            switch (/*Random.Range(0, 1)*/walkDir)
            {
                //case 0:
                //    break;
                case 1:
                rb.velocity = Vector3.left * 5;
                    //rb.MovePosition(rb.position + new Vector3(0, 0, -0.02f));

                    break;
                case 2:
                rb.velocity = Vector3.right * 5;
                //rb.AddForce(0, 0, -15 * Time.deltaTime);
                //rb.MovePosition(rb.position + new Vector3(0, 0, 0.02f));
                break;

            }
        //}

    }
    public void Die()
    {
        walkDir = 0;
        transform.localEulerAngles = new Vector3 (180, transform.rotation.y, transform.rotation.z);
        dead = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !dead)
        {
            if (walkDir == 1)
            {
                walkDir = 2;
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
        while (health > 0)
        {
            switch (Random.Range(0, 3))
            {
                case 0: //move left
                    walkDir = 1;
                    Debug.Log("move left");
                    bh = 0;
                    break;
                case 1: //move right
                    walkDir = 2;
                    Debug.Log("move right");
                    bh = 1;
                    break;
                case 2:
                    bh = 2;
                    Debug.Log("turn");
                    yield return null;
                    break;

            }
            yield return new WaitForSeconds(Random.Range(1.5f, 6f));

        }
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
        float tVal = (Random.Range(0, 4) * 45);
        Debug.Log(tVal);
        transform.rotation = new Quaternion(0,tVal , 0, 0);
    }
}

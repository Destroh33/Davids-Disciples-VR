using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireCrabMoveScript : MonoBehaviour
{
    int health = 10;
    int walkDir;
    Animator anim;
    Rigidbody rb;
    Transform tr;
    private bool wallCollide = false;
    bool dead = false;
    private void Awake()
    {

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        walkDir = 1;
    }
    private void Update()
    {
        Walk();

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
    public void Die()
    {
        walkDir = 0;
        tr.localEulerAngles = new Vector3 (180, tr.rotation.y, tr.rotation.z);
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
        transform.rotation = new Quaternion(0, (Random.Range(0, 4) * 90), 0, 0);
    }
}

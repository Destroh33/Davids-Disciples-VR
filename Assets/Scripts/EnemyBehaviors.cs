using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviors : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Transform tr;
    private Transform t;
    [SerializeField] float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        t = FindObjectOfType<PlayerMovement>().transform;
    }
    void setDirection()
    {
        tr.LookAt(t.position, tr.up);
        tr.rotation = new Quaternion(tr.rotation.x, 0f, tr.rotation.z, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();
        Debug.Log(t.position);
        rb.velocity = tr.forward*speed;
    }
}

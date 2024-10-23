using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    bool inRange = false;
    Collider lastCollision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("In Range");
            lastCollision = collision;
            inRange = true;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Out of Range");
            inRange = false;
            lastCollision = null;
        }
    }

    void OnMelee()
    {
        if (inRange)
        {
            Debug.Log("Attacked");
            Destroy(lastCollision.gameObject);
        }
    }
}

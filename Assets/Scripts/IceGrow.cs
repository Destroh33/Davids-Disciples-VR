using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class IceGrow : MonoBehaviour
{
    public float timer = 10f;
    Vector3 bob;
    //public float maxSize = 20f;
    //public bool growing = false;

    public void Start()
    {
        bob = new Vector3(transform.position.x, (transform.position.y - 0.2f), transform.position.z);
        Invoke("Melt", 7f);
    }
    
    private void Melt()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //    float startTime = Time.deltaTime;
        //    float totalTime;
        //    if (collision.gameObject.CompareTag("Player"))
        //    {
        //        do{
        //            totalTime = (Time.time - startTime);
        //            float frac = totalTime / 0.2f;
        //            transform.position = Vector3.Lerp(transform.position, bob, frac);
        //        }
        //        while(transform.position != bob);
        //    }
        //}
        //gameObject.GetComponent<Animator>().set
    
    
    }
}
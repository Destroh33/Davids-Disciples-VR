using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().keyCount >= 4)
            {
                boss.SetActive(true);
            } 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

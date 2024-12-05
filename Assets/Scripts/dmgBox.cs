using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgBox : MonoBehaviour
{
    [SerializeField] bool playerDet;
    [SerializeField] bossBehavior bossBehavior = null;
    bool dmgCooldown = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerDet)
            {
                bossBehavior.playerIn = true;
            }
            else if (dmgCooldown)
            {
                dmgCooldown = false;
                other.gameObject.GetComponent<EntityHealthAndDmg>().TakeDamage(10);
                Invoke("Cooldown",0.2f);
            }
            //Debug.Log("owie");}
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("leavingbox");
            if (playerDet)
            {
                bossBehavior.playerIn = false;
            }
        }
    }
    private void Cooldown()
    {
        dmgCooldown = true;
    }
}

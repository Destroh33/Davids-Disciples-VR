using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    //bool inRange = false;
    //Collider lastCollision;
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    bool fire = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerAbility>().GetAbilityVal() == 2)
        {
            fire = true;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EntityHealthAndDmg>().TakeDamage(50);
        }
        if (collision.gameObject.CompareTag("Burnable") && fire){
            collision.GetComponent<BurnScript>().Burn();
        }
    }
    
}

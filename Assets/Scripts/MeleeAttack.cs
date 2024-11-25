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
    bool moving = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerAbility>().GetAbilityVal() == 4)
        {
            fire = true;
        }

    }
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EntityHealthAndDmg>().TakeDamage(50);
        }
        if (collision.gameObject.CompareTag("Burnable") && fire){
           // Debug.Log("burn");
            collision.GetComponent<BurnScript>().Burn();
        }
        if (collision.gameObject.CompareTag("Button")){
            switchAirMoving();
        }
        if (collision.gameObject.CompareTag("Crab"))
        {
            collision.GetComponent<FireCrabMoveScript>().decHealth();
        }
    }
    void switchAirMoving()
    {
        moving = !moving;
    }
    public bool isAirMoving() {
        return moving;
    }
    
}

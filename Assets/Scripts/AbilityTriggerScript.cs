using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int AbilityValSet;
    [SerializeField] GameObject ImageUI;
    int numKeys;
    //Material mat;
    void Start()
    {
       // mat = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (collision.gameObject.GetComponent<PlayerAbility>().GetAbilityVal() == 0)
            {
                numKeys = collision.gameObject.GetComponent<PlayerMovement>().keyCount;
                collision.gameObject.GetComponent<PlayerAbility>().SetAbilityVal(AbilityValSet);
                ImageUI.GetComponent<Animator>().SetTrigger("change");
                GetComponent<MeshRenderer>().enabled = true;
                GetComponent<Collider>().isTrigger = false;
            }
                //if (collision.gameObject.GetComponent<PlayerAbility>().GetAbilityVal() != 0)
                //{
                //    collision.gameObject.GetComponent<PlayerAbility>().SetAbilityVal(0);
                //}
                //else
                //{
                //    collision.gameObject.GetComponent<PlayerAbility>().SetAbilityVal(AbilityValSet);

                //    ImageUI.GetComponent<Animator>().SetTrigger("change");
                //    //Destroy(this);

                //}
                Debug.Log("Ability val changed");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && numKeys != collision.gameObject.GetComponent<PlayerMovement>().keyCount)
        {
            collision.gameObject.GetComponent<PlayerAbility>().SetAbilityVal(0);
            Destroy(gameObject);
        }
    }
}
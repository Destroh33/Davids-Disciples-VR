using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int AbilityValSet;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerAbility>().SetAbilityVal(AbilityValSet);
        }
    }
}
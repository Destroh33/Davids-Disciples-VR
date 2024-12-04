using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRemover : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerAbility>().GetAbilityVal() != 0)
            {
                collision.gameObject.GetComponent<PlayerAbility>().SetAbilityVal(0);
            }
        }
    }
}

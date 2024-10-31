using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthAndDmg : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;
    [SerializeField] bool player;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

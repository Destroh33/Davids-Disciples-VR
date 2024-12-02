using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EntityHealthAndDmg : MonoBehaviour
{
    [SerializeField] int maxHealth;
    public int currentHealth;
    [SerializeField] bool player;

    EnemyBehaviors enemyBehaviors;

    void Start()
    {
        currentHealth = maxHealth;
        enemyBehaviors = GetComponent<EnemyBehaviors>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
    }
    public int GetHealth()
    {
        return currentHealth;
    }

    public void GainHealth()
    {
        currentHealth = maxHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            if (player)
            {
                SceneManager.LoadScene("Game Over");
                Cursor.lockState = CursorLockMode.None;
            }
            if(!player)
            {
                enemyBehaviors.OnDeath();
                Debug.Log(" enemy died");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int GetHealth()
    {
        return currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
            if (player)
            {
                SceneManager.LoadScene("Game Over");
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}

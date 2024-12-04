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
    public AudioSource deathSound;
    public AudioSource bgMusic;
    public AudioSource playerHurtSound;
    void Start()
    {
        currentHealth = maxHealth;
        enemyBehaviors = GetComponent<EnemyBehaviors>();
        GameObject sound = GameObject.Find("Enemy Death");
        if (sound != null)
        {
            deathSound = sound.GetComponent<AudioSource>(); 
        }
        bgMusic = GameObject.Find("Audio Source").GetComponent<AudioSource>(); 
        playerHurtSound = GameObject.Find("Player Hurt").GetComponent<AudioSource>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (player && playerHurtSound != null)
        {
            playerHurtSound.Play();
        }  
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
                // if (bgMusic != null && bgMusic.isPlaying)
                // {
                //     bgMusic.Stop();  
                // }
                
            }
            if(!player)
            {
                deathSound.Play();
                enemyBehaviors.OnDeath();

                Debug.Log(" enemy died");
            }
        }
    }
}

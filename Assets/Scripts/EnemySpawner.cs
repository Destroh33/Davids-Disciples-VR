using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab; 
    [SerializeField] Transform[] spawnPoints; 
    // private List<GameObject> spawnedEnemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies() 
    {
        // spawns one enemy at each checkpoint 
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
            EnemyBehaviors enemyBehaviors = enemy.GetComponent<EnemyBehaviors>();
            if (enemyBehaviors != null)
            {
                enemyBehaviors.currentIndex = i;
                // enemyBehaviors.spawnPoint = i;
            }
            // spawnedEnemies.Add(enemy);
        } 
    }

    public void RespawnEnemies()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        // spawnedEnemies[index] = enemy;
        Debug.Log("enemy respawned");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : StaticInstance<EnemySpawner>
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private float spawnDelay = 3f;


    [SerializeField] public static int maxEnemies = 10;

    [SerializeField] public static int currentEnemyCount = 0;


    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
        {
            return;
        }
        int randomIndex = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomIndex].position,
            Quaternion.identity);
        currentEnemyCount++;
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnDelay);
    }


    static public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
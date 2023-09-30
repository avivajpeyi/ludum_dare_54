using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private float spawnDelay = 3f;


    [SerializeField] public  int maxEnemies = 10;

    [SerializeField] public  int currentEnemyCount = 0;

    
    // Make an event for EnemySpawned
    
    public static event Action OnEnemySpawned;
    
    
    private bool _canSpawn = false;


    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) StartCoroutine("SpawnEnemyCoroutine");
        else StopCoroutine("SpawnEnemyCoroutine");
    }


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

    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }


    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
    
    
}
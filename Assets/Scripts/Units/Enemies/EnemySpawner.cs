using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    // Editor fields
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] public int maxEnemies = 10;

    // Make getter for currentEnemyCount public, setter private
    public int currentEnemyCount { get; private set; } = 0;

    public static event Action OnEnemySpawned;
    private bool _canSpawn = false;


    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        EnemyHealth.OnEnemyDeath += DecreaseEnemyCount;
    }

    private void OnDestroy()
    {
        GameManager.OnBeforeStateChanged -= OnStateChanged;
        EnemyHealth.OnEnemyDeath -= DecreaseEnemyCount;
    }

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

        OnEnemySpawned?.Invoke();
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
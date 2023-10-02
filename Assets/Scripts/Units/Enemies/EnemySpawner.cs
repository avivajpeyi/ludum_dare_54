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

    public int maxEnemies
    {
        get { return Mathf.Max(50, 10 + 5 * PlayerLevel.Instance.currentLevel); }
    }

    // Make getter for currentEnemyCount public, setter private
    public int currentEnemyCount { get; private set; } = 0;

    float _lastSpawnTime = 0f;

    public static event Action OnEnemySpawned;


    private bool _canSpawn
    {
        get
        {
            return (
                GameManager.Instance.State == GameState.InGame &&
                currentEnemyCount < maxEnemies &&
                Time.time - _lastSpawnTime > spawnDelay
            );
        }
    }


    private void Awake()
    {
        base.Awake();
        EnemyHealth.OnEnemyDeath += DecreaseEnemyCount;

        foreach (var t in spawnPoints)
        {
            t.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

    private void OnDestroy()
    {
        EnemyHealth.OnEnemyDeath -= DecreaseEnemyCount;
    }

    public void Update()
    {
        if (_canSpawn)
        {
            SpawnEnemy();
        }
    }


    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
        {
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        GameObject e = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomIndex]
        .position,
            Quaternion.identity);
        e.name = $"enemy_{currentEnemyCount:000}";
        currentEnemyCount++;
        _lastSpawnTime = Time.time;
        OnEnemySpawned?.Invoke();
    }


    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
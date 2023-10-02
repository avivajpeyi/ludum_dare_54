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

    int MAX_ENEMIES = 50;
    int INTRO_DIFFIULTY_ENDS_AT_LVL = 5;
    
    
    private float spawnDelay
    {
        get
        {
            if (curLvl < INTRO_DIFFIULTY_ENDS_AT_LVL)
                return 3f;
            else if (curLvl > 15)
                return 0.1f;
            else
            {
                return Mathf.Max(3f - 0.1f * curLvl, 0.1f);
            }
        }
    }

    int curLvl => PlayerLevel.Instance.currentLevel;

    public int maxThisLvL
    {
        get { return Mathf.Clamp(10 + 5 * curLvl, 10, MAX_ENEMIES); }
    }

    public int probOfFastEnemy
    {
        get
        {
            if (curLvl < INTRO_DIFFIULTY_ENDS_AT_LVL)
                return 0;
            else if  (curLvl > 15)
                return 50;
            return 100 * Math.Clamp(curLvl+5, 1, maxThisLvL) / maxThisLvL;
        }
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
                currentEnemyCount < maxThisLvL &&
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
        if (currentEnemyCount >= maxThisLvL)
        {
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        GameObject e = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomIndex]
                .position,
            Quaternion.identity);

        e.name = $"enemy_{currentEnemyCount:000}";
        if (Random.Range(0, 100) < probOfFastEnemy)
        {
            float newspeed = Random.Range(4f, 7f);
            e.GetComponent<EnemyMovement>().SetSpeed(newspeed);
            e.name = $"enemy_{currentEnemyCount:000}_fast";
        }

        currentEnemyCount++;
        _lastSpawnTime = Time.time;
        OnEnemySpawned?.Invoke();
    }


    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
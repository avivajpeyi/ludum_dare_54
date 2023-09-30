using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : StaticInstance<EnemySpawner>
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private float spawnDelay = 3f;


    [SerializeField] public static int maxEnemies = 10;

    [SerializeField] public static int currentEnemyCount = 0;

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


    static public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}
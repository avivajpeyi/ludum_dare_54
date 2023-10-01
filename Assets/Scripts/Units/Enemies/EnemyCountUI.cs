using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCountUI : MonoBehaviour
{
    private TMP_Text txt;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        EnemySpawner.OnEnemySpawned += UpdateTxt;
        EnemyHealth.OnEnemyDeath += UpdateTxt;
    }


    private void OnDestroy()
    {
        EnemySpawner.OnEnemySpawned -= UpdateTxt;
        EnemyHealth.OnEnemyDeath -= UpdateTxt;
    }


    void Start()
    {
        txt = GetComponent<TMP_Text>();
        enemySpawner = EnemySpawner.Instance;
        txt.text = "";
        UpdateTxt();
    }


    void UpdateTxt()
    {
        if (GameManager.Instance.DebugMode)
        {
            txt.text = String.Format(
                "Enemy: {0}/{1}",
                enemySpawner.currentEnemyCount,
                enemySpawner.maxEnemies
            );
        }
    }
}
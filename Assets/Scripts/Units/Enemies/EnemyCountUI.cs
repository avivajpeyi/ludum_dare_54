using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCountUI : MonoBehaviour
{
    public TMP_Text txt;
    EnemySpawner enemySpawner;

    
    
    
    
    void Start()
    {
        txt = GetComponent<TMP_Text>();
        enemySpawner = EnemySpawner.Instance;
    }


    void Update()
    {
        
        // FIXME: on EnemyDeath and EnemySpawned events
        txt.text = String.Format(
            "Enemy: {0}/{1}",
            enemySpawner.currentEnemyCount,
            enemySpawner.maxEnemies
        );
    }
}
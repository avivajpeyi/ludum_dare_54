using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCountUI : MonoBehaviour
{
    public TMP_Text txt;

    void Update()
    {
        txt.text = String.Format(
            "Enemy: {0}/{1}",
            EnemySpawner.currentEnemyCount,
            EnemySpawner.maxEnemies
        );
    }
}
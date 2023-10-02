using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    public int score = 0;
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int amt)
    {
        score += amt;
        _scoreText.text = $"Score: {score:000}";
    }
}
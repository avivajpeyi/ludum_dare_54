using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UserLeaderboardInput : MonoBehaviour
{
    // [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TMP_InputField nameInput;

    public void SubmitScore()
    {
        Leaderboard.Instance.SetLeaderboard(nameInput.text, PlayerLevel.Instance.currentLevel, Timer.Instance.time);
    }
}
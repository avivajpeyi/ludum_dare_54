using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _scoreText;


    void Awake ()
    {
        ScoreEvents.SetScore(0f);
    }

    void Update ()
    {
        _scoreText.text = "Score: " + ScoreEvents.GetScore();
    }

    void OnDestroy() {
    }
}

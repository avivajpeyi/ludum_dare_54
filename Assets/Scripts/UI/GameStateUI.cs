using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : StaticInstance<GameStateUI>
{
    [SerializeField] private string startText = "Press any key to start";
    [SerializeField] private string gameOverText = "Game Over!";


    private TMP_Text txt;
    private Image img;

    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        img = GetComponent<Image>();
        txt = GetComponentInChildren<TMP_Text>();
        SetStartUi();
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.Initalisation) SetStartUi();
        else if (newState == GameState.GameOver) SetGameOverUi();
        else
        {
            txt.text = "";
            img.color = new Color(0, 0, 0, 0);
        }
    }

    

    public void SetStartUi()
    {
        txt.text = startText;
        img.color = new Color(0, 0, 0, 204/255f);
    }

    public void SetGameOverUi()
    {
        txt.text = gameOverText;
        img.color = new Color(0, 0, 0, 204/255f);
    }
}
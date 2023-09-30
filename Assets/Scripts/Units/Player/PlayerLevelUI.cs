using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLevelUI : MonoBehaviour
{
    TMP_Text txt;


    private void Awake()
    {
        GameManager.OnAfterStateChanged += OnStateChanged;
        PlayerLevel.OnIncreaseXP += UpdateLvlTxt;
    }

    private void OnDestroy()
    {
        GameManager.OnAfterStateChanged -= OnStateChanged;
        PlayerLevel.OnIncreaseXP -= UpdateLvlTxt;
    }


    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) UpdateLvlTxt();
        else txt.text = "";
    }


    private void Start()
    {
        txt = GetComponent<TMP_Text>();
    }


    public void UpdateLvlTxt()
    {
        txt.text = String.Format(
            "LVL: {0:00} [{1:000}/{2:000}]",
            PlayerLevel.currentLevel,
            PlayerLevel.currentXP,
            PlayerLevel.XPneeded
        );
    }
}
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
    }

    private void OnDestroy()
    {
        GameManager.OnAfterStateChanged -= OnStateChanged;
    }


    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame) SetTxt();
        else txt.text = "";
    }


    private void Start()
    {
        txt = GetComponent<TMP_Text>();
    }


    void SetTxt()
    {
        txt.text = String.Format(
            "LVL: {0:00} [{1:000}/{2:000}]",
            PlayerLevel.currentLevel,
            PlayerLevel.currentXP,
            PlayerLevel.XPneeded
        );
    }
}
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
        txt = GetComponent<TMP_Text>();
    }

    private void OnDestroy()
    {
        GameManager.OnAfterStateChanged -= OnStateChanged;
        PlayerLevel.OnIncreaseXP -= UpdateLvlTxt;
    }


    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.InGame)
        {
            UpdateLvlTxt();
            Debug.Log($"<color=yellow>{newState}: Lvl UI set</color>");
        }
        // else
        // {
        //     txt.text = "";
        //     Debug.Log("<color=yellow>Lvl UI cleared</color>");
        // }
    }


    private void Start()
    {
        txt = GetComponent<TMP_Text>();
    }


    public void UpdateLvlTxt()
    {
        txt.text = $"LVL: {PlayerLevel.Instance.currentLevel:00}";
        // txt.text = String.Format(
        //     "LVL: {0:00} [{1:000}/{2:000}]",
        //     PlayerLevel.Instance.currentLevel,
        //     PlayerLevel.Instance.currentXP,
        //     PlayerLevel.Instance.XPneeded
        // );
    }
}
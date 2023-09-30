using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLevelUI : MonoBehaviour
{
    TMP_Text txt;

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

    // Update is called once per frame
    void Update()
    {
        SetTxt();
    }
}
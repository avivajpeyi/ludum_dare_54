using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlayImageUi : StaticInstance<ScreenOverlayImageUi>
{
    
    private Image img;

    [SerializeField]
    Color inGameColor = new Color(0, 0, 0, 0);
    [SerializeField]
    Color otherStateColor = new Color(0, 0, 0, 204/255f);
    
    private void Awake()
    {
        GameManager.OnBeforeStateChanged += OnStateChanged;
        img = GetComponent<Image>();
        img.color = otherStateColor;
    }

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.Initalisation || newState == GameState.GameOver)
        {
            img.color = otherStateColor;
        }
        else
        {
            img.color = inGameColor;
        }
    }
    
}
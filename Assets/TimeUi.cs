using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUi : MonoBehaviour
{
    // Start is called before the first frame update
 TMP_Text txt;


    private void Awake()
    {
        GameManager.OnAfterStateChanged += OnStateChanged;
        txt = GetComponent<TMP_Text>();
    }

    private void OnDestroy()
    {
        GameManager.OnAfterStateChanged -= OnStateChanged;
    }


    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.GameOver)
        {
            UpdateLvlTxt();
        }

    }


    private void Start()
    {
        txt = GetComponent<TMP_Text>();
    }


    public void UpdateLvlTxt()
    {
        txt.text = Timer.Instance.time;
    }
}

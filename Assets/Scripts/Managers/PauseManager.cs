using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : StaticInstance<PauseManager>
{
    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    
    
    void OnGUI()
    {
        if (gameIsPaused)
        {
            // GUI BOX TOP LEFT CORNER
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "||");
        }
    }
    
    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{

    public void StartGame()
    {
        
        // Load the scene with the name 'game'
        SceneManager.LoadScene("game");
    }

    private void Update()
    {
        // click any mouse button to start the game
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;


    public GameState State { get; private set; }

    [SerializeField]
    bool  DebugMode = false;
    
    
    [Tooltip("Time when the game is over")]
    private float timeWhenGameOver = -1;

    [Tooltip("We havve waited long enough to restart the game")]
    bool CanRestart => State == GameState.GameOver && Time.time > timeWhenGameOver + 2f;


    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Initalisation);

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Initalisation:
                HandleGameInitialisation();
                break;
            case GameState.IsPaused:
                HandlePause();
                break;
            case GameState.InGame:
                HandleInGameState();
                break;
            case GameState.InUpgrades:
                HandleInGameState();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }


    private void OnGUI()
    {
        // put  textbox with current state in top right corner
        if (DebugMode)
            GUI.Label(new Rect(10, 10, 100, 20), $"State: {State}");
    }

    private void HandleGameInitialisation()
    {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state

        // TODO Call this from the UI when the player clicks the start button
        // ChangeState(GameState.InGame);
        PauseManager.gameIsPaused = false;
    }

    private void HandlePause()
    {
        PauseManager.gameIsPaused = true;
    }
    
    private void HandleInUpgrades()
    {
        PauseManager.gameIsPaused = true;
    }

    private void HandleInGameState()
    {
        PauseManager.gameIsPaused = false;
    }

    private void HandleGameOver()
    {
        // TODO: Show game over screen
        // PauseManager.gameIsPaused = true;
        timeWhenGameOver = Time.time;
    }
    

    private void Update()
    {
        if (State == GameState.Initalisation && Input.anyKeyDown)
            ChangeState(GameState.InGame);
        else if (CanRestart && Input.anyKeyDown)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}


[Serializable]
public enum GameState
{
    Initalisation = 0,
    IsPaused = 1,
    InGame = 2,
    GameOver = 3,
    InUpgrades = 4,
}
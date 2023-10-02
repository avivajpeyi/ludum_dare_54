using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSComputer))]
public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;


    public GameState State { get; private set; } = GameState.Initalisation;

    [SerializeField] public bool DebugMode = false;


    [Tooltip("Time when the game is over")]
    private float timeWhenGameOver = -1;

    [Tooltip("We havve waited long enough to restart the game")]
    bool CanRestart => State == GameState.GameOver && Time.time > timeWhenGameOver + 2f;


    private void Start()
    {
        ChangeState(GameState.Initalisation);
    }

    public void ChangeState(GameState newState)
    {
        Debug.Log($"<color=green>STATE:{State}-->{newState}</color>");
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
        {
            GUI.Label(new Rect(10, 10, 100, 20), $"State: {State}");
            // Add FPS on next line 
            GUI.Label(new Rect(10, 30, 100, 20), $"FPS: {FPSComputer.Instance.fps}");
            // Add number of enemies
            GUI.Label(new Rect(10, 50, 100, 20), 
                $"Enemies: {EnemySpawner.Instance.currentEnemyCount}/{EnemySpawner.Instance.maxThisLvL}");
            // Add amount of XP needed
            GUI.Label(new Rect(10, 70, 100, 20), 
                $"LVL XP: {PlayerLevel.Instance.currentXP}/{PlayerLevel.Instance.XPneeded}");
        }

        
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            DebugMode = !DebugMode;
            PlayerStats.Instance.damage = PlayerStats.Instance.maxDamage;
            PlayerStats.Instance.speed = PlayerStats.Instance.maxSpeed;
            // GameSpotlight.Instance.IncreaseSpotDistance();
        }
            
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
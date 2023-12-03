using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Runner,
    Shooter,
    Platformer,
    Puzzle
}
public class GameModeController : Singleton<GameModeController>
{
    public GameMode CurrentGameMode
    {
        get => _curGameMode;
        private set => SetGameMode(value);
    }
    private GameMode _curGameMode = GameMode.Runner;

    /// <summary>
    /// Method to set the current Game Mode
    /// </summary>
    /// <param name="gameMode"></param>
    public void SetGameMode(GameMode gameMode)
    {
        _curGameMode = gameMode;

        Debug.Log("[GameModeController] Game mode now set to: " + _curGameMode.ToString());
        
        OnGameModeChanged?.Invoke(_curGameMode);
    }
    
    public delegate void GameModeChanged(GameMode gameMode);

    /// <summary>
    /// Event that fires when the Game Mode changes
    /// </summary>
    public event GameModeChanged OnGameModeChanged;

    void Start()
    {
        // Start in runner mode
        SetGameMode(GameMode.Runner);
    }
    
    // INPUT BINDINGS
    public void OnNextMode()
    {
        // Increment the current game mode
        int nextGameMode = (int)CurrentGameMode + 1;
        if(nextGameMode > (int)GameMode.Puzzle)
        {
            nextGameMode = 0;
        }

        SetGameMode((GameMode)nextGameMode);
    }

    public void OnPreviousMode()
    {
        // Decrement the current game mode
        int nextGameMode = (int)CurrentGameMode - 1;
        if(nextGameMode < 0)
        {
            nextGameMode = (int)GameMode.Puzzle - 1;
        }

        SetGameMode((GameMode)nextGameMode);
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages swapping between different player controllers during a 
/// game mode change
/// </summary>
public class PlayerControllerManager : MonoBehaviour
{
    /// <summary>
    /// Simple pair class to hold a game mode and a player controller
    /// </summary> 
    [Serializable]
    public class PlayerControllerPair
    {
        public GameMode gameMode;
        public BasePlayerController playerController;
    }

    /// <summary>
    /// Creates a dictionary of player controllers for easy lookup
    /// </summary>
    /// <typeparam name="PlayerControllerPair"></typeparam>
    /// <returns></returns>
    public List<PlayerControllerPair> playerControllers = new List<PlayerControllerPair>();

    void Start()
    {
        // Register for the GameModeChanged event
        GameModeController.Instance.OnGameModeChanged += OnGameModeChanged;
    }

    /// <summary>
    /// Callback to handle enabling/disabling player controllers when the game mode changes
    /// </summary>
    /// <param name="gameMode"></param> 
    private void OnGameModeChanged(GameMode gameMode)
    {
        foreach(PlayerControllerPair pair in playerControllers)
        {
            if(pair.gameMode == gameMode)
            {
                pair.playerController.gameObject.SetActive(true);
            }
            else
            {
                pair.playerController.gameObject.SetActive(false);
            }
        }
    }
}
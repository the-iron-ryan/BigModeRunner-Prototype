using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ModeUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text modeText;

    [Header("Settings")]
    public string runnerModePrefix = "Current MODE: ";

    // Start is called before the first frame update
    void Start()
    {
        // Register for the GameModeChanged event
        GameModeController.Instance.OnGameModeChanged += OnGameModeChanged;
    }
    
    void OnDestroy()
    {
        // Unregister for the GameModeChanged event
        GameModeController.Instance.OnGameModeChanged -= OnGameModeChanged;
    }

    /// <summary>
    /// Swap the mode text when the game mode changes
    /// </summary>
    /// <param name="gameMode"></param>
    private void OnGameModeChanged(GameMode gameMode)
    {
        modeText.text = runnerModePrefix + gameMode.ToString();
    }

}

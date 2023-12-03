using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera References")]
    public Camera runnerCamera;
    public Camera shooterCamera;
    public Camera puzzleCamera;

    void Awake()
    {
    }
    

    void Start()
    {
        // Set camera to runner mode at start
        OnGameModeChanged(GameMode.Runner);

        // Register for the OnGameModeChanged event
        GameModeController.Instance.OnGameModeChanged += OnGameModeChanged;
    }

    void OnDestroy()
    {
        // Unregister for the OnGameModeChanged event
        GameModeController.Instance.OnGameModeChanged -= OnGameModeChanged;
    }

    /// <summary>
    /// Sets the respective camera active based on the current Game Mode
    /// </summary>
    /// <param name="gameMode"></param>
    private void OnGameModeChanged(GameMode gameMode)
    {
        switch(gameMode)
        {
            case GameMode.Runner:
                runnerCamera.gameObject.SetActive(true);
                shooterCamera.gameObject.SetActive(false);
                puzzleCamera.gameObject.SetActive(false);
                break;
            case GameMode.Shooter:
                runnerCamera.gameObject.SetActive(false);
                shooterCamera.gameObject.SetActive(true);
                puzzleCamera.gameObject.SetActive(false);
                break;
            case GameMode.Puzzle:
                runnerCamera.gameObject.SetActive(false);
                shooterCamera.gameObject.SetActive(false);
                puzzleCamera.gameObject.SetActive(true);
                break;
        }
    }
}
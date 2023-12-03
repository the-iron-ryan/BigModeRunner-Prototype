using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected BoxCollider boxCollider;
    protected Vector3 boxColliderDefaultSize;
    protected GameSettingsData GameSettingsData => GameSettings.Instance.GameSettingsData;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxColliderDefaultSize = boxCollider.size;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameModeController.Instance.OnGameModeChanged += OnGameModeChanged;
    }

    /// <summary>
    /// Handles swapping the enemy's collider size based on the current game mode
    /// </summary>
    /// <param name="gameMode"></param>
    private void OnGameModeChanged(GameMode gameMode)
    {
        switch(gameMode)
        {
            default:
            case GameMode.Runner:
            case GameMode.Shooter:
                // In Runner and shooter modes, the collider is the default size
                boxCollider.size = boxColliderDefaultSize;
                break;
            case GameMode.Platformer:
                // Expand the collider the entire width of stage
                boxCollider.size = new Vector3(2 * GameSettingsData.stageWidth, boxColliderDefaultSize.y, boxColliderDefaultSize.z);
                break;
            case GameMode.Puzzle:
                // Expand collider to be tall as entire stage
                boxCollider.size = new Vector3(boxColliderDefaultSize.x, 2 * GameSettingsData.stageHeight, boxColliderDefaultSize.z);
                break;
        }
    }
}

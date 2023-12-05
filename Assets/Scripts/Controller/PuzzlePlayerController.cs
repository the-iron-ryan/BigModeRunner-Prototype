using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePlayerController : BasePlayerController
{
    [Header("Puzzle References")]
    public Transform BottomWall;
    public Transform TopWall;

    /// <summary>
    /// Offset range from the wall that the player will select to do the puzzle with
    /// </summary>
    public float EnemyWallSelectionOffset = 5f;

    protected List<BaseEnemyCharacter> enemiesInRange = new List<BaseEnemyCharacter>();

    protected override void Awake()
    {
        base.Awake();
    }
    
    protected virtual void Update()
    {
        HandlePuzzleInput();
    }

    private void HandlePuzzleInput()
    {
        if(playerInputState.move.x >= 0.5f)
        {
            // Move right
            Grid grid;
            grid.
        }
        else if(playerInputState.move.x <= -0.5f)
        {
            // Move left
        }
        else if(playerInputState.move.y >= 0.5f)
        {
            // Move up
        }
        else if(playerInputState.move.y <= -0.5f)
        {
            // Move down
        }
    }

    /// <summary>
    /// Checks if the enemy is within the range of the puzzle
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    protected bool IsValidForPuzzle(BaseEnemyCharacter enemy)
    {
        // Find enemies that are between the top and bottom walls with an offset
        return enemy.transform.position.z > BottomWall.position.z + EnemyWallSelectionOffset && enemy.transform.position.z < TopWall.position.z - EnemyWallSelectionOffset;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        enemiesInRange.Clear();

        // Get the enemies in range and add them to the puzzle list
        BaseEnemyCharacter[] allEnemies = FindObjectsOfType<BaseEnemyCharacter>(includeInactive: true);
        foreach(BaseEnemyCharacter enemy in allEnemies)
        {
            if (IsValidForPuzzle(enemy))
            {
                enemy.gameObject.SetActive(true);
                enemiesInRange.Add(enemy);
            }
            else
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();

        // Reactivate all enemies
        BaseEnemyCharacter[] allEnemies = FindObjectsOfType<BaseEnemyCharacter>(includeInactive: true);
        foreach(BaseEnemyCharacter enemy in allEnemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}

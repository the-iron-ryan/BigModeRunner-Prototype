using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzlePlayerController : BasePlayerController
{

    [Header("Puzzle References")]
    public EnemyLayout EnemyLayout;
    public PuzzleArea PuzzleArea;

    [Header("Puzzle Settings")]
    /// <summary>
    /// Offset range from the wall that the player will select to do the puzzle with
    /// </summary>
    public float EnemyWallSelectionOffset = 5f;
    public float InputTimeout = 0.5f;

    protected float inputTimeoutTimer = 0f;

    protected PlayerCharacter playerCharacter;
    protected List<BaseEnemyCharacter> enemiesInRange = new List<BaseEnemyCharacter>();

    protected override void Awake()
    {
        base.Awake();

        playerCharacter = GetComponent<PlayerCharacter>();
    }
    
    protected virtual void Update()
    {
        HandlePuzzleInput();
    }

    private void HandlePuzzleInput()
    {
        if(inputTimeoutTimer > 0f)
        {
            inputTimeoutTimer -= Time.deltaTime;
            return;
        }

        if(playerInputState.move.x >= 0.5f)
        {
            inputTimeoutTimer = InputTimeout;

            // Move right
            float moveDistance = EnemyLayout.Grid.cellSize.x;
            foreach(BaseEnemyCharacter enemy in enemiesInRange)
            {
                // Move the enemy to the right
                enemy.transform.position += new Vector3(moveDistance, 0f, 0f);
            }
        }
        else if(playerInputState.move.x <= -0.5f)
        {
            inputTimeoutTimer = InputTimeout;
            // Move left
            float moveDistance = -EnemyLayout.Grid.cellSize.x;
            foreach(BaseEnemyCharacter enemy in enemiesInRange)
            {
                // Move the enemy to the right
                enemy.transform.position += new Vector3(moveDistance, 0f, 0f);
            }
        }
        else if(playerInputState.move.y >= 0.5f)
        {
            inputTimeoutTimer = InputTimeout;
            // Move up
            float moveDistance = EnemyLayout.Grid.cellSize.y;
            foreach(BaseEnemyCharacter enemy in enemiesInRange)
            {
                // Move the enemy to the right
                enemy.transform.position += new Vector3(0f, 0f, moveDistance);
            }
        }
        else if(playerInputState.move.y <= -0.5f)
        {
            inputTimeoutTimer = InputTimeout;
            // Move down
            float moveDistance = -EnemyLayout.Grid.cellSize.y;
            foreach(BaseEnemyCharacter enemy in enemiesInRange)
            {
                // Move the enemy to the right
                enemy.transform.position += new Vector3(0f, 0f, moveDistance);
            }
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
        return enemy.transform.position.z > PuzzleArea.BottomWall.position.z + EnemyWallSelectionOffset && enemy.transform.position.z < PuzzleArea.TopWall.position.z - EnemyWallSelectionOffset;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        PuzzleArea.gameObject.SetActive(true);

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

        PuzzleArea.gameObject.SetActive(false);

        // Reactivate all enemies
        BaseEnemyCharacter[] allEnemies = FindObjectsOfType<BaseEnemyCharacter>(includeInactive: true);
        foreach(BaseEnemyCharacter enemy in allEnemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class BaseEnemy : BaseCharacter, IDamageable
{
    /// <summary>
    /// Basic enum to represent the enemy's color
    /// </summary>
    public enum EnemyColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }
    
    public override CharacterTeam Team {get; set;} = CharacterTeam.Enemy;

    [Header("Enemy Settings")]
    public float EnemyStartingHealth = 100f;
    private float curEnemyHealth;
    public EnemyColor EnemyColorType;

    protected BoxCollider boxCollider;
    protected Vector3 boxColliderDefaultSize;

    protected MeshRenderer meshRenderer;


    protected GameSettingsData GameSettingsData => GameSettings.Instance.GameSettingsData;

    protected override void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxColliderDefaultSize = boxCollider.size;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameModeController.Instance.OnGameModeChanged += OnGameModeChanged;

        SetEnemyMeshColor();
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

    /// <summary>
    /// Helper function to set the enemy's mesh color based on the enemy's color enum
    /// </summary>
    public void SetEnemyMeshColor()
    {
        // Can be null if this function is called in the Editor
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
            
        switch(EnemyColorType)
        {
            case EnemyColor.Red:
                meshRenderer.material.color = Color.red;
                break;
            case EnemyColor.Blue:
                meshRenderer.material.color = Color.blue;
                break;
            case EnemyColor.Green:
                meshRenderer.material.color = Color.green;
                break;
            case EnemyColor.Yellow:
                meshRenderer.material.color = Color.yellow;
                break;
        }
    }

    public override void TakeDamage(float damage)
    {
        curEnemyHealth -= damage;

        if(curEnemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    public override CharacterTeam Team { get; set; } = CharacterTeam.Player;

    
    [Header("Player Settings")]
    public float PlayerStartingHealth = 100f;
    private float curPlayerHealth;

    public override void TakeDamage(float damage)
    {
        curPlayerHealth -= damage;

        if(curPlayerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Game Over Man!");
    }
}
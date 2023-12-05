using UnityEngine;

public enum CharacterTeam
{
    Player,
    Enemy
}

public abstract class BaseCharacter : MonoBehaviour, IDamageable
{
    /// <summary>
    /// Abstract team property that must be set by the child class
    /// </summary>
    /// <value></value>
    public abstract CharacterTeam Team {get; set;}

    [Header("Health Settings")]
    public float StartingHealth = 100f;
    protected float curHealth;

    protected virtual void Awake()
    {
        curHealth = StartingHealth;
    }
   public abstract void TakeDamage(float damage);
}
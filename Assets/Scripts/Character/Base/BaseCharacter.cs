using UnityEngine;

public enum CharacterTeam
{
    Player,
    Enemy
}

[RequireComponent(typeof(CharacterController))]
public abstract class BaseCharacter : MonoBehaviour, IDamageable
{
    /// <summary>
    /// Abstract team property that must be set by the child class
    /// </summary>
    /// <value></value>
    public abstract CharacterTeam Team {get; set;}

	protected CharacterController controller;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public abstract void TakeDamage(float damage);
}
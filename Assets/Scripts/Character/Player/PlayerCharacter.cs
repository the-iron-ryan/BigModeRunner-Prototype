using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : BaseCharacter
{
    public override CharacterTeam Team { get; set; } = CharacterTeam.Player;

	[Header("Player Gun")]
	public PlayerGun Gun;

    protected override void Awake()
    {
        base.Awake();

		if(Gun == null)
		{
			Gun = GetComponentInChildren<PlayerGun>();
		}
    }


    public override void TakeDamage(float damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Game Over Man!");
    }
}
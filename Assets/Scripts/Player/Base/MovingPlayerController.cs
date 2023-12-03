using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base class for a player that is able to jump
/// </summary>
public abstract class MovingPlayerController : BasePlayerController
{
	[Header("Player Movement")]
	[Tooltip("Move speed of the character in m/s")]
	public float MoveSpeed = 4.0f;
	[Tooltip("Sprint speed of the character in m/s")]
	public float SprintSpeed = 6.0f;
	[Tooltip("Acceleration and deceleration")]
	public float SpeedChangeRate = 10.0f;

	[Header("Player Jump")]
	[Space(10)]
	[Tooltip("The height the player can jump")]
	public float JumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.1f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float GroundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground. Defaults to 'Default Layer")]
	public LayerMask GroundLayers = 1;

	protected float verticalVelocity;
	protected float terminalVelocity = 53.0f;

	// timeout deltatime
	protected float jumpTimeoutDelta;
	protected float fallTimeoutDelta;

	protected override void Start()
	{
		base.Start();
		
		// reset our timeouts on start
		jumpTimeoutDelta = JumpTimeout;
		fallTimeoutDelta = FallTimeout;
	}
	
	protected virtual void Update()
	{
		JumpAndGravity();
		GroundedCheck();
		Move();
	}

	/// <summary>
	/// Checking if the player is on the ground (not jumping)
	/// </summary>
    protected virtual void GroundedCheck()
    {
		// set sphere position, with offset
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

	/// <summary>
	/// Handling vertical movement of the player
	/// </summary>
    protected virtual void JumpAndGravity()
	{
		if (Grounded)
		{
			// reset the fall timeout timer
			fallTimeoutDelta = FallTimeout;

			// stop our velocity dropping infinitely when grounded
			if (verticalVelocity < 0.0f)
			{
				verticalVelocity = -2f;
			}

			// Jump
			if (playerInputState.jump && jumpTimeoutDelta <= 0.0f)
			{
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
			}

			// jump timeout
			if (jumpTimeoutDelta >= 0.0f)
			{
				jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			// reset the jump timeout timer
			jumpTimeoutDelta = JumpTimeout;

			// fall timeout
			if (fallTimeoutDelta >= 0.0f)
			{
				fallTimeoutDelta -= Time.deltaTime;
			}

			// if we are not grounded, do not jump
			playerInputState.jump = false;
		}

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (verticalVelocity < terminalVelocity)
		{
			verticalVelocity += Gravity * Time.deltaTime;
		}
	}

	protected abstract void Move();
}
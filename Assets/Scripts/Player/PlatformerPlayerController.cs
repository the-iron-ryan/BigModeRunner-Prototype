using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerController : MovingPlayerController
{
    [Header("Platformer Settings")]
    public Vector3 runnerDirection = Vector3.forward;

    private float curRunnerSpeed = 10.0f;

    protected override void Move()
    {
        // Fetch the current input direction. Invert it because it comes in backwards relative to the
        // platformer camera
		Vector3 horizontalInputDirection = Mathf.Clamp(-playerInputState.move.x, -1.0f, 1.0f) * runnerDirection;
        Vector3 inputMovement = horizontalInputDirection * MoveSpeed * Time.deltaTime;

        // Fetch jump/gravity movement calculated from JumpAndGravity function
        Vector3 jumpMovement = new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime;

        Vector3 combinedMovement = inputMovement + jumpMovement;
        controller.Move(combinedMovement);
    }
}

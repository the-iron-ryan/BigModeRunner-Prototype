using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerController : MovingPlayerController
{
    [Header("Runner Settings")]
    public Vector3 runnerDirection = Vector3.forward;
    public float runnerAcceleration = 10.0f;
    public float maxRunnerSpeed = 50.0f;

    private float curRunnerSpeed = 10.0f;


    protected override void Move()
    {
        // Calculate current runner speed/direction
        Vector3 runnerMovement = runnerDirection * curRunnerSpeed * Time.deltaTime;
        curRunnerSpeed += runnerAcceleration * Time.deltaTime;
        curRunnerSpeed = Mathf.Clamp(curRunnerSpeed, 0.0f, maxRunnerSpeed);
        
        // Calculate input movement
		Vector3 horizontalInputDirection = new Vector3(playerInputState.move.x, 0.0f, 0.0f).normalized;
        Vector3 inputMovement = horizontalInputDirection * MoveSpeed * Time.deltaTime;

        // Fetch jump/gravity movement calculated from JumpAndGravity function
        Vector3 jumpMovement = new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime;

        Vector3 combinedMovement = runnerMovement + inputMovement + jumpMovement;
        controller.Move(combinedMovement);
    }
}

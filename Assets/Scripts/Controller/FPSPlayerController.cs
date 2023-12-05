using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// </summary>
public class FPSPlayerController : MovingPlayerController
{
	[Header("Player Camera")]
	[Tooltip("Rotation speed of the character")]
	public float RotationSpeed = 1.0f;
	[Tooltip("How far in degrees can you move the camera up")]
	public float TopClamp = 90.0f;
	[Tooltip("How far in degrees can you move the camera down")]
	public float BottomClamp = -90.0f;

	[Header("Player Gun")]
	public PlayerGun Gun;


	// player
	private float speed;
	private float rotationVelocity;

	private const float rotationInputThreshold = 0.01f;


    protected override void Awake()
    {
        base.Awake();

		if(Gun == null)
		{
			Gun = GetComponentInChildren<PlayerGun>();
		}
    }

    protected override void OnEnable()
    {
        base.OnEnable();

		Gun.gameObject.SetActive(true);
    }
	
	protected override void OnDisable()
	{
		base.OnDisable();

		Gun.gameObject.SetActive(false);
	}

    private void LateUpdate()
	{
		CameraRotation();
	}

	private void CameraRotation()
	{
		// if there is an input
		if (playerInputState.look.sqrMagnitude >= rotationInputThreshold)
		{
			//Don't multiply mouse input by Time.deltaTime
			float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

			cinemachineTargetPitch += playerInputState.look.y * RotationSpeed * deltaTimeMultiplier;
			rotationVelocity = playerInputState.look.x * RotationSpeed * deltaTimeMultiplier;

			// clamp our pitch rotation
			cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);

			// Update Cinemachine camera target pitch
			CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

			// rotate the player left and right
			transform.Rotate(Vector3.up * rotationVelocity);
		}

		// Lastly, update the gun direction	
		Gun.AimDirection = transform.forward;
	}

	override protected void Move()
	{
		// set target speed based on move speed, sprint speed and if sprint is pressed
		float targetSpeed = playerInputState.sprint ? SprintSpeed : MoveSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if (playerInputState.move == Vector2.zero) targetSpeed = 0.0f;

		// a reference to the players current horizontal velocity
		float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

		float speedOffset = 0.1f;
		float inputMagnitude = playerInputState.analogMovement ? playerInputState.move.magnitude : 1f;

		// accelerate or decelerate to target speed
		if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

			// round speed to 3 decimal places
			speed = Mathf.Round(speed * 1000f) / 1000f;
		}
		else
		{
			speed = targetSpeed;
		}

		// normalise input direction
		Vector3 inputDirection = new Vector3(playerInputState.move.x, 0.0f, playerInputState.move.y).normalized;

		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		if (playerInputState.move != Vector2.zero)
		{
			// move
			inputDirection = transform.right * playerInputState.move.x + transform.forward * playerInputState.move.y;
		}

		// move the player
		controller.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
	}

	/// <summary>
	/// Bang bang
	/// </summary>
	/// <param name="value"></param>
	public void OnShoot(InputValue value)
	{
		// Don't shoot if this controller is not enabled
		if(!enabled)
		{
			return;
		}

		if(value.isPressed)
		{
			Gun.Shoot();
		}
	}

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (Grounded) Gizmos.color = transparentGreen;
		else Gizmos.color = transparentRed;

		// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
	}
}
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerInputState))]
public abstract class BasePlayerController : MonoBehaviour
{
	[Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	public GameObject CinemachineCameraTarget;

	// cinemachine
	protected float cinemachineTargetPitch;


	protected PlayerInput playerInput;
	protected PlayerInputState playerInputState;
	protected CharacterController controller;

	protected bool IsCurrentDeviceMouse
	{
		get
		{
			return playerInput.currentControlScheme == "KeyboardMouse";
		}
	}


	protected virtual void Start()
	{
		controller = GetComponent<CharacterController>();
		playerInputState = GetComponent<PlayerInputState>();
		playerInput = GetComponent<PlayerInput>();
	}
}
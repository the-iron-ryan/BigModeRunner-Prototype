using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerInputState))]
public abstract class BasePlayerController : MonoBehaviour
{
	[Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	public GameObject CinemachineCameraTarget;
	[Tooltip("The camera for this player")]
	public CinemachineVirtualCameraBase CinemachineCamera;

	// cinemachine
	protected float cinemachineTargetPitch;
	/// <summary>
	/// Starting/default camera rotation. The controller will swap the camera back to this
	/// orientation when the controller is enabled
	/// </summary>
	protected Quaternion cameraStartingRotation;


	protected PlayerCharacter playerCharacter;
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

	protected virtual void Awake()
	{
		// Assign components 
		playerCharacter = GetComponent<PlayerCharacter>();
		controller = GetComponent<CharacterController>();
		playerInputState = GetComponent<PlayerInputState>();
		playerInput = GetComponent<PlayerInput>();

		// Make sure camera starting rotation is assigned before OnEnable
		cameraStartingRotation = CinemachineCamera.transform.rotation;
	}

	/// <summary>
	/// Handles camera settings when enabled
	/// </summary>	
	protected virtual void OnEnable()
	{
		Debug.Log($"Character {gameObject.name} has been enabled");
		CinemachineCamera.gameObject.SetActive(true);

		// Reset the camera's orientation
		CinemachineCamera.transform.rotation = cameraStartingRotation;
		
		// Reset the player's orientation 
		transform.rotation = Quaternion.identity;
	}
	
	/// <summary>
	/// Handles camera settings when disabled
	/// </summary>	
	protected virtual void OnDisable()
	{
		Debug.Log($"Character {gameObject.name} has been disabled");
		CinemachineCamera.gameObject.SetActive(false);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerInputState))]
public class PlatformerPlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public Vector3 moveDirection = new Vector3(1f, 0f, 0f);


    private float speed;
    private float verticalVelocity;

    private CharacterController controller;
    private PlayerInput playerInput;
    private PlayerInputState playerInputState;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputState = GetComponent<PlayerInputState>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    private void Move()
    {
    }
    
    private void JumpAndGravity()
    {
    }
}

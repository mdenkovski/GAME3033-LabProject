using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class MovementComponent : MonoBehaviour
{

    


    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float JumpForce;

    //components
    private PlayerController PlayerController;
    private Animator PlayerAnimator;
    private Rigidbody PlayerRigidbody;

    //references
    private Transform PlayerTransform;

    private Vector2 InputVector = Vector2.zero;
    private Vector3 MoveDirection = Vector3.zero;


    //Animator Hashes
    public readonly int MovementXHash = Animator.StringToHash("MovementX");
    public readonly int MovementYHash = Animator.StringToHash("MovementY");
    public readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int IsRunningHash = Animator.StringToHash("IsRunning");


    private void Awake()
    {
        PlayerTransform = transform;
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    public void OnMovement(InputValue value)
    {
        //Debug.Log(value.Get());
        InputVector = value.Get<Vector2>();
        PlayerAnimator.SetFloat(MovementXHash, InputVector.x);
        PlayerAnimator.SetFloat(MovementYHash, InputVector.y);
    }

    public void OnRun(InputValue value)
    {
        PlayerController.IsRunning = value.isPressed;
        PlayerAnimator.SetBool(IsRunningHash, value.isPressed);
    }


    public void OnJump(InputValue value)
    {
        PlayerController.IsJumping = value.isPressed;
        PlayerAnimator.SetBool(IsJumpingHash, value.isPressed);


        PlayerRigidbody.AddForce((PlayerTransform.up + MoveDirection) * JumpForce , ForceMode.Impulse);


    }


    private void Update()
    {
        if (PlayerController.IsJumping) return;

        if (!(InputVector.magnitude > 0)) MoveDirection = Vector3.zero;

        
        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;

        float currentSpeed = PlayerController.IsRunning ? RunSpeed : WalkSpeed;

        Vector3 movementDirection = MoveDirection * (currentSpeed * Time.deltaTime);

        PlayerTransform.position += movementDirection;



    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !PlayerController.IsJumping) return;
        
        PlayerController.IsJumping = false;
        PlayerAnimator.SetBool(IsJumpingHash, false);
        
    }

    //PlayerInputActions PlayerActions;

    //private void Awake()
    //{
    //    PlayerActions = new PlayerInputActions();
    //}

    //private void OnEnable()
    //{
    //    PlayerActions.Enable();
    //    PlayerActions.PlayerActionMap.Movement.performed += Movement;
    //}
    //public void Movement(InputAction.CallbackContext value)
    //{
    //    Debug.Log(value.ReadValue<Vector2>());
    //}

    //private void OnDisable()
    //{
    //    PlayerActions.Disable();
    //    PlayerActions.PlayerActionMap.Movement.performed -= Movement;
    //}
}

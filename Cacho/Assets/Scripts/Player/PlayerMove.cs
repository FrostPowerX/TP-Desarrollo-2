using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody rb;
    [SerializeField] FloorDetector fDet;

    [Header("Movement Config")]
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;

    [Header("Jump Config")]
    [SerializeField] float jumpForce;
    [SerializeField] float divisorJumpForce;
    [SerializeField] [Range(0f,1f)] float boostJumpDefaultTime;

    [Header("Info")]
    [SerializeField] float boostJumpTimer;

    InputAction move;
    InputAction crouch;
    InputAction jump;
    InputAction sprint;

    Vector3 movementX;
    Vector3 movementZ;

    float usedSpeed;

    bool jumping;
    bool pressJump;

    void Start()
    {
        move = inputActions.FindAction("Move");
        crouch = inputActions.FindAction("Crouch");
        jump = inputActions.FindAction("Jump");
        sprint = inputActions.FindAction("Sprint");

        jump.started += Jump;
        jump.canceled += CancelBoostJump;

        inputActions.Enable();
    }

    void Update()
    {
        boostJumpTimer -= (boostJumpTimer > 0) ? Time.deltaTime : boostJumpTimer;

        pressJump = jump.IsPressed() && boostJumpTimer > 0 && boostJumpTimer <= boostJumpDefaultTime - 0.05;

        Move();
    }

    void FixedUpdate()
    {
        Movement();
        Jumping();
    }

    void Movement()
    {
        if (!fDet.OnFloor)
            return;

        rb.AddForce(movementX, ForceMode.Force);
        rb.AddForce(movementZ, ForceMode.Force);
    }

    void Jumping()
    {
        if (jumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumping = false;
        }

        if (pressJump)
        {
            rb.AddForce(new Vector3(0, jumpForce / divisorJumpForce, 0), ForceMode.Impulse);
        }
    }

    void CancelBoostJump(InputAction.CallbackContext obj)
    {
        boostJumpTimer = 0;
    }

    void Jump(InputAction.CallbackContext obj)
    {
        if (fDet.OnFloor)
        {
            jumping = true;
            boostJumpTimer = boostJumpDefaultTime;
        }
    }

    void Move()
    {
        usedSpeed = (sprint.IsPressed()) ? sprintSpeed : speed;

        movementX = transform.right * usedSpeed * move.ReadValue<Vector2>().x;
        movementZ = transform.forward * usedSpeed * move.ReadValue<Vector2>().y;
    }
}

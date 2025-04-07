using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody rb;
    [SerializeField] FloorDetector fDet;

    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float jumpForce;

    InputAction move;
    InputAction crouch;
    InputAction jump;
    InputAction sprint;

    Vector3 movementX;
    Vector3 movementZ;

    float usedSpeed;

    bool jumping;

    void Start()
    {
        move = inputActions.FindAction("Move");
        crouch = inputActions.FindAction("Crouch");
        jump = inputActions.FindAction("Jump");
        sprint = inputActions.FindAction("Sprint");

        jump.started += Jump;

        inputActions.Enable();
    }

    void Update()
    {
        Move();
    }

    void Jump(InputAction.CallbackContext obj)
    {
        if (fDet.OnFloor)
            jumping = true;
    }

    void Move()
    {
        usedSpeed = (sprint.IsPressed()) ? sprintSpeed : speed;

        movementX = transform.right * usedSpeed * move.ReadValue<Vector2>().x;
        movementZ = transform.forward * usedSpeed * move.ReadValue<Vector2>().y;
    }

    void FixedUpdate()
    {
        rb.AddForce(movementX, ForceMode.Force);
        rb.AddForce(movementZ, ForceMode.Force);

        if(jumping)
        {
            rb.AddForce(new Vector3(0, jumpForce,0), ForceMode.Impulse);
            jumping = false;
        }
    }
}

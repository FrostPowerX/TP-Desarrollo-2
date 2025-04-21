using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Character character;

    [SerializeField] ForceRequest forceRequest;

    [SerializeField] float force;
    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    Vector3 dir3;

    InputAction move;
    InputAction jump;

    private void Awake()
    {
        inputActions.Enable();

        move = inputActions.FindAction("Move");
        jump = inputActions.FindAction("Jump");

        jump.started += Jump;
        move.canceled += CancelMove;

        forceRequest = new ForceRequest();
        forceRequest.speed = speed;
    }

    private void Update()
    {
        ActualizeDirection();
        Move();
    }

    void Jump(InputAction.CallbackContext cont)
    {
        ForceRequest request = new ForceRequest(Vector3.up, jumpForce, speed);
        character.InstantForceRequest(request);
    }

    void Move()
    {
        if (move.IsPressed())
        {
            forceRequest.force = force;
            character.ConstantForceRequest(forceRequest);
        }
    }

    void CancelMove(InputAction.CallbackContext cont)
    {
        character.CancelForce();
    }

    void ActualizeDirection()
    {
        Vector2 dir = move.ReadValue<Vector2>();

        dir3 = transform.right * dir.x + transform.forward * dir.y;

        forceRequest.direction.x = dir3.x;
        forceRequest.direction.z = dir3.z;
    }
}

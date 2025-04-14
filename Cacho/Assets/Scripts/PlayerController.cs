using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Character character;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    InputAction move;

    private void Awake()
    {
        inputActions.Enable();
        move = inputActions.FindAction("Move");
    }

    private void Update()
    {
        ActualizeDirection();
        Move();
    }

    void Jump()
    {

    }

    void Move()
    {
        if(move.IsPressed())
        {


            character.ConstantForceRequest(forceRequest);
        }
    }

    void ActualizeDirection()
    {
        Vector2 dir = move.ReadValue<Vector2>();
        forceRequest.direction = new Vector3(dir.x, 0, dir.y);
    }
}

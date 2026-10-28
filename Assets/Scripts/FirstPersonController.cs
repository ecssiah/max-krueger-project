using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] Transform camera_pivot;

    [SerializeField] float look_sensitivity = 0.6f;

    [SerializeField] float move_speed = 8.0f;
    [SerializeField] float jump_impulse = 13.0f;

    [SerializeField] float acceleration = 18.0f;

    [SerializeField] float gravity = -20.0f;

    CharacterController controller;

    Vector2 move_input;
    Vector2 look_input;
    bool jump_input;

    float pitch = 1.0f;

    Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look_input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump_input = true;
        }
        else if (context.canceled)
        {
            jump_input = false;            
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up, look_input.x * look_sensitivity);

        pitch = Mathf.Clamp(pitch - look_input.y * look_sensitivity, -89f, 89f);
        camera_pivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector3 move_intent = 
            transform.right * move_input.x + 
            transform.forward * move_input.y;

        Vector3 velocity_flat_current = new Vector3(velocity.x, 0.0f, velocity.z);
        Vector3 velocity_flat_target = move_intent * move_speed;
        
        velocity_flat_current = Vector3.MoveTowards(
            velocity_flat_current, 
            velocity_flat_target, 
            acceleration * Time.deltaTime
        );

        if (controller.isGrounded && velocity.y < 0.0f)
        {
            if (jump_input)
            {
                velocity.y = jump_impulse;
            }
            else
            {
                velocity.y = -2.0f;
            }
        }

        velocity = new Vector3(
            velocity_flat_current.x, 
            velocity.y + gravity * Time.deltaTime, 
            velocity_flat_current.z
        );

        CollisionFlags flags = controller.Move(velocity * Time.deltaTime);

        if ((flags & CollisionFlags.Above) != 0 && velocity.y > 0f) 
        {
            velocity.y = 0f;
        }
    }
}

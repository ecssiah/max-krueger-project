using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] Transform cameraPivot;

    [SerializeField] float lookSensitivity = 0.6f;

    [SerializeField] float moveSpeed = 12.0f;
    [SerializeField] float groundAcceleration = 32.0f;
    [SerializeField] float groundDecceleration = 64.0f;
    [SerializeField] float jumpImpulse = 12.0f;

    [SerializeField] float gravity = -20.0f;

    CharacterController controller;

    Vector2 moveInput;
    Vector2 lookInput;
    bool jumpInput;

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
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInput = true;
        }
        else if (context.canceled)
        {
            jumpInput = false;            
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up, lookInput.x * lookSensitivity);

        pitch = Mathf.Clamp(pitch - lookInput.y * lookSensitivity, -89f, 89f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector3 moveIntent = 
            transform.right * moveInput.x + 
            transform.forward * moveInput.y;

        Vector3 velocityFlatCurrent = new Vector3(velocity.x, 0.0f, velocity.z);
        Vector3 velocityFlatNext = moveIntent * moveSpeed;

        float acceleration = moveIntent.sqrMagnitude > 0.01f ? groundAcceleration : groundDecceleration;
        
        velocityFlatNext = Vector3.MoveTowards(
            velocityFlatCurrent, 
            velocityFlatNext, 
            acceleration * Time.deltaTime
        );

        if (controller.isGrounded && velocity.y < 0.0f)
        {
            if (jumpInput)
            {
                velocity.y = jumpImpulse;
            }
            else
            {
                velocity.y = -2.0f;
            }
        }

        velocity = new Vector3(
            velocityFlatNext.x,
            velocity.y + gravity * Time.deltaTime,
            velocityFlatNext.z
        );

        CollisionFlags flags = controller.Move(velocity * Time.deltaTime);

        if ((flags & CollisionFlags.Above) != 0 && velocity.y > 0f) 
        {
            velocity.y = 0f;
        }
    }
}

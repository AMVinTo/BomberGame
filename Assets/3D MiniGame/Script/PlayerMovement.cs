using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private  float forwardSpeed = 5f;
    [SerializeField] private float sideSpeed = 5f;
    [SerializeField] private  float gravity = -20f;

    private CharacterController controller;
    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller == null || !controller.enabled)
            return;
        
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        
        //float horizontalInput = Input.GetAxis("Horizontal");
        
        float horizontalInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }
        Vector3 movement = new Vector3(horizontalInput * sideSpeed,verticalVelocity,forwardSpeed
        );

        movement *= Time.deltaTime;
        controller.Move(movement);
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private  float forwardSpeed = 5f;
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
        
        Vector3 movement = new Vector3(
            0f,
            verticalVelocity,
            forwardSpeed
        );

        movement *= Time.deltaTime;
        controller.Move(movement);
    }
}
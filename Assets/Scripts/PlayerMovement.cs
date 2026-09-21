using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.down;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject spriteUp;
    [SerializeField] private GameObject spriteDown;
    [SerializeField] private GameObject spriteLeft;
    [SerializeField] private GameObject spriteRight;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDirection(Vector2.down, spriteDown);
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = speed * Time.fixedDeltaTime * direction;
        rb.MovePosition(position + translation);
        HandleDirection();
    }

    private void SetDirection(Vector2 newDirection, GameObject newSprite)
    {
        direction = newDirection;
        spriteUp.SetActive(newSprite == spriteUp);
        spriteDown.SetActive(newSprite == spriteDown);
        spriteLeft.SetActive(newSprite == spriteLeft);
        spriteRight.SetActive(newSprite == spriteRight);
    }
    
    private void HandleDirection()
    {
        
        switch (true)
        {
            case true when Keyboard.current.wKey.isPressed: SetDirection(Vector2.up, spriteUp); break;
            
            case true when Keyboard.current.sKey.isPressed: SetDirection(Vector2.down, spriteDown); break;
            
            case true when Keyboard.current.aKey.isPressed: SetDirection(Vector2.left, spriteLeft); break;
            
            case true when Keyboard.current.dKey.isPressed: SetDirection(Vector2.right, spriteRight); break;
            
            default: direction = Vector2.zero; break;
        }
        
    }

    private void HandleAnimation()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            animator.Play("WalkUp");
        }
        
    }
}
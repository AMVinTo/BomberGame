using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.down;
    [SerializeField] private float speed = 5f;
    private HandleAnimation handleAnim;
    [SerializeField] private HandleAnimation spriteUp;
    [SerializeField] private HandleAnimation spriteDown;
    [SerializeField] private HandleAnimation spriteLeft;
    [SerializeField] private HandleAnimation spriteRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDirection(Vector2.down, spriteDown);
    }

    private void Update()
    {
        HandleDirection();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = speed * Time.fixedDeltaTime * direction;
        rb.MovePosition(position + translation);
        
    }

    private void SetDirection(Vector2 newDirection,HandleAnimation newSpriteRenderer)
    {
        direction = newDirection;
        spriteUp.gameObject.SetActive(newSpriteRenderer == spriteUp);
        spriteDown.gameObject.SetActive(newSpriteRenderer == spriteDown);
        spriteLeft.gameObject.SetActive(newSpriteRenderer == spriteLeft);
        spriteRight.gameObject.SetActive(newSpriteRenderer == spriteRight);

        handleAnim = newSpriteRenderer;
        handleAnim.SetMoving(true);
    }
    
    private void HandleDirection()
    {
        
        switch (true)
        {
            case true when Keyboard.current.wKey.isPressed: SetDirection(Vector2.up, spriteUp); break;
            
            case true when Keyboard.current.sKey.isPressed: SetDirection(Vector2.down, spriteDown); break;
            
            case true when Keyboard.current.aKey.isPressed: SetDirection(Vector2.left, spriteLeft); break;
            
            case true when Keyboard.current.dKey.isPressed: SetDirection(Vector2.right, spriteRight); break;
            
            default: 
                direction = Vector2.zero;
                handleAnim.SetMoving(false);
                break;
        }
        
    }

   /* private void HandleAnimation()
    {
        if (direction == Vector2.up)
        {
            animator.Play("WalkUp");
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            animator.Play("WalkDown");
        }
        
    }*/
}
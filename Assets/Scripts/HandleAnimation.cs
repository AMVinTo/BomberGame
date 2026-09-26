using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HandleAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite[] animationSprites;
    [SerializeField] private float animationTime = 0.25f;
    [SerializeField] private bool loop = true;
    private int animationFrame;
    private bool isMoving;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        animationFrame = 0;

        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;

        CancelInvoke(nameof(NextFrame));
    }

    private void NextFrame()
    {
        if (!isMoving)
        {
            spriteRenderer.sprite = idleSprite;
            return;
        }

        if (animationSprites.Length == 0)
            return;

        spriteRenderer.sprite = animationSprites[animationFrame];

        animationFrame++;

        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        else if (!loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = animationSprites.Length - 1;
        }
    }

    public void SetMoving(bool moving)
    {
        if (isMoving == moving)
            return;

        isMoving = moving;
        animationFrame = 0;
    }
}
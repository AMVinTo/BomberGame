using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float explosionTime = 3f;
    private BombPlacer bombPlacer;

    public void SetBombPlacer(BombPlacer placer)
    {
        bombPlacer = placer;
    }

    private void Explode()
    {
        Debug.Log("Explode");
        bombPlacer.BombExploded();
        Destroy(gameObject);
    }
    void Start()
    {
        Invoke(nameof(Explode),explosionTime);
    }
    
}

using UnityEngine;
using UnityEngine.InputSystem;

public class BombPlacer : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        Instantiate(bombPrefab ,  transform.position , Quaternion.identity);
    }
}



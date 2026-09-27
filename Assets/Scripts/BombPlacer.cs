using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BombPlacer : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private int maxBombs = 1;
    private int activeBombs;

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        if (activeBombs >= maxBombs)
            return;

        GameObject bomb = Instantiate(
            bombPrefab,
            transform.position,
            Quaternion.identity
        );

        BombController bombController = bomb.GetComponent<BombController>();
        bombController.SetBombPlacer(this);
        activeBombs++;
    }
    
    public void BombExploded()
    {
        activeBombs--;
    }
}



using UnityEngine;

public class StickInventory : MonoBehaviour
{
    public int stickCount = 0;

    public void AddStick(int amount)
    {
        stickCount += amount;

        Debug.Log("Sticks: " + stickCount);
    }
}
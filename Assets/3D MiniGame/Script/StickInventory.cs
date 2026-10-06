using System;
using UnityEngine;
using TMPro;

public class StickInventory : MonoBehaviour
{
    public int stickCount = 0;
    [SerializeField] private TMP_Text displayText;

    private void Update()
    {
        displayText.text = "Sticks: " + stickCount;
    }

    public void AddStick(int amount)
    {
        stickCount += amount;

        Debug.Log("Sticks: " + stickCount);
        
    }
    
}
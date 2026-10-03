using UnityEngine;
using TMPro;

public class GateModifier : MonoBehaviour
{
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    [Header("Gate Settings")]
    [SerializeField] private Operation operation = Operation.Add;
    [SerializeField] private int value = 1;

    [Header("Gate Display")]
    [SerializeField] private TMP_Text displayText;

    [Header("Gate Color")]
    [SerializeField] private Color addColor = Color.green;
    [SerializeField] private Color subtractColor = Color.red;
    [SerializeField] private Color multiplyColor = Color.blue;
    [SerializeField] private Color divideColor = Color.yellow;

    private Renderer[] gateRenderers;
    private bool used = false;

    void Start()
    {
        gateRenderers = GetComponentsInChildren<Renderer>();

        UpdateDisplay();
        UpdateGateColor();
    }

    void UpdateDisplay()
    {
        if (displayText == null)
            return;

        switch (operation)
        {
            case Operation.Add:
                displayText.text = "+ " + value;
                break;

            case Operation.Subtract:
                displayText.text = "- " + value;
                break;

            case Operation.Multiply:
                displayText.text = "× " + value;
                break;

            case Operation.Divide:
                displayText.text = "÷ " + value;
                break;
        }
    }

    void UpdateGateColor()
    {
        Color selectedColor = Color.white;

        switch (operation)
        {
            case Operation.Add:
                selectedColor = addColor;
                break;

            case Operation.Subtract:
                selectedColor = subtractColor;
                break;

            case Operation.Multiply:
                selectedColor = multiplyColor;
                break;

            case Operation.Divide:
                selectedColor = divideColor;
                break;
        }

        foreach (Renderer renderer in gateRenderers)
        {
            if (renderer != null)
            {
                renderer.material.color = selectedColor;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (used)
            return;

        if (!other.CompareTag("Player"))
            return;

        StickInventory inventory = other.GetComponent<StickInventory>();

        if (inventory == null)
            return;

        switch (operation)
        {
            case Operation.Add:
                inventory.stickCount += value;
                break;

            case Operation.Subtract:
                inventory.stickCount -= value;
                break;

            case Operation.Multiply:
                inventory.stickCount *= value;
                break;

            case Operation.Divide:
                if (value != 0)
                {
                    inventory.stickCount /= value;
                }
                break;
        }

        if (inventory.stickCount < 0)
        {
            inventory.stickCount = 0;
        }

        Debug.Log(
            "Gate Used | Operation: " +
            operation +
            " | Value: " +
            value +
            " | Sticks: " +
            inventory.stickCount
        );

        used = true;
    }
}
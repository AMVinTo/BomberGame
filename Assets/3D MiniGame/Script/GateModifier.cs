using UnityEngine;
using TMPro;

public class GateModifier : MonoBehaviour
{
    [Header("Gate Mode")]
    [SerializeField] private GateMode gateMode = GateMode.Fixed;
    [Header("Fixed Gate Settings")]
    [SerializeField] private Operation operation = Operation.Add;
    [SerializeField] private int value = 1;
    [Header("Random Gate Settings")]
    [SerializeField] private RandomOption[] randomOptions;
    [Header("Gate Display")]
    [SerializeField] private TMP_Text displayText;
    [Header("Gate Color")]
    [SerializeField] private Color addColor = Color.green;
    [SerializeField] private Color subtractColor = Color.red;
    [SerializeField] private Color multiplyColor = Color.blue;
    [SerializeField] private Color divideColor = Color.yellow;
    private Renderer[] gateRenderers;
    private bool used = false;
    private Operation selectedOperation;
    private int selectedValue;
    public enum Operation
    {
        Add,Subtract,Multiply,Divide
    }

    public enum GateMode
    {
        Fixed,Random
    }

    [System.Serializable]
    public class RandomOption
    {
        public Operation operation;
        public int value = 1;
    }

    
    private void Start()
    {
        gateRenderers = GetComponentsInChildren<Renderer>();

        SelectGateOperation();
        UpdateDisplay();
        UpdateGateColor();
    }

    private void SelectGateOperation()
    {
        if (gateMode == GateMode.Fixed)
        {
            selectedOperation = operation;
            selectedValue = value;
            return;
        }

       if (randomOptions == null || randomOptions.Length == 0)
        {
            Debug.LogWarning(
                "Random Gate has no options assigned!",
                gameObject
            );
            selectedOperation = Operation.Add;
            selectedValue = 1;
            return;
        }

        int randomIndex = Random.Range(0, randomOptions.Length);

        selectedOperation = randomOptions[randomIndex].operation;
        selectedValue = randomOptions[randomIndex].value;
    }

    private void UpdateDisplay()
    {
        if (displayText == null)
            return;

        switch (selectedOperation)
        {
            case Operation.Add:
                displayText.text = "+ " + selectedValue;
                break;
            case Operation.Subtract:
                displayText.text = "- " + selectedValue;
                break;
            case Operation.Multiply:
                displayText.text = "× " + selectedValue;
                break;
            case Operation.Divide:
                displayText.text = "÷ " + selectedValue;
                break;
        }
    }

    private void UpdateGateColor()
    {
        Color selectedColor = Color.white;

        switch (selectedOperation)
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
        switch (selectedOperation)
        {
            case Operation.Add:
                inventory.stickCount += selectedValue;
                break;
            case Operation.Subtract:
                inventory.stickCount -= selectedValue;
                break;
            case Operation.Multiply:
                inventory.stickCount *= selectedValue;
                break;
            case Operation.Divide:
                if (selectedValue != 0)
                {
                    inventory.stickCount /= selectedValue;
                }
                break;
        }
        // Prevent negative sticks
        if (inventory.stickCount < 0)
        {
            inventory.stickCount = 0;
        }
        Debug.Log(
            "Gate Used | Mode: " +gateMode +" | Operation: " +selectedOperation +" | Value: " 
            +selectedValue +" | Sticks: " +inventory.stickCount
        );
        used = true;
    }
}


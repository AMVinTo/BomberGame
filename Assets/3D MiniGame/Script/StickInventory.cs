using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StickInventory : MonoBehaviour
{
    public int stickCount = 0;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private Transform stickHolder;
    [SerializeField] private float stickSpacing = 0.25f;
    private List<GameObject> visualSticks = new List<GameObject>();
    private int lastStickCount = 0;
    private void Update()
    {
        displayText.text = "Sticks: " + stickCount;
        if (stickCount != lastStickCount)
        {
            UpdateVisualSticks();
            lastStickCount = stickCount;
        }
    }

    public void AddStick(int amount)
    {
        stickCount += amount;

        Debug.Log("Sticks: " + stickCount);
        UpdateVisualSticks();
        lastStickCount = stickCount;
    }

    private void UpdateVisualSticks()
    {
        if (stickPrefab == null || stickHolder == null)
            return;

        while (visualSticks.Count < stickCount)
        {
            GameObject stick = Instantiate(
                stickPrefab,
                stickHolder
            );
            visualSticks.Add(stick);
        }

        while (visualSticks.Count > stickCount)
        {
            GameObject stick = visualSticks[visualSticks.Count - 1];
            visualSticks.RemoveAt(visualSticks.Count - 1);
            Destroy(stick);
        }

        for (int i = 0; i < visualSticks.Count; i++)
        {
            visualSticks[i].transform.localPosition =
                new Vector3(0f , 1+ i * stickSpacing, 0f );
        }
    }
}
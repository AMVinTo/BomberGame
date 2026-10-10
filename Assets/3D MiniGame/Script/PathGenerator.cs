
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private GameObject groundChunkPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private float chunkLength = 10f;
    [Header("Gaps")]
    [Range(0f, 1f)]
    [SerializeField] private float gapChance = 0.3f;
    [SerializeField] private int minRequiredSticks = 5;
    [SerializeField] private int maxRequiredSticks = 10;
    [SerializeField] private float stickLength = 0.3f;
    [Header("Sticks")]
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private int sticksPerChunk = 15;
    [SerializeField] private float stickHeight = 0.75f;
    private readonly List<GameObject> chunks = new();
    private readonly Dictionary<GameObject, List<GameObject>>
        chunkSticks = new();

    private readonly Dictionary<GameObject, int>
        gapBeforeChunk = new();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject chunk = Instantiate(
                groundChunkPrefab,
                Vector3.zero,
                Quaternion.identity,
                transform
            );

            chunk.name = "GroundChunk_" + i;
            chunk.SetActive(true);

            chunks.Add(chunk);
            CreateSticks(chunk);
        }

        chunks[0].transform.position = Vector3.zero;
        gapBeforeChunk[chunks[0]] = 0;
        for (int i = 1; i < chunks.Count; i++)
        {
            PlaceAfter(chunks[i], chunks[i - 1]);
        }

        Debug.Log($"PathGenerator: Created {chunks.Count} ground chunks.");
    }
    
    private void CreateSticks(GameObject chunk)
    {
        List<GameObject> sticks = new();
        for (int i = 0; i < sticksPerChunk; i++)
        {
            GameObject stick = Instantiate(stickPrefab,chunk.transform);
            stick.name = "Stick_" + i;
            sticks.Add(stick);
        }
        chunkSticks[chunk] = sticks;
        ResetSticks(chunk);
    }

    private void PlaceAfter(GameObject chunk, GameObject previous)
    {
        int required = 0;

        if (Random.value < gapChance)
        {
            required = Random.Range(minRequiredSticks,maxRequiredSticks + 1);
        }

        float gapWidth = required * stickLength;
        Vector3 position = previous.transform.position;
        position.z += chunkLength + gapWidth;
        chunk.transform.position = position;
        gapBeforeChunk[chunk] = required;
    }

    private void Update()
    {
        if (player == null || chunks.Count < 2)
            return;
        while (player.position.z >
               chunks[0].transform.position.z + chunkLength)
        {
            GameObject firstChunk = chunks[0];
            chunks.RemoveAt(0);
            GameObject lastChunk = chunks[chunks.Count - 1];
            PlaceAfter(firstChunk, lastChunk);
            ResetSticks(firstChunk);
            chunks.Add(firstChunk);
        }
    }

    private void ResetSticks(GameObject chunk)
    {
        if (!chunkSticks.TryGetValue(chunk, out List<GameObject> sticks))
            return;
        foreach (GameObject stick in sticks)
        {
            if (stick == null)
                continue;
            stick.transform.SetParent(chunk.transform, false);
            float randomX = Random.Range(-0.5f, 0.5f);
            float randomZ = Random.Range(-1f, 0.5f);
            stick.transform.localPosition = new Vector3(randomX,stickHeight,randomZ);
            stick.transform.localRotation = Quaternion.identity;
            stick.SetActive(true);
        }
    }
    public int GetRequiredSticksForGap(GameObject chunk)
    {
        if (chunk != null &&
            gapBeforeChunk.TryGetValue(chunk, out int required))
        {
            return required;
        }
        return 0;
    }
}
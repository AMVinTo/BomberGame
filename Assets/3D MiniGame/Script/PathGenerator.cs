using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private  GameObject groundChunk;
    [SerializeField] private  Transform player;
    [SerializeField] private float chunkLength = 10f;
    [SerializeField] private  int initialChunks = 10;
    [SerializeField] private  int chunksAhead = 5;
    private List<GameObject> chunks = new List<GameObject>();


    void Start()
    {
      
        for (int i = 0; i < initialChunks; i++)
        {
            SpawnChunk();
        }
    }
    void Update()
    {
        if (player == null)
            return;

        GenerateChunks();
        DeleteChunks();
    }
 
    void GenerateChunks()
    {
        float playerZ = player.position.z;

        float lastChunkZ = 0f;

        if (chunks.Count > 0)
        {
            lastChunkZ = chunks[chunks.Count - 1].transform.position.z;
        }
        while (lastChunkZ < playerZ + chunksAhead * chunkLength)
        {
            SpawnChunk();
            lastChunkZ = chunks[chunks.Count - 1].transform.position.z;
        }
    }


 
    void SpawnChunk()
    {
        float spawnZ = 0f;

        if (chunks.Count > 0)
        {
            GameObject lastChunk = chunks[chunks.Count - 1];
            spawnZ = lastChunk.transform.position.z + chunkLength;
        }

        Vector3 spawnPosition = new Vector3(
            0f,
            0f,
            spawnZ
        );

        GameObject newChunk = Instantiate(
            groundChunk,
            spawnPosition,
            Quaternion.identity
        );

        newChunk.name = "Chunk_" + chunks.Count;
        chunks.Add(newChunk);
    }

    

    void DeleteChunks()
    {
        while (chunks.Count > 0)
        {
            GameObject firstChunk = chunks[0];

            if (firstChunk == null)
            {
                chunks.RemoveAt(0);
                continue;
            }

            float chunkEndZ =
                firstChunk.transform.position.z + chunkLength;

        
            if (player.position.z > chunkEndZ)
            {
                chunks.RemoveAt(0);
                Destroy(firstChunk);
            }
            else
            {
                break;
            }
        }
    }
}
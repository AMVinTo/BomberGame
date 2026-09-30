using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    public GameObject stickPrefab;
    public int stickCount = 5;

    public float minZ = 0f;
    public float maxZ = 50f;

    void Start()
    {
        for (int i = 0; i < stickCount; i++)
        {
            float z = Random.Range(minZ, maxZ);
            float x = Random.Range(-2f, 2f);

            Vector3 position = new Vector3(
                x,
                0.75f,
                z
            );

            Instantiate(
                stickPrefab,
                position,
                Quaternion.identity
            );
        }
    }
}
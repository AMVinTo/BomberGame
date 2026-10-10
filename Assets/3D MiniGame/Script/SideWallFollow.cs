using UnityEngine;

public class SideWallsFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float startZOffset;

    void Start()
    {
        if (player != null)
        {
            startZOffset = transform.position.z - player.position.z;
        }
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.position.z + startZOffset
        );
    }
}
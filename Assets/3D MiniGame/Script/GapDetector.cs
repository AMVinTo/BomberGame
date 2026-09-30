using System.Collections;
using UnityEngine;

public class GapDetector : MonoBehaviour
{
    [SerializeField] private float checkDistance = 1.5f;
    [SerializeField] private float checkHeight = 0.5f;
    [SerializeField] private GameObject bridgeStickPrefab;
    [SerializeField] private float bridgeHeight = 0.25f;
    [SerializeField] private float stickPlaceInterval = 0.2f;
    [SerializeField] private int maxBridgeSticks = 30;
    private StickInventory inventory;
    private bool isBuildingBridge = false;

    void Start()
    {
        inventory = GetComponent<StickInventory>();
    }

    void Update()
    {
        if (!isBuildingBridge)
        {
            DetectGap();
        }
    }

    void DetectGap()
    {
        Vector3 checkPosition =
            transform.position +
            Vector3.forward * checkDistance +
            Vector3.up * checkHeight;

        bool groundExists = Physics.Raycast(
            checkPosition,
            Vector3.down,
            2f
        );

        Debug.DrawRay(
            checkPosition,
            Vector3.down * 2f,
            groundExists ? Color.green : Color.red
        );

        if (!groundExists)
        {
            Debug.Log("GAP DETECTED!");

            StartCoroutine(BuildBridge());
        }
    }

    IEnumerator BuildBridge()
    {
        isBuildingBridge = true;

        for (int i = 0; i < maxBridgeSticks; i++)
        {

            Vector3 checkPosition =
                transform.position +
                Vector3.forward * checkDistance +
                Vector3.up * checkHeight;

            bool groundExists = Physics.Raycast(
                checkPosition,
                Vector3.down,
                2f
            );
            
            if (groundExists)
            {
                Debug.Log("GROUND FOUND");
                break;
            }
            
            if (inventory == null || inventory.stickCount <= 0)
            {
                Debug.Log("NO STICKS");
                break;
            }

            Vector3 stickPosition = new Vector3(
                transform.position.x,
                bridgeHeight,
                transform.position.z + 0.5f
            );

            GameObject NewStick = Instantiate(bridgeStickPrefab, stickPosition, Quaternion.identity);
            Destroy(NewStick, 1f);
            inventory.stickCount--;
            Debug.Log(
                "STICK PLACED | Remaining: " +
                inventory.stickCount
            );
            yield return new WaitForSeconds(stickPlaceInterval);
        }

        isBuildingBridge = false;
    }
}


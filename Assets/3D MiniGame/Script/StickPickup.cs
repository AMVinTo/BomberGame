using UnityEngine;

public class StickPickup : MonoBehaviour
{
    public int stickAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StickInventory inventory = other.GetComponent<StickInventory>();

            if (inventory != null)
            {
                inventory.AddStick(stickAmount);
                gameObject.SetActive(false);
            }
        }
    }
}
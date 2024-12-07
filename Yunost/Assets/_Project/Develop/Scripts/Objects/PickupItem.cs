using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;
    private PickupUIManager uiManager;

    private void Start()
    {
        // Находим общий UI-менеджер
        uiManager = FindAnyObjectByType<PickupUIManager>();
        if (uiManager == null)
        {
            Debug.LogError("PickupUIManager отсутствует на сцене.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && uiManager != null)
        {
            uiManager.ShowPickupText(this);
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                inventory.ShowPickupUI(this);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && uiManager != null)
        {
            uiManager.HidePickupText();
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                inventory.HidePickupUI();
            }

        }
    }





}

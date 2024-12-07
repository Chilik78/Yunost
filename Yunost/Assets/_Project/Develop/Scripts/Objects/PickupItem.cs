using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;
    private PickupUIManager uiManager;

    private void Start()
    {
        // ������� ����� UI-��������
        uiManager = FindAnyObjectByType<PickupUIManager>();
        if (uiManager == null)
        {
            Debug.LogError("PickupUIManager ����������� �� �����.");
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

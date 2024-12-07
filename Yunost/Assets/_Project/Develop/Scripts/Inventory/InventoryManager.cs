using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject inventoryUI; // ������ ���������
    public Button inventoryButton; // ������ ��� �������� ���������
    public Transform itemParent;   // �������� ��� ������ ���������
    public GameObject slotPrefab;  // ������ ����� ��� ���������

    private bool isInventoryOpen = false;
    private List<Item> inventoryItems = new List<Item>();
    private PickupItem nearbyItem;

    void Start()
    {
        if (inventoryUI == null || inventoryButton == null || itemParent == null || slotPrefab == null)
        {
            Debug.LogError("�� ��� �������� UI ��������� � ����������!");
            return;
        }

        inventoryUI.SetActive(false);
        inventoryButton.onClick.AddListener(ToggleInventory);
    }

    void Update()
    {
        // ��������/�������� ���������
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // ������ �������� �� ������ F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nearbyItem != null)
            {
                PickupNearbyItem();
            }
            else
            {
                Debug.LogWarning("����� ��� ��������� ��� �������!");
            }
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
    }

    public void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.LogWarning("������� �������� ������ �������!");
            return;
        }

        inventoryItems.Add(newItem);

        // �������� ����� ��� ������ ��������
        GameObject slot = Instantiate(slotPrefab, itemParent);

        // ���������� �������� � ������
        InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
        if (inventorySlot != null)
        {
            inventorySlot.SetItem(newItem);
        }
        else
        {
            Debug.LogError("������ ����� �� �������� ��������� InventorySlot!");
        }
    }

    public void ShowPickupUI(PickupItem item)
    {
        if (item != null)
        {
            nearbyItem = item;
        }
    }

    public void HidePickupUI()
    {
        nearbyItem = null;
    }

    private void PickupNearbyItem()
    {
        if (nearbyItem != null && nearbyItem.item != null)
        {
            AddItem(nearbyItem.item);
            Destroy(nearbyItem.gameObject);
            HidePickupUI();
        }
        else
        {
            Debug.LogWarning("���������� ��������� �������: ���� nearbyItem, ���� ��� item == null.");
        }
    }
}

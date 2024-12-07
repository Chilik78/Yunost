using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject inventoryUI; // Панель инвентаря
    public Button inventoryButton; // Кнопка для открытия инвентаря
    public Transform itemParent;   // Родитель для слотов инвентаря
    public GameObject slotPrefab;  // Префаб слота для предметов

    private bool isInventoryOpen = false;
    private List<Item> inventoryItems = new List<Item>();
    private PickupItem nearbyItem;

    void Start()
    {
        if (inventoryUI == null || inventoryButton == null || itemParent == null || slotPrefab == null)
        {
            Debug.LogError("Не все элементы UI настроены в инспекторе!");
            return;
        }

        inventoryUI.SetActive(false);
        inventoryButton.onClick.AddListener(ToggleInventory);
    }

    void Update()
    {
        // Открытие/закрытие инвентаря
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Подбор предмета по кнопке F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nearbyItem != null)
            {
                PickupNearbyItem();
            }
            else
            {
                Debug.LogWarning("Рядом нет предметов для подбора!");
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
            Debug.LogWarning("Попытка добавить пустой предмет!");
            return;
        }

        inventoryItems.Add(newItem);

        // Создание слота для нового предмета
        GameObject slot = Instantiate(slotPrefab, itemParent);

        // Обновление текстуры и текста
        InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
        if (inventorySlot != null)
        {
            inventorySlot.SetItem(newItem);
        }
        else
        {
            Debug.LogError("Префаб слота не содержит компонент InventorySlot!");
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
            Debug.LogWarning("Невозможно подобрать предмет: либо nearbyItem, либо его item == null.");
        }
    }
}

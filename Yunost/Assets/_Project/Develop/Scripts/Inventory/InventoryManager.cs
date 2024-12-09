using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject inventoryUI; // Панель инвентаря
    public GameObject craftingZoneUI;
    public List<GameObject> HUDButtons = new List<GameObject>();
    public Button inventoryButton; // Кнопка для открытия инвентаря
    public Transform itemParent;   // Родитель для слотов инвентаря
    public GameObject slotPrefab;  // Префаб слота для предметов

    private bool isInventoryOpen = false;
    private List<Item> inventoryItems = new List<Item>();
    private PickupItem nearbyItem;
    private bool isPaused = false;

    void Start()
    {
        if (inventoryUI == null || inventoryButton == null || itemParent == null || slotPrefab == null)
        {
            Debug.LogError("Не все элементы UI настроены в инспекторе!");
            return;
        }

        inventoryUI.SetActive(false);
        craftingZoneUI.SetActive(false);
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
        craftingZoneUI.SetActive(isInventoryOpen);
        if (isPaused)
        {
            Resume();
            for (int i = 0; i < HUDButtons.Count; i++) HUDButtons[i].SetActive(true);
        }
        else
        {
            Pause();
            for (int i = 0; i < HUDButtons.Count; i++) HUDButtons[i].SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void AddItem(Item item)
    {
        Debug.Log(item.name);
        inventoryItems.Add(item);
        UpdateInventoryUI();
    }

    /*    public void AddItem(Item newItem)
        {
            if (newItem == null)
            {
                Debug.LogWarning("Попытка добавить пустой предмет!");
                return;
            }

            inventoryItems.Add(newItem);

            
            GameObject slot = Instantiate(slotPrefab, itemParent);

            
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (inventorySlot != null)
            {
                if (slot.GetComponent<CanvasGroup>() == null)
                {
                    slot.AddComponent<CanvasGroup>();
                }
                inventorySlot.SetItem(newItem);
                inventorySlot.SetParent(itemParent);
            }
            else
            {
                Debug.LogError("Префаб слота не содержит компонент InventorySlot!");
            }
        }*/

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


    public void RemoveItem(Item itemToRemove)
    {
        int index = inventoryItems.IndexOf(itemToRemove);
        if (index != -1)
        {
            inventoryItems.RemoveAt(index);
       
            UpdateInventoryUI(); 
        }
    }

    private void UpdateInventoryUI()
    {
   
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }
 
        foreach (Item item in inventoryItems)
        {
            GameObject slot = Instantiate(slotPrefab, itemParent);
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (inventorySlot != null)
            {
                inventorySlot.SetItem(item);
            }
        }
    }
}

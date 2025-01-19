using Global;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class InventoryManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject inventoryUI; // Панель инвентаря
    public GameObject craftingZoneUI;
    public List<GameObject> HUDButtons = new List<GameObject>();
    public Button inventoryButton; // Кнопка для открытия инвентаря
    public Transform itemParent;   // Родитель для слотов инвентаря
    public GameObject slotPrefab;  // Префаб слота для предметов
    public List<Item> autoInventoryItems = new List<Item>();

    private bool isInventoryOpen = false;
    private List<Item> inventoryItems = new List<Item>();
    private PickupItem nearbyItem;
    private bool isPaused = false;

    private UniversalTutorialManager universalTutorialManager;

    private CraftingManager craftingManager;
    private ListOfItems listOfItems;

    void Start()
    {
        inventoryUI = GameObject.Find("InventoryPanel");
        craftingZoneUI = GameObject.Find("CraftPanel");
        var inventoryButtonObject = GameObject.Find("InventoryButton");
        HUDButtons[0] = inventoryButtonObject;
        HUDButtons[1] = GameObject.Find("DiaryButton");
        inventoryButton = inventoryButtonObject.GetComponent<Button>();
        itemParent = GameObject.Find("SlotParent").transform;
        slotPrefab = Resources.Load<GameObject>("SlotPrefab");

        if (inventoryUI == null || inventoryButton == null || itemParent == null || slotPrefab == null)
        {
            Debug.LogError("Не все элементы UI настроены в инспекторе!");
            return;
        }

        inventoryUI.SetActive(false);
        craftingZoneUI.SetActive(false);
        inventoryButton.onClick.AddListener(ToggleInventory);

        universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();

        listOfItems = ServiceLocator.Get<ListOfItems>();

        if (autoInventoryItems.Count > 0)
        {
            listOfItems.AutoInventoryItems = autoInventoryItems;
        }
        AutoFillInventory();
    }

    private void AutoFillInventory()
    {
        foreach (string itemName in listOfItems.ItemNames)
        {
            Item existingItem = listOfItems.AutoInventoryItems.Find(item => item.name == itemName);

            if (existingItem != null ) 
            {
                AddItem(existingItem); 
                Debug.LogWarning($"Предмет автоматически {existingItem.name} добавлен в инвентарь.");
            }
            else
            {
                Debug.LogWarning($"Предмет {itemName} отсутствует в inventoryItems.");
            }
        }

    }

    public void RemoveItemFromInventory(string itemName)
    {
        foreach (Item item in inventoryItems)
        {
            if (item.name == itemName)
            {
                RemoveItem(item);
                listOfItems.RemoveItemFromList(itemName);

                break;
            }
            else
            {
                Debug.LogWarning("Нельзя удалить этот итем: он не существует");
            }
        }

    }

    void Update()
    {
        
        // Открытие/закрытие инвентаря
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            ToggleInventory();
            
            
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {   
            Debug.LogWarning(listOfItems.ItemExists("bag"));
            Debug.LogWarning("Количество итемов в целом: " + listOfItems.AutoInventoryItems.Count);
        }



        // Подбор предмета по кнопке F
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (nearbyItem != null)
        //    {
        //        PickupNearbyItem(); //временная штука для тестов
        //        Debug.LogWarning("В зоне пика предмета");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Рядом нет предметов для подбора!");
        //    }
        //}
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

        universalTutorialManager.TriggerTutorial("Inventory"); //Триггер на появление окна туториала
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
        /*        Debug.Log(ListOfItems.ItemExists("bag"));
                Debug.Log(item.name);*/

        inventoryItems.Add(item);
        UpdateInventoryUI();

        if (!listOfItems.ItemNames.Contains(item.name))
        {
            listOfItems.ItemNames.Add(item.name);
            Debug.Log($"Название {item.name} добавлено в глобальный список.");
        }
        else
        {
            Debug.Log($"Название {item.name} уже есть в глобальном списке.");
        }
        Debug.Log("bag существует: " + listOfItems.ItemExists("bag"));
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

    public void PickupNearbyItem()
    {
        if (nearbyItem != null && nearbyItem.item != null)
        {
            
            AddItem(nearbyItem.item);
            Destroy(nearbyItem.gameObject);
            HidePickupUI();
            Debug.LogWarning("Добавили в инвентарь");
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


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class InventoryManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject inventoryUI; // ������ ���������
    public GameObject craftingZoneUI;
    public List<GameObject> HUDButtons = new List<GameObject>();
    public Button inventoryButton; // ������ ��� �������� ���������
    public Transform itemParent;   // �������� ��� ������ ���������
    public GameObject slotPrefab;  // ������ ����� ��� ���������
    public List<Item> autoInventoryItems = new List<Item>();

    private bool isInventoryOpen = false;
    private List<Item> inventoryItems = new List<Item>();
    private PickupItem nearbyItem;
    private bool isPaused = false;

    private UniversalTutorialManager universalTutorialManager;

    private CraftingManager craftingManager;

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
            Debug.LogError("�� ��� �������� UI ��������� � ����������!");
            return;
        }

        inventoryUI.SetActive(false);
        craftingZoneUI.SetActive(false);
        inventoryButton.onClick.AddListener(ToggleInventory);

        universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();
        if (autoInventoryItems.Count > 0)
        {
            ListOfItems.autoInventoryItems = autoInventoryItems;
        }
        AutoFillInventory();
    }

    private void AutoFillInventory()
    {
        foreach (string itemName in ListOfItems.ItemNames)
        {
            Item existingItem = ListOfItems.autoInventoryItems.Find(item => item.name == itemName);

            if (existingItem != null ) 
            {
                AddItem(existingItem); 
                Debug.LogWarning($"������� ������������� {existingItem.name} �������� � ���������.");
            }
            else
            {
                Debug.LogWarning($"������� {itemName} ����������� � inventoryItems.");
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
                ListOfItems.RemoveItemFromList(itemName);

                break;
            }
            else
            {
                Debug.LogWarning("������ ������� ���� ����: �� �� ����������");
            }
        }

    }

    void Update()
    {
        
        // ��������/�������� ���������
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            ToggleInventory();
            
            
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {   
            Debug.LogWarning(ListOfItems.ItemExists("bag"));
            Debug.LogWarning("���������� ������ � �����: " + ListOfItems.autoInventoryItems.Count);
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

        universalTutorialManager.TriggerTutorial("Inventory"); //������� �� ��������� ���� ���������
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

        if (!ListOfItems.ItemNames.Contains(item.name))
        {
            ListOfItems.ItemNames.Add(item.name);
            Debug.Log($"�������� {item.name} ��������� � ���������� ������.");
        }
        else
        {
            Debug.Log($"�������� {item.name} ��� ���� � ���������� ������.");
        }
        Debug.Log("bag ����������: " + ListOfItems.ItemExists("bag"));
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


using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public Transform inputSlot1;
    public Transform inputSlot2;
    public Transform outputSlot;
    public Transform inventorySlotParent;
    public Button craftButton;
    private InventoryManager inventoryManager;
    public List<Item> resultItem = new List<Item>();
    private Item itemInInput1;
    private Item itemInInput2;
    public GameObject slotPrefab;

    void Start()
    {
        craftButton.onClick.AddListener(Craft);
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found!");
        }
        EnsureSlotsCreated();
     
        //Debug.Log("Объект существует? - " + exist);
    }

    private void Update()
    {
        
        //Debug.Log("Объект существует? - " + exist);
    }

    public void AddItemToCraftSlot(Transform slot, Item item)
    {
        if (item != null) //&& slot.childCount == 0
        {
            GameObject newItem = Instantiate(inventoryManager.slotPrefab, slot);
            newItem.GetComponent<InventorySlot>().SetItem(item);
            inventoryManager.RemoveItem(item);
            itemInInput1 = (slot == inputSlot1) ? item : itemInInput1;
            itemInInput2 = (slot == inputSlot2) ? item : itemInInput2;
        }
    }

    private void Craft()
    {
        if (itemInInput1 != null && itemInInput2 != null)
        {
            Item craftedItem = CraftItem(itemInInput1, itemInInput2);
            if (craftedItem != null)
            {
                CreateOutputItem(craftedItem);
                inventoryManager.RemoveItemFromInventory(itemInInput1.name);
                inventoryManager.RemoveItemFromInventory(itemInInput2.name);
                ClearInputSlots();
            }
        }
    }


    private Item CraftItem(Item item1, Item item2)
    {
        
        
        if ((item1.itemName == "picklock" && item2.itemName == "screwdriver") || (item1.itemName == "screwdriver" && item2.itemName == "picklock"))
        {
            return resultItem.FirstOrDefault(item => item.itemName == "tool"); //{ itemName = "tool", icon = Resources.Load<Texture2D>("Assets/_Project/Images/Slot/slot.png") }
        }
        return null; 
    }


    public void CreateOutputItem(Item outputItem)
    {
        if (outputSlot.childCount > 0) ClearSlot(outputSlot);
        

        if (inventoryManager != null)
        {
            inventoryManager.AddItem(outputItem); // ��������� � ��������� ����� ��������
        }
        else
        {
            Debug.LogError("InventoryManager is null!");
        }
    }

    private void ClearInputSlots()
    {
        ClearSlot(inputSlot1);
        ClearSlot(inputSlot2);
        ClearSlot(outputSlot);
        itemInInput1 = null;
        itemInInput2 = null;
        //EnsureSlotsCreated();

    }

    public void ClearSlot(Transform slot)
    {
        Debug.Log(slot.gameObject.name);
        Transform child = slot.Find("SlotPrefab(Clone)");
        if (child != null)
        {
            Destroy(child.gameObject);
        }

    }

    private void EnsureSlotsCreated()
    {
        CreateSlotIfNeeded(inputSlot1);
        CreateSlotIfNeeded(inputSlot2);
        CreateSlotIfNeeded(outputSlot);
    }
    private void CreateSlotIfNeeded(Transform slotTransform)
    {
        //Debug.Log(slotTransform.childCount);

        if (slotTransform.childCount == 0) 
        {
            //Debug.Log("������� ����");
            //Instantiate(slotPrefab, slotTransform);
            GameObject newSlot = Instantiate(slotPrefab, slotTransform);
            
            if (newSlot.GetComponent<RawImage>() == null)
            {
                newSlot.AddComponent<RawImage>();
            }
        }
    }

    
    

}
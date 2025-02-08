using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Global;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public RawImage iconImage; // UI-��������� ��� ����������� ��������
    public TMP_Text itemNameText; // UI-��������� ��� ����������� ����� �������� (�����������)
    private Item currentItem;

    private CanvasGroup canvasGroup;

    private Transform itemParent;
    private CraftingManager craftingManager;
    public static Item buffItem1;
    public static Item buffItem2;
    private ListOfItems listOfItems;
    public Transform GetItemParent()
    {
        return itemParent;
    }

    public void SetItem(Item item)
    {
        currentItem = item;

        if (currentItem != null)
        {
            if (iconImage != null)
            {
                iconImage.texture = currentItem.icon; // ������������� ��������
                iconImage.enabled = true;            // ���������� ��������
            }

            if (itemNameText != null)
            {
                itemNameText.text = currentItem.itemName; // ������������� ��� ��������
            }
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;

        if (iconImage != null)
        {
            iconImage.texture = null;
            iconImage.enabled = false; // �������� ��������
        }

        if (itemNameText != null)
        {
            itemNameText.text = string.Empty;
        }
    }


    public Item GetItem()
    {
        return currentItem;
    }


    //������� drag and drop
    private void Awake()
    {


            //����� ���������
            craftingManager = FindAnyObjectByType<CraftingManager>();
            if (craftingManager == null)
            {
                Debug.LogError("CraftingManager �� ������ � �����!");
            }
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        listOfItems = ServiceLocator.Get<ListOfItems>();
        if (eventData.button == PointerEventData.InputButton.Left && craftingManager != null && currentItem != null)
        {
            Debug.Log("�Ѩ �����" + craftingManager.inputSlot1.childCount + " " + craftingManager.inputSlot2.childCount);
            //craftingManager.inputSlot1.childCount == 1 || craftingManager.inputSlot1.childCount == 0
            //craftingManager.inputSlot1.childCount >= 0) &&currentItem != buffItem
            if ((craftingManager.inputSlot1.childCount == 1) && currentItem != buffItem1 && currentItem != buffItem2)
            {
                buffItem1 = currentItem;

                Debug.LogWarning("��������� � ������ ���� " + currentItem);
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot1, currentItem);

            }
            //craftingManager.inputSlot2.childCount == 1 || craftingManager.inputSlot1.childCount == 0
            //craftingManager.inputSlot2.childCount >= 0) && currentItem != buffItem
            else if ((craftingManager.inputSlot2.childCount == 1) && currentItem != buffItem2 && currentItem != buffItem1)
            {
                buffItem2 = currentItem;
                Debug.LogWarning("��������� �� ������ ���� " + currentItem);
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot2, currentItem);
            }
            else if (currentItem == buffItem1 || currentItem == buffItem2)
            {
                if (currentItem == buffItem1)
                {
                    Debug.LogWarning("�������� ������� ������� " + currentItem);
                    buffItem1 = null;
                    craftingManager.CreateOutputItem(currentItem);
                    ClearSlot();
                    craftingManager.ClearSlot(craftingManager.inputSlot1);
                    Debug.LogWarning("���������� ������ " + listOfItems.ItemNames.Count + " count of inputSlot1 childs " + craftingManager.inputSlot1.childCount);
                }
                else
                {
                    Debug.LogWarning("�������� ������� ������� " + currentItem);
                    buffItem2 = null;
                    craftingManager.CreateOutputItem(currentItem);
                    ClearSlot();
                    craftingManager.ClearSlot(craftingManager.inputSlot2);
                    Debug.LogWarning("���������� ������ " + listOfItems.ItemNames.Count + " count of inputSlot1 childs " + craftingManager.inputSlot2.childCount);
                }
            }
        }
    }




    //������������ �������� ��� ����������� ����������� ����� � ���������
    public void SetParent(Transform parent)
    {
        itemParent = parent;
    }
}

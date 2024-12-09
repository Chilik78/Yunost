using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public RawImage iconImage; // UI-��������� ��� ����������� ��������
    public TMP_Text itemNameText; // UI-��������� ��� ����������� ����� �������� (�����������)
    private Item currentItem;

    private CanvasGroup canvasGroup;

    private Transform itemParent;
    private CraftingManager craftingManager;

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
        Debug.Log("������ �� ������");
        Debug.Log(currentItem);
        Debug.Log(eventData.button);
        if (eventData.button == PointerEventData.InputButton.Left && craftingManager != null && currentItem != null)
        {
            Debug.Log("�Ѩ �����" + craftingManager.inputSlot1.childCount + craftingManager.inputSlot2.childCount);
            
            if (craftingManager.inputSlot1.childCount == 1 || craftingManager.inputSlot1.childCount == 0)
            {
                Debug.Log("��������� � ������ ����");
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot1, currentItem);

            }
            else if (craftingManager.inputSlot2.childCount == 1 || craftingManager.inputSlot1.childCount == 0)
            {
                Debug.Log("��������� �� ������ ����");
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot2, currentItem);
            }
        }
    }



    //������������ �������� ��� ����������� ����������� ����� � ���������
    public void SetParent(Transform parent)
    {
        itemParent = parent;
    }
}

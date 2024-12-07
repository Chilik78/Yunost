using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public RawImage iconImage; // UI-��������� ��� ����������� ��������
    public TMP_Text itemNameText; // UI-��������� ��� ����������� ����� �������� (�����������)

    private Item currentItem;

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
}
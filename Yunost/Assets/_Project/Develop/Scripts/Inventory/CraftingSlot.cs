using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public Item currentItem; // ������� � ���� �����
    public RawImage slotImage;  // UI-������� ��� ����������� ��������

    private void Start()
    {
        if (slotImage == null)
            slotImage = GetComponent<RawImage>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // ���������, ��� ������� ��� ���������
        DragAndDropItem draggedItem = eventData.pointerDrag.GetComponent<DragAndDropItem>();
        if (draggedItem != null)
        {
            SetItem(draggedItem.item);
        }
    }

    public void SetItem(Item item)
    {
        currentItem = item;

        if (slotImage != null && item != null)
        {
            slotImage.texture = item.icon; // ������������� �������� ��������
            slotImage.enabled = true;
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        if (slotImage != null)
        {
            slotImage.texture = null; // ������� ��������
            slotImage.enabled = false;
        }
    }
}

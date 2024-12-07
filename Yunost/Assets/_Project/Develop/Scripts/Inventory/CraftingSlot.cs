using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public Item currentItem; // Предмет в этом слоте
    public RawImage slotImage;  // UI-элемент для отображения предмета

    private void Start()
    {
        if (slotImage == null)
            slotImage = GetComponent<RawImage>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Проверяем, что предмет был перетащен
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
            slotImage.texture = item.icon; // Устанавливаем текстуру предмета
            slotImage.enabled = true;
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        if (slotImage != null)
        {
            slotImage.texture = null; // Очищаем текстуру
            slotImage.enabled = false;
        }
    }
}

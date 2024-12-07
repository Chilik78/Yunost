using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public RawImage iconImage; // UI-компонент для отображения текстуры
    public TMP_Text itemNameText; // UI-компонент для отображения имени предмета (опционально)

    private Item currentItem;

    public void SetItem(Item item)
    {
        currentItem = item;

        if (currentItem != null)
        {
            if (iconImage != null)
            {
                iconImage.texture = currentItem.icon; // Устанавливаем текстуру
                iconImage.enabled = true;            // Показываем текстуру
            }

            if (itemNameText != null)
            {
                itemNameText.text = currentItem.itemName; // Устанавливаем имя предмета
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
            iconImage.enabled = false; // Скрываем текстуру
        }

        if (itemNameText != null)
        {
            itemNameText.text = string.Empty;
        }
    }
}
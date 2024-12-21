using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public RawImage iconImage; // UI-компонент для отображения текстуры
    public TMP_Text itemNameText; // UI-компонент для отображения имени предмета (опционально)
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


    public Item GetItem()
    {
        return currentItem;
    }


    //Область drag and drop
    private void Awake()
    {


            //новые изменения
            craftingManager = FindAnyObjectByType<CraftingManager>();
            if (craftingManager == null)
            {
                Debug.LogError("CraftingManager не найден в сцене!");
            }
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("НАЖАЛИ НА РЖОМБУ");
        Debug.Log(currentItem);
        Debug.Log(eventData.button);
        if (eventData.button == PointerEventData.InputButton.Left && craftingManager != null && currentItem != null)
        {
            Debug.Log("ВСЁ ЧЕТКО" + craftingManager.inputSlot1.childCount + craftingManager.inputSlot2.childCount);
            
            if (craftingManager.inputSlot1.childCount == 1 || craftingManager.inputSlot1.childCount == 0)
            {
                Debug.Log("ДОБАВЛЯЕМ В ПЕРВЫЙ СЛОТ");
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot1, currentItem);

            }
            else if (craftingManager.inputSlot2.childCount == 1 || craftingManager.inputSlot1.childCount == 0)
            {
                Debug.Log("ДОБАВЛЯЕМ ВО ВТОРОЙ СЛОТ");
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot2, currentItem);
            }
        }
    }



    //Присваивание родителя для правильного возвращения слота в инвентарь
    public void SetParent(Transform parent)
    {
        itemParent = parent;
    }
}

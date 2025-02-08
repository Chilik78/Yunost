using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Global;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public RawImage iconImage; // UI-компонент для отображения текстуры
    public TMP_Text itemNameText; // UI-компонент для отображения имени предмета (опционально)
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

        listOfItems = ServiceLocator.Get<ListOfItems>();
        if (eventData.button == PointerEventData.InputButton.Left && craftingManager != null && currentItem != null)
        {
            Debug.Log("ВСЁ ЧЕТКО" + craftingManager.inputSlot1.childCount + " " + craftingManager.inputSlot2.childCount);
            //craftingManager.inputSlot1.childCount == 1 || craftingManager.inputSlot1.childCount == 0
            //craftingManager.inputSlot1.childCount >= 0) &&currentItem != buffItem
            if ((craftingManager.inputSlot1.childCount == 1) && currentItem != buffItem1 && currentItem != buffItem2)
            {
                buffItem1 = currentItem;

                Debug.LogWarning("ДОБАВЛЯЕМ В ПЕРВЫЙ СЛОТ " + currentItem);
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot1, currentItem);

            }
            //craftingManager.inputSlot2.childCount == 1 || craftingManager.inputSlot1.childCount == 0
            //craftingManager.inputSlot2.childCount >= 0) && currentItem != buffItem
            else if ((craftingManager.inputSlot2.childCount == 1) && currentItem != buffItem2 && currentItem != buffItem1)
            {
                buffItem2 = currentItem;
                Debug.LogWarning("ДОБАВЛЯЕМ ВО ВТОРОЙ СЛОТ " + currentItem);
                craftingManager.AddItemToCraftSlot(craftingManager.inputSlot2, currentItem);
            }
            else if (currentItem == buffItem1 || currentItem == buffItem2)
            {
                if (currentItem == buffItem1)
                {
                    Debug.LogWarning("Пытаемся вернуть обратно " + currentItem);
                    buffItem1 = null;
                    craftingManager.CreateOutputItem(currentItem);
                    ClearSlot();
                    craftingManager.ClearSlot(craftingManager.inputSlot1);
                    Debug.LogWarning("количество итемов " + listOfItems.ItemNames.Count + " count of inputSlot1 childs " + craftingManager.inputSlot1.childCount);
                }
                else
                {
                    Debug.LogWarning("Пытаемся вернуть обратно " + currentItem);
                    buffItem2 = null;
                    craftingManager.CreateOutputItem(currentItem);
                    ClearSlot();
                    craftingManager.ClearSlot(craftingManager.inputSlot2);
                    Debug.LogWarning("количество итемов " + listOfItems.ItemNames.Count + " count of inputSlot1 childs " + craftingManager.inputSlot2.childCount);
                }
            }
        }
    }




    //Присваивание родителя для правильного возвращения слота в инвентарь
    public void SetParent(Transform parent)
    {
        itemParent = parent;
    }
}

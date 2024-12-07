using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // Ссылка на предмет, который перетаскиваем
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Делает объект полупрозрачным
        canvasGroup.blocksRaycasts = false; // Позволяет игнорировать объект в Raycast
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale; // Перемещаем объект
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f; // Восстанавливает прозрачность
        canvasGroup.blocksRaycasts = true; // Включает Raycast обратно
    }
}

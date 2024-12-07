using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // ������ �� �������, ������� �������������
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // ������ ������ ��������������
        canvasGroup.blocksRaycasts = false; // ��������� ������������ ������ � Raycast
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale; // ���������� ������
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f; // ��������������� ������������
        canvasGroup.blocksRaycasts = true; // �������� Raycast �������
    }
}

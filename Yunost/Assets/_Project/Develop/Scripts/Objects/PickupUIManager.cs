using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupUIManager : MonoBehaviour
{
    public GameObject pickupTextPrefab;
    private GameObject currentTextInstance; 
    private Transform targetObject; 

    void Start()
    {
        // Создаём экземпляр текста и прячем его
        currentTextInstance = Instantiate(pickupTextPrefab, transform);
        currentTextInstance.SetActive(false);
    }

    void Update()
    {
        // Если есть цель, обновляем позицию текста
        if (targetObject != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetObject.position + Vector3.up * 2f);
            currentTextInstance.transform.position = screenPosition;
        }
    }

    public void ShowPickupText(PickupItem item)
    {
        if (currentTextInstance != null)
        {
            targetObject = item.transform; // Привязываем объект
            currentTextInstance.SetActive(true); // Показываем текст
            currentTextInstance.GetComponent<TMP_Text>().text = $"Нажмите F, чтобы поднять {item.item.itemName}"; // Устанавливаем текст {item.item.itemName}
            //Debug.Log(currentTextInstance.GetComponent<TMP_Text>().text);
        }
    }

    public void HidePickupText()
    {
        if (currentTextInstance != null)
        {
            currentTextInstance.SetActive(false); // Скрываем текст
            targetObject = null;
        }
    }
}


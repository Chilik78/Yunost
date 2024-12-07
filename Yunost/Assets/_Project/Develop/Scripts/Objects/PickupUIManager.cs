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
        // ������ ��������� ������ � ������ ���
        currentTextInstance = Instantiate(pickupTextPrefab, transform);
        currentTextInstance.SetActive(false);
    }

    void Update()
    {
        // ���� ���� ����, ��������� ������� ������
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
            targetObject = item.transform; // ����������� ������
            currentTextInstance.SetActive(true); // ���������� �����
            currentTextInstance.GetComponent<TMP_Text>().text = $"������� F, ����� ������� {item.item.itemName}"; // ������������� ����� {item.item.itemName}
            //Debug.Log(currentTextInstance.GetComponent<TMP_Text>().text);
        }
    }

    public void HidePickupText()
    {
        if (currentTextInstance != null)
        {
            currentTextInstance.SetActive(false); // �������� �����
            targetObject = null;
        }
    }
}


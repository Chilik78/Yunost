using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryManager : MonoBehaviour
{
    public GameObject diaryUI;
    public Button diaryButton;
    public TMP_Text diaryText;

    private bool isDiaryOpen = false;

    public void UpdateDiary(string mainTask, string[] subTasks)
    {
        string diaryContent = $"<b><color=blue>�������:</color></b> <color=black>{mainTask}</color>\n\n";

        if (subTasks != null && subTasks.Length > 0)
        {
            diaryContent += "<b><color=yellow>����������:</color></b>\n";
            for (int i = 0; i < subTasks.Length; i++)
            {
                diaryContent += $"<color=black>- {subTasks[i]}</color>\n";
            }
        }

        if (diaryText != null)
        {
            diaryText.text = diaryContent;
        }
    }

    private void Start()
    {

        if (diaryUI != null)
        {
            diaryUI.SetActive(false);
        }


        if (diaryUI != null)
        {
            diaryButton.onClick.AddListener(ToggleDiary);
        }


        //��� ������
        string mainTask = "����� �����";
        string[] subTasks = {
            "���������� � ��������",
            "�������� ���� �������",
            "����� ���� �����",
            "��������� �� ����"
        };

        //FindObjectOfType<DiaryManager>().UpdateDiary(mainTask, subTasks);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDiary();
        }
    }

    private void ToggleDiary()
    {

        isDiaryOpen = !isDiaryOpen;

        if (diaryUI != null)
        {
            diaryUI.SetActive(isDiaryOpen); 
        }

        Time.timeScale = isDiaryOpen ? 0f : 1f;

    }
}

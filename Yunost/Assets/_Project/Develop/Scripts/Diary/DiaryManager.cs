using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DiaryManager : MonoBehaviour
{
    public GameObject diaryUI;
    public Button diaryButton;
    public GameObject taskPanelPrefab; // ������ ������ �������, ���������� ������ � ���������� ������.
    public Transform taskListContent; 
    public ScrollRect scrollRect; // ������ �� Scroll Rect


    private bool _isDiaryOpen = false;

    // ��� �����
    public List<string> mainTasks = new List<string>();
    public List<List<string>> allSubTasks = new List<List<string>>();




    private void Start()
    {

        if (diaryUI != null)
        {
            diaryUI.SetActive(false);
        }

        if (diaryButton != null)
        {
            diaryButton.onClick.AddListener(ToggleDiary);
        }


        //��� �����
        mainTasks.Add("����� �����");
        List<string> task1Subtasks = new List<string> {
            "���������� � ��������",
            "�������� ���� �������",
            "����� ���� �����",
            "��������� �� ����" };
        allSubTasks.Add(task1Subtasks);

        mainTasks.Add("����� ����");
        List<string> task2Subtasks = new List<string> {
            "���������� � �����",
            "����� �����",
            "����� ������" };
        allSubTasks.Add(task2Subtasks);

        mainTasks.Add("����� �����");
        List<string> task3Subtasks = new List<string> {
            "���",
            "������",
            "���������" };
        allSubTasks.Add(task3Subtasks);

        mainTasks.Add("����� ����");
        List<string> task4Subtasks = new List<string> {
            "������",
            "��������",
            "����" };
        allSubTasks.Add(task4Subtasks);


        UpdateTaskList(mainTasks, allSubTasks);

    }

    private void UpdateTaskList(List<string> mainTasks, List<List<string>> allSubTasks)
    {
        // ������� ������ ������ �������


        // ������� ������ ��� ������� �������
        for (int i = 0; i < mainTasks.Count; i++)
        {
            GameObject taskPanelGO = Instantiate(taskPanelPrefab, taskListContent);
            TaskPanelController taskPanelController = taskPanelGO.GetComponent<TaskPanelController>();

            if (taskPanelController != null)
            {
                taskPanelController.SetTaskData(mainTasks[i], allSubTasks[i]);
            }
            else
            {
                Debug.LogError("TaskPanelPrefab �� ����� TaskPanelController!");
            }
        }
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

        _isDiaryOpen = !_isDiaryOpen;

        if (diaryUI != null)
        {
            diaryUI.SetActive(_isDiaryOpen); 
        }

        Time.timeScale = _isDiaryOpen ? 0f : 1f;

    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DiaryManager : MonoBehaviour
{
    public GameObject diaryUI;
    public Button diaryButton;
    public GameObject taskPanelPrefab; // Префаб панели задания, включающей кнопку и выпадающий список.
    public Transform taskListContent; 
    public ScrollRect scrollRect; // Ссылка на Scroll Rect


    private bool _isDiaryOpen = false;

    // Для теста
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


        //Для теста
        mainTasks.Add("Найти Шнюка");
        List<string> task1Subtasks = new List<string> {
            "Поговорить с Пупсенем",
            "Обыскать хату Вупсеня",
            "Найти клад бабки",
            "Вернуться на луну" };
        allSubTasks.Add(task1Subtasks);

        mainTasks.Add("Найти клад");
        List<string> task2Subtasks = new List<string> {
            "Поговорить с Кузей",
            "Взять карту",
            "Взыть лопату" };
        allSubTasks.Add(task2Subtasks);

        mainTasks.Add("Найти бабку");
        List<string> task3Subtasks = new List<string> {
            "Бла",
            "Блабла",
            "Блаблабла" };
        allSubTasks.Add(task3Subtasks);

        mainTasks.Add("Найти луну");
        List<string> task4Subtasks = new List<string> {
            "Бабаба",
            "Беребере",
            "бебе" };
        allSubTasks.Add(task4Subtasks);


        UpdateTaskList(mainTasks, allSubTasks);

    }

    private void UpdateTaskList(List<string> mainTasks, List<List<string>> allSubTasks)
    {
        // Очищаем старые кнопки заданий


        // Создаем панели для каждого задания
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
                Debug.LogError("TaskPanelPrefab не имеет TaskPanelController!");
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


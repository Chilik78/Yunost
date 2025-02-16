using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Global;
using ProgressModul;
using System.Linq;

public class DiaryManager : MonoBehaviour
{
    public GameObject diaryUI;
    public Button diaryButton;
    public TMP_Text diaryText;

    private bool isDiaryOpen = false;

    public void UpdateDiary(string mainTask, string[] subTasks)
    {
        string diaryContent = $"<b><color=blue>Задание:</color></b> <color=black>{mainTask}</color>\n\n";

        if (subTasks != null && subTasks.Length > 0)
        {
            diaryContent += "<b><color=yellow>Подзадания:</color></b>\n";
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


        /*//для тестов
        string mainTask = "Найти Шнюка";
        string[] subTasks = {
            "Поговорить с Пупсенем",
            "Обыскать хату Вупсеня",
            "Найти клад бабки",
            "Вернуться на луну"
        };*/

        var taskObserver = ServiceLocator.Get<TaskObserver>();

        var tasks = taskObserver.GetTasks(TaskState.InProgress, TaskType.Main, 0, 1000);
        UpdateTask(tasks.First());

        taskObserver.HaveNewSubTasks += UpdateSubTasks;
    }
    private Task _task; 
    private void UpdateSubTasks(IEnumerable<SubTask> subTasks)
    {
        UpdateDiary(_task.Name, subTasks.Select(x => x.Description).ToArray());
    }

    private void UpdateTask(Task task)
    {
        _task = task;
        if (_task == null)
        {
            Debug.LogWarning("Задания в прогрессе исчерпаны");
            return;
        }
        UpdateDiary(_task.Name, _task.CurrentSubTasks.Select(x => x.Description).ToArray());
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

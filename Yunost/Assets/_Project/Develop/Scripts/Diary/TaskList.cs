using System.Collections.Generic;
using UnityEngine;

public abstract class TaskList : MonoBehaviour
{
    public GameObject taskPanelPrefab;
    public Transform taskListContent;

    protected List<TaskData> tasks = new List<TaskData>(); 

    protected virtual void Start()
    {
        LoadTasks();
        UpdateTasksList();
    }

    protected abstract void LoadTasks(); 

    protected virtual void UpdateTasksList()
    {
        foreach (TaskData task in tasks)
        {
            GameObject taskPanel = Instantiate(taskPanelPrefab, taskListContent);
            TaskPanelController taskPanelController = taskPanel.GetComponent<TaskPanelController>();

            taskPanelController.SetTaskData(task.mainTask, task.subTasks); 

        }
    }
}

//Дата-класс для задач и подзадач
public class TaskData
{
    public string mainTask;
    public List<string> subTasks;

    public TaskData(string main, List<string> subs)
    {
        mainTask = main;
        subTasks = subs;
    }
}


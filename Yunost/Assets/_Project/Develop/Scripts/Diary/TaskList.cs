using Global;
using ProgressModul;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        ServiceLocator.Get<TaskObserver>().TaskStateChanged += UpdateOnChangedTask;
        ServiceLocator.Get<TaskObserver>().HaveNewSubTasks += UpdateOnChangedSubTasks;
    }

    private void UpdateOnChangedSubTasks(IEnumerable<SubTask> subTasks)
    {
        LoadTasks();
        UpdateTasksList();
    }

    private void UpdateOnChangedTask(Task task)
    {
        LoadTasks();
        UpdateTasksList();
    }

    protected abstract void LoadTasks(); 

    private List<GameObject> panels = new List<GameObject>();

    private void _clearPanels()
    {
        while(panels.Count > 0 )
        {
            Destroy(panels[0]);
            panels.RemoveAt(0);
        }
    }

    protected virtual void UpdateTasksList()
    {
        _clearPanels();
        foreach (TaskData task in tasks)
        {
            GameObject taskPanel = Instantiate(taskPanelPrefab, taskListContent);
            TaskPanelController taskPanelController = taskPanel.GetComponent<TaskPanelController>();

            taskPanelController.SetTaskData(task.mainTask, task.subTasks);
            panels.Add(taskPanel);
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


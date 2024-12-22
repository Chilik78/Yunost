using Global;
using ProgressModul;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _taskNameField;
    [SerializeField]
    private TMP_Text _taskDescriptionField;


    void Start()
    {
        var taskObserver = ServiceLocator.Get<TaskObserver>();
        Task currentTask = taskObserver.GetFirstInProgressTask;
        UpdateTask(currentTask);

        taskObserver.HaveNewTask += UpdateTask;
        taskObserver.HaveNewSubTask += UpdateDescription;
    }

    private void UpdateTask(Task task)
    {
        if (task == null) {
            Debug.LogWarning("Задания в прогрессе исчерпаны");
            return;
        }
        Debug.Log(task.Name);
        
        _taskNameField.text = task.Name;
        _taskDescriptionField.text = task.Description;//task.Description;
    }

    private void UpdateDescription(Task task)
    {
        if (task == null)
        {
            Debug.LogWarning("Задания в прогрессе исчерпаны");
            return;
        }
        Debug.Log(task.Name);

        _taskDescriptionField.text = task.Description;//task.Description;
    }
}

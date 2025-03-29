using Global;
using ProgressModul;
using System.Linq;

public class SecondaryTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: динамический подбор времени
        var secondaryTasks = ServiceLocator.Get<TaskObserver>().GetTasks(TaskState.InProgress, TaskType.Side, 1000);
        foreach (var task in secondaryTasks)
        {
            var name = task.Name;
            var subNames = task.CurrentSubTasks.Select(s => s.Description).ToList();
            tasks.Add(new TaskData(name, subNames));
        }
    }
}
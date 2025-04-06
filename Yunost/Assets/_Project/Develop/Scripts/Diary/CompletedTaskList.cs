using Global;
using ProgressModul;
using System.Collections.Generic;
using System.Linq;

public class CompletedTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: динамический подбор времени
        var mainDoneTasks = ServiceLocator.Get<TaskObserver>().GetTasks(TaskState.Done, TaskType.Main, 1000);
        var sideDoneTasks = ServiceLocator.Get<TaskObserver>().GetTasks(TaskState.Done, TaskType.Side, 1000);
        var doneTasks = new List<Task>(mainDoneTasks);
        doneTasks.AddRange(sideDoneTasks);
        foreach (var task in doneTasks)
        {
            var name = task.Name;
            var subNames = task.CurrentSubTasks.Select(s => s.Description).ToList();
            tasks.Add(new TaskData(name, subNames));
        }
    }
}

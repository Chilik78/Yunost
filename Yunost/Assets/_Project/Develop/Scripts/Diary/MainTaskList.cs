using Global;
using ProgressModul;
using System.Collections.Generic;
using System.Linq;


public class MainTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: динамический подбор времени
        var border = ServiceLocator.Get<GameTimeControl>().MainTime+1;
        var mainTasks = ServiceLocator.Get<TaskObserver>().GetTasks(TaskState.InProgress, TaskType.Main, border);
        foreach ( var task in mainTasks )
        {
            var name = task.Name;
            var subNames = task.CurrentSubTasks.Select(s => s.Description).ToList();
            tasks.Add(new TaskData(name, subNames));
        }
        
    }
}

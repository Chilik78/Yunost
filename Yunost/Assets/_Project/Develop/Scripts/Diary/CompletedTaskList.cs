using System.Collections.Generic;

public class CompletedTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: добавить корректные данные
        //Для теста
        tasks.Add(new TaskData("Выполненная задача 1", new List<string> { "Подзадача 1", "Подзадача 2" }));
    }
}

using System.Collections.Generic;

public class SecondaryTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: добавить корректные данные
        //Для теста
        tasks.Add(new TaskData("Второстепенная задача 1", new List<string> { "Подзадача 1", "Подзадача 2" }));
    }
}
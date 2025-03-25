using System.Collections.Generic;
using UnityEngine;


public class MainTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: добавить корректные данные
        //Для теста
        tasks.Add(new TaskData("Главная задача 1", new List<string> { "Подзадача 1", "Подзадача 2" }));
        tasks.Add(new TaskData("Главная задача 2", new List<string> { "Подзадача 1", "Подзадача 2" }));
    }
}

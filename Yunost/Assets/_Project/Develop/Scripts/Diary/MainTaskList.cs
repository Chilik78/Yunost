using System.Collections.Generic;
using UnityEngine;


public class MainTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: �������� ���������� ������
        //��� �����
        tasks.Add(new TaskData("������� ������ 1", new List<string> { "��������� 1", "��������� 2" }));
        tasks.Add(new TaskData("������� ������ 2", new List<string> { "��������� 1", "��������� 2" }));
    }
}

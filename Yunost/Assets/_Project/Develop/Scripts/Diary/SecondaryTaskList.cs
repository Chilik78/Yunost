using System.Collections.Generic;

public class SecondaryTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: �������� ���������� ������
        //��� �����
        tasks.Add(new TaskData("�������������� ������ 1", new List<string> { "��������� 1", "��������� 2" }));
        tasks.Add(new TaskData("�������������� ������ 2", new List<string> { "��������� 1", "��������� 2" }));
        tasks.Add(new TaskData("�������������� ������ 3", new List<string> { "��������� 1", "��������� 2" }));
        tasks.Add(new TaskData("�������������� ������ 4", new List<string> { "��������� 1", "��������� 2" }));
        tasks.Add(new TaskData("�������������� ������ 5", new List<string> { "��������� 1", "��������� 2" }));
    }
}
using System.Collections.Generic;

public class CompletedTaskList : TaskList
{
    protected override void LoadTasks()
    {
        tasks.Clear();

        // TODO: �������� ���������� ������
        //��� �����
        tasks.Add(new TaskData("����������� ������ 1", new List<string> { "��������� 1", "��������� 2" }));
    }
}

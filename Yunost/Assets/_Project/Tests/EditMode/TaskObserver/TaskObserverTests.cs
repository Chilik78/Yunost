using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ProgressModul;
using UnityEngine;

public class TaskObserverTests
{
    private TaskObserver taskObserver;

    [SetUp]
    public void SetUp()
    {
        string json = Resources.Load<TextAsset>("TestInitTasks").text;
        taskObserver = new TaskObserver(json);
    }


    [Test]
    public void TaskObserverReadJsonTasks()
    {
        var task = taskObserver.Tasks.Where(t => t.Id == "team_game").First();

        foreach (var s in task.SubTasks) {
            Debug.Log(s.GetType().Name);
            if (typeof(CounterSubTask) == s.GetType())
            {
                Debug.Log($"{(s as CounterSubTask).FinalCount}\n{s.Id}");
            }
        }

        Assert.AreEqual(8, task.SubTasks.Count());
    }

    /* [Test]
     public void TaskObserverWriteJsonTasks()
     {
         var task = taskObserver.Tasks.Where(t => t.Id == "team_game").First();

         foreach (var s in task.SubTasks)
         {
             Debug.Log(s.GetType().Name);
             if (typeof(CounterSubTask) == s.GetType())
             {
                 Debug.Log($"{(s as CounterSubTask).FinalCount}\n{s.Id}");
             }
         }

         Assert.AreEqual(0, 1);
     }*/

    [Test]
    public void GetTasks()
    {
        var tasks = taskObserver.GetTasks(TaskState.InProgress, TaskType.Main, 0, 2000);
        var count = tasks.Count();
        Debug.Log(count);

        Assert.AreEqual(4, count);
    }

    [Test]
    public void SetSubTaskStateById()
    {
        taskObserver.SetDoneSubTaskById("long_road", "talk_makar");
        var subTask = taskObserver.GetSubTaskById("long_road", "talk_makar");
        Debug.Log(subTask.Id);

        Assert.AreEqual(true, subTask.IsDone);
    }

    [Test]
    public void SetTaskStateById()
    {
        taskObserver.SetTaskStateById("long_road", TaskState.Failed);
        var task = taskObserver.GetTaskById("long_road");
        Debug.Log(task.Id);

        Assert.AreEqual(TaskState.Failed, task.State);
    }

    private IEnumerable<SubTask> _subTasks;
    private void _onSubTask(IEnumerable<SubTask> subTasks)
    {
        _subTasks = subTasks;
    }

    [Test]
    public void HaveNewSubTasks()
    {
        taskObserver.HaveNewSubTasks += _onSubTask;
        taskObserver.SetDoneSubTaskById("long_road", "talk_makar");
        taskObserver.HaveNewSubTasks -= _onSubTask;
        foreach (var subTask in _subTasks)
        {
            Debug.Log(subTask.Id);
        }

        Assert.AreEqual(1, _subTasks.Count());
    }

    [Test]
    public void StackIndexWithFriends()
    {
        taskObserver.HaveNewSubTasks += _onSubTask;
        taskObserver.SetDoneSubTaskById("long_road", "take_case");
        taskObserver.HaveNewSubTasks -= _onSubTask;

        foreach (var subTask in _subTasks)
        {
            Debug.Log(subTask.Id);
        }

        Assert.AreEqual(0, _subTasks.First().StackIndex);
    }

    private Task _task;
    private void _onTaskState(Task task)
    {
        _task = task;
    }

    [Test]
    public void TaskStateChanged()
    {
        const string id = "long_road";
        taskObserver.TaskStateChanged += _onTaskState;
        taskObserver.SetTaskStateById(id, TaskState.Done);
        taskObserver.TaskStateChanged -= _onTaskState;

        Assert.AreEqual(id, _task.Id);
    }

    [Test]
    public void IsTaskInProgress()
    {
        const string id = "long_road";
        //foreach (var t in taskObserver.GetTasks(TaskState.InProgress, TaskType.Main, 10000)) Debug.Log(t.ToString());
        taskObserver.SetDoneSubTaskById("long_road", "take_case");
        var task = taskObserver.GetTaskById(id);
        Debug.Log(task.ToString());
        Debug.Log(task.SubTasks.All(s => s.IsDone));
        var res = taskObserver.IsTaskInProgress(id, TaskType.Main);
        Assert.IsTrue(res);
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /*[UnityTest]
    public IEnumerator TaskObserverWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}

using System.Collections;
using System.Linq;
using NUnit.Framework;
using ProgressModul;
using UnityEngine;
using UnityEngine.TestTools;

public class TaskObserverTests
{
    private TaskObserver taskObserver;

    [SetUp]
    public void SetUp()
    {
        string json = Resources.Load<TextAsset>("InitTasks").text;
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
    public void SetTaskStateById()
    {
        var tasks = taskObserver.GetTasks(TaskState.InProgress, TaskType.Main, 0, 2000);
        var count = tasks.Count();
        Debug.Log(count);

        Assert.AreEqual(4, count);
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

using System.Collections;
using NUnit.Framework;
using ProgressModul;
using UnityEngine;
using UnityEngine.TestTools;

public class Dialoges
{
    private TaskObserver taskObserver;

    [SetUp]
    public void SetUp()
    {
        string json = Resources.Load<TextAsset>("TestInitTasks").text;
        taskObserver = new TaskObserver(json);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void DialogesRoad()
    {
        taskObserver.SetDoneSubTaskById("long_road", "take_case");
        taskObserver.SetDoneSubTaskById("long_road", "go_to_camp");
        foreach (var t in taskObserver.GetTasks(TaskState.InProgress, TaskType.Main, 10000)) Debug.Log(t.ToString());
        var res = taskObserver.IsTaskInProgress("help_for_friend", TaskType.Main);
        Assert.IsTrue(res);
    }
/*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DialogesWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}

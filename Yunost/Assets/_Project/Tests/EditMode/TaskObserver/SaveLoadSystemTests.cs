using System.Collections;
using System.Linq;
using Global;
using NUnit.Framework;
using ProgressModul;
using UnityEngine;
using UnityEngine.TestTools;

public class SaveLoadSystemTests
{
    private TaskObserver taskObserver;
    private SaveLoadSystem saveLoadSystem;

    [SetUp]
    public void SetUp()
    {
        string json = Resources.Load<TextAsset>("TestInitTasks").text;
        taskObserver = new TaskObserver(json);

        saveLoadSystem = new SaveLoadSystem();
        saveLoadSystem.AddToSaveLoad(taskObserver);

        saveLoadSystem.SaveGame(SaveType.File);
        saveLoadSystem.LoadGame(SaveType.File);
    }

    [Test]
    public void TaskObserverSaveLoad()
    {
        var tasks = taskObserver.GetTasks(TaskState.InProgress, TaskType.Main, 0, 2000);
        var count = tasks.Count();
        Debug.Log(count);

        Assert.AreEqual(4, count);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /*[UnityTest]
    public IEnumerator SaveLoadSystemWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}

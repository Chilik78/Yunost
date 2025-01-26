using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using ProgressModul;
using UnityEngine;
using UnityEngine.TestTools;

public class TaskTests
{
    List<Task> tasks;
    [SetUp]
    public void Setup()
    {
        string json = Resources.Load<TextAsset>("InitTasks").text;
        tasks = TaskObserver.ParseJsonWithTasks(json);
    }

    [Test]
    public void GetCurrentSubTasks()
    {
        var subTasks = tasks[0].CurrentSubTasks;
        Assert.AreEqual(2, subTasks.Count());
    }

    [Test]
    public void ChangeFlow()
    {
        tasks[0].Flow = "2";
        Assert.AreEqual(tasks[0].Flow, "2");
    }

    [Test]
    public void SetDoneWithFriends()
    {
        tasks[0].SetDoneSubTaskById("take_case");
        tasks[0].Flow = "2";

        var subTasks = tasks[0].CurrentSubTasks;
        Assert.AreEqual(1, subTasks.Count());
    }

    [Test]
    public void ChangeState()
    {
        Debug.Log(tasks[0].ToString());
        tasks[0].State = TaskState.Done;
       
        Assert.AreEqual(tasks[0].State, TaskState.Done);
    }


}

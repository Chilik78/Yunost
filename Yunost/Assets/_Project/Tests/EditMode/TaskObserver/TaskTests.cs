using System.Collections;
using System.Collections.Generic;
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
    // A Test behaves as an ordinary method
    [Test]
    public void SerializeFromJson()
    {
        foreach (var task in tasks[0].SubTasks) 
        {
            Debug.Log(task.GetModel.ToString());
        }
        
    }
}

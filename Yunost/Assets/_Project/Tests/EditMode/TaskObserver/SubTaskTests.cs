using NUnit.Framework;
using UnityEngine;
using ProgressModul;
using Newtonsoft.Json;

static class  Tests 
{
    static public (string, string) ProducedModelIsEqualToEarlyModel(SubTaskModel subTaskModel, SubTask subTask)
    {
        string earlyJson = JsonConvert.SerializeObject(subTaskModel);

        var newModel = subTask.GetModel;
        string newJson = JsonConvert.SerializeObject(newModel);

        return (earlyJson, newJson);
    }


    static public (string, string) ProducedCounterModelIsEqualToEarlyModel(CounterSubTaskModel counterSubTaskModel, CounterSubTask counterSubTask)
    {
        string earlyJson = JsonConvert.SerializeObject(counterSubTaskModel);

        var newModel = counterSubTask.GetModel;
        string newJson = JsonConvert.SerializeObject(newModel);
        return (earlyJson, newJson);
    }


    static public (string, string) DeserializationSubTaskModel(SubTask subTask)
    {
        var newModel = subTask.GetModel;
        string newJson = JsonConvert.SerializeObject(newModel);

        SubTaskModel deserial = JsonConvert.DeserializeObject<SubTaskModel>(newJson);
        Debug.Log(newModel.ToString());
        return (newModel.ToString(), deserial.ToString());
    }


    static public (string, string) DeserializationCounterSubTaskModel(CounterSubTask counterSubTask)
    {
        var newModel = counterSubTask.GetModel;
        string newJson = JsonConvert.SerializeObject(newModel);

        CounterSubTaskModel deserial = JsonConvert.DeserializeObject<CounterSubTaskModel>(newJson);

        return (newModel.ToString(), deserial.ToString());
    }


    static public (int, int) DecreaseStackIndex(SubTask subTask)
    {
        var prev = subTask.StackIndex;
        subTask.DeacreaseStackIndex();
        var current = subTask.StackIndex;

        return (prev, current);
    }


    static public (int, int) IncreaseCounterSubTask(CounterSubTask counterSubTask)
    {
        var prev = counterSubTask.CurrentCount;
        counterSubTask.SetDone();
        var current = counterSubTask.CurrentCount;
        Debug.Log(counterSubTask.ToString());
        return (prev, current);
    }


    static public bool CounterSubTaskDoneIfCurrentEqualToFinal(CounterSubTask counterSubTask)
    {
        counterSubTask.SetDone();
        return counterSubTask.IsDone;
    }
}

public class SubTaskTests
{
    static private SubTask subTask;
    static private SubTaskModel subTaskModel;

    private CounterSubTask counterSubTask;
    private CounterSubTaskModel counterSubTaskModel;

    private SubTask initSubTask;

    [SetUp]
    public void Setup()
    {
        subTaskModel = new SubTaskModel("id", "Описание", "1", 1, true, new string[]{"1", "2" });
        subTask = new SubTask(subTaskModel);

        counterSubTaskModel = new CounterSubTaskModel("id", "Описание", "1", 1, 10, 9, false);
        counterSubTask = new CounterSubTask(counterSubTaskModel);

        string json = Resources.Load<TextAsset>("InitTasks").text;
        initSubTask = TaskObserver.ParseJsonWithTasks(json)[0].SubTasksTest[0];
    }

    [TearDown]
    public void Teardown()
    {
       
    }

    [Test]
    public void ProducedModelIsEqualToEarlyModelFromInit()
    {
        var (r1, r2) = Tests.ProducedModelIsEqualToEarlyModel(initSubTask.GetModel, initSubTask);
        Assert.AreEqual(r1, r2);
    }

    [Test]
    public void DeserializationSubTaskModelFromInit()
    {
        var (r1, r2) = Tests.DeserializationSubTaskModel(initSubTask);
        Assert.AreEqual(r1, r2);
    }

    [Test]
    public void ProducedModelIsEqualToEarlyModel()
    {
        var (r1, r2) = Tests.ProducedModelIsEqualToEarlyModel(subTaskModel, subTask);
        Assert.AreEqual(r1, r2);
    }

    [Test]
    public void ProducedCounterModelIsEqualToEarlyModel()
    {
        var (r1, r2) = Tests.ProducedCounterModelIsEqualToEarlyModel(counterSubTaskModel, counterSubTask);
        Assert.AreEqual(r1, r2);
    }

    [Test]
    public void DeserializationSubTaskModel()
    {
        var (r1, r2) = Tests.DeserializationSubTaskModel(subTask);
        Assert.AreEqual(r1, r2);
    }

    [Test]
    public void DeserializationCounterSubTaskModel()
    {
        var (r1, r2) = Tests.DeserializationCounterSubTaskModel(counterSubTask);
        Assert.AreEqual(r1, r2);
    }

    [Test]
    public void DecreaseStackIndex()
    {
        var (prev, current) = Tests.DecreaseStackIndex(subTask);
        Assert.AreEqual(prev - 1, current);
    }

    [Test]
    public void IncreaseCounterSubTask()
    {
        var (prev, current) = Tests.IncreaseCounterSubTask(counterSubTask);
        Assert.AreEqual(prev + 1, current);
    }

    [Test]
    public void CounterSubTaskDoneIfCurrentEqualToFinal()
    {
        var res = Tests.CounterSubTaskDoneIfCurrentEqualToFinal(counterSubTask);
        Assert.IsTrue(res);
    }

    [Test]
    public void CounterSubTaskDescription()
    {
        const string desc = "Выкопать ямы в местах с Красными Флажками";
        const int finalCount = 3;
        CounterSubTaskModel model = new CounterSubTaskModel("digging", desc, "1", 4, finalCount);
        CounterSubTask counterSubTask = new CounterSubTask(model);
        Debug.Log(counterSubTask.Description);
        for(int i = 0; i < finalCount+1; i++)
        {
            var res = counterSubTask.SetDone();
            Debug.Log(counterSubTask.Description);
            Debug.Log(res);
        }  

        Assert.AreEqual($"{desc} {finalCount}/{finalCount}", counterSubTask.Description);
    }
}

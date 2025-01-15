using Ink.Runtime;
using ProgressModul;
using System.Collections.Generic;
using System.IO;

// Класс Ink переменных
public class DialogVariables : ISaveLoadObject
{
    // Словарь Ink переменных
    public Dictionary<string, Object> variables { get; private set; }

    public string ComponentSaveId => "DialogVariables";

    private Story _globalVariablesStory;
    private Story _currentStory;
    // Ink переменные
    
    // Включение прослушивания изменения Ink переменных 
    public void StartListening(Story story)
    {
        _currentStory = story;
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    // Остановка прослушивания изменения Ink переменных
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    // Изменение Ink переменных
    public void VariableChanged(string name, Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    public void ChangeVariable(string name, string value)
    {
        UnityEngine.Debug.Log($"Before: {name} : {value}");
        _globalVariablesStory.variablesState[name] = value;
        Object objectValue = _globalVariablesStory.variablesState.GetVariableWithName(name);
        variables.Remove(name);
        variables.Add(name, objectValue);
        if (_currentStory != null)
        {
            _currentStory.variablesState[name] = value;
        }
        UnityEngine.Debug.Log($"After: {_globalVariablesStory.variablesState[name]}");
    }
    
    // Установка состояния Ink переменных в Story
    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public SaveLoadData GetSaveLoadData()
    {
        return new DialogVariablesSaveLoadData(ComponentSaveId, _globalVariablesStory.ToJson());
    }

    public void RestoreValues(SaveLoadData loadData)
    {
        if (loadData?.Data == null || loadData.Data.Length < 1)
        {
            UnityEngine.Debug.LogError($"Can't restore values.");
            return;
        }

        // [0] - (field)

        string globalJsonStory = loadData.Data[0].ToString();
        _globalVariablesStory = new Story(globalJsonStory);

        // Получение Ink переменных и их значений
        variables = new Dictionary<string, Object>();
        foreach (string name in _globalVariablesStory.variablesState)
        {
            Object value = _globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }

    public void SetDefault()
    {
        string ink;
        using (StreamReader sr = new StreamReader(UnityEngine.Application.streamingAssetsPath + "/" + "InkJSON/globals.ink"))
        {
            ink = sr.ReadToEnd();
        }

        // Чтение файла global.ink и компиляция
        Ink.Compiler compiler = new Ink.Compiler(ink);
        _globalVariablesStory = compiler.Compile();

        // Получение Ink переменных и их значений
        variables = new Dictionary<string, Object>();
        foreach (string name in _globalVariablesStory.variablesState)
        {
            Object value = _globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }
}

using Ink.Runtime;
using System.Collections.Generic;

// Класс Ink переменных
public class DialogVariables
{
    // Словарь Ink переменных
    public Dictionary<string, Object> variables { get; private set; }
    private Story _globalVariablesStory;
    private Story _currentStory;
    // Ink переменные
    public DialogVariables(string inkFileContents)
    {
        // Чтение файла global.ink и компиляция
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        _globalVariablesStory = compiler.Compile();

        // Получение Ink переменных и их значений
        variables = new Dictionary<string, Object>();
        foreach(string name in _globalVariablesStory.variablesState)
        {
            Object value = _globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }
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
}

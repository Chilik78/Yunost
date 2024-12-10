using Ink.Runtime;
using System.Collections.Generic;
using System.IO;

// Класс Ink переменных
public class DialogVariables
{
    // Словарь Ink переменных
    public Dictionary<string, Object> variables { get; private set; }

    // Ink переменные
    public DialogVariables(string inkFileContents)
    {
        // Чтение файла global.ink и компиляция
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        // Получение Ink переменных и их значений
        variables = new Dictionary<string, Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }
    // Включение прослушивания изменения Ink переменных 
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    // Остановка прослушивания изменения Ink переменных
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    // Изменение Ink переменных
    private void VariableChanged(string name, Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
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

using Ink.Runtime;
using System.Collections.Generic;
using System.IO;

// ����� Ink ����������
public class DialogVariables
{
    // ������� Ink ����������
    public Dictionary<string, Object> variables { get; private set; }

    // Ink ����������
    public DialogVariables(string inkFileContents)
    {
        // ������ ����� global.ink � ����������
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        // ��������� Ink ���������� � �� ��������
        variables = new Dictionary<string, Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }
    // ��������� ������������� ��������� Ink ���������� 
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    // ��������� ������������� ��������� Ink ����������
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    // ��������� Ink ����������
    private void VariableChanged(string name, Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }
    
    // ��������� ��������� Ink ���������� � Story
    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}

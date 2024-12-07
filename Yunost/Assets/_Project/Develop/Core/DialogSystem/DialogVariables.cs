using Ink.Runtime;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Ink.UnityIntegration;

// ����� Ink ����������
public class DialogVariables
{
    // ������� Ink ����������
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    // Ink ����������
    public DialogVariables(string globalsFilePath)
    {
        // ������ ����� global.ink � ����������
        string inkFileContents = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        // ��������� Ink ���������� � �� ��������
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
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
    private void VariableChanged(string name, Ink.Runtime.Object value)
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
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}

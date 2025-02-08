using UnityEngine;
using System.Collections.Generic;

public class UniversalTutorialManager : MonoBehaviour
{
    private TutorialManager tutorialManager; 
    public Dictionary<string, string> windowMessages = new Dictionary<string, string>(); // ��������� ��� ����

    private HashSet<string> triggeredWindows = new HashSet<string>(); // �������� ����, ��� ������� �������� ��� �����������

    public void AddMessage(string windowName, string message)
    {
        if (!windowMessages.ContainsKey(windowName))
        {
            windowMessages.Add(windowName, message);
        }
    }

    private void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
    public void TriggerTutorial(string windowName)
    {
        if (windowMessages.ContainsKey(windowName) && !triggeredWindows.Contains(windowName))
        {
            Debug.LogWarning($"���� ��������� {windowName} ��������");;
            triggeredWindows.Add(windowName);
            tutorialManager.StartTutorial(windowMessages[windowName]);
        }
    }
}


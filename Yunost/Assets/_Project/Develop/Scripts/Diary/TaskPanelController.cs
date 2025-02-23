using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskPanelController : MonoBehaviour
{
    public Button taskButton;
    public TMP_Dropdown subTaskDropdown;
    public TMP_Text taskText;

    private List<string> _subTasks;
    private bool _initialized = false;

    public void SetTaskData(string mainTask, List<string> subTasks)
    {
        _subTasks = subTasks;
        taskText.text = mainTask;

        InitializeDropdown();

        UpdateSubtaskDropdown(subTasks);

        // �����������
        taskButton.onClick.RemoveAllListeners();
        taskButton.onClick.AddListener(ToggleDropdownVisibility);

        // ������ ������
        subTaskDropdown.onValueChanged.RemoveAllListeners();
        subTaskDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        subTaskDropdown.value = 0;
    }

    private void InitializeDropdown()
    {
        if (_initialized) return;

        // ��������� �� ���� ����� "���������"
        TMP_Dropdown.OptionData placeholder = new TMP_Dropdown.OptionData("���������");
        subTaskDropdown.options.Insert(0, placeholder);
        _initialized = true;
    }

    private void UpdateSubtaskDropdown(List<string> subtasks)
    {

        // ������� ��� ��������, ����� ������� "���������"
        while (subTaskDropdown.options.Count > 1)
        {
            subTaskDropdown.options.RemoveAt(1);
        }

        // ��������� ���������
        foreach (string subtask in subtasks)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(subtask);
            subTaskDropdown.options.Add(option);
        }
        subTaskDropdown.value = 0;
        //subTaskDropdown.captionText.text = "���������";
    }

    public void ToggleDropdownVisibility()
    {
        subTaskDropdown.gameObject.SetActive(!subTaskDropdown.gameObject.activeSelf);
    }

    public void OnDropdownValueChanged(int value)
    {
        subTaskDropdown.value = 0;
    }
}
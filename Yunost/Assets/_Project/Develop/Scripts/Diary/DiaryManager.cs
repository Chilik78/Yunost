using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaryManager : MonoBehaviour
{


    public GameObject diaryUI;
    public Button diaryButton;
    public GameObject taskPanelPrefab;

    public MainTaskList mainTaskList;
    public SecondaryTaskList secondaryTaskList;
    public CompletedTaskList completedTaskList;

    private bool _isDiaryOpen = false;




    private void Start()
    {
        if (diaryUI != null)
        {
            diaryUI.SetActive(false);
        }

        if (diaryButton != null)
        {
            diaryButton.onClick.AddListener(ToggleDiary);
        }

    
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleDiary();
        }
    }

    private void ToggleDiary()
    {
        _isDiaryOpen = !_isDiaryOpen;

        if (diaryUI != null)
        {
            diaryUI.SetActive(_isDiaryOpen);
            
        }

        Time.timeScale = _isDiaryOpen ? 0f : 1f;
    }
}
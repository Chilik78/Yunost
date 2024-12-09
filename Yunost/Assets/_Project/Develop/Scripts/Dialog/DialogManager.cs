using Ink.Runtime;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Ink.UnityIntegration;
using MiniGames;
using Global;
using ProgressModul;

public class DialogManager : MonoBehaviour
{
    // ���� global.ink
    [Header("Global Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    // ������
    [Header("Dialog UI")]
    // ���������� ������
    [SerializeField] private GameObject dialoguePanel;
    // ����� ������� �������
    [SerializeField] private TextMeshProUGUI dialogueText;
    // ������ ������ �������� ��������/������
    [SerializeField] private GameObject[] choices;

    
    private TextMeshProUGUI[] choicesText;

    private static DialogManager instance;

    public bool dialogIsPlaying { get; private set; }

    private Story currentStory;

    private DialogVariables dialogVariables;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("�� ����� ������ ������ �������");
        }
        instance = this;

        // ������������� Ink ����������
        dialogVariables = new DialogVariables(globalsInkFile.filePath);
    }

    // ��������� ������� 
    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        // ���������� ��������� �� ������ ������
        foreach (GameObject choice in choices)
        {
            choice.GetComponent<Button>().onClick.AddListener(() => ContinueStory());
        }

        // ����� ��� �������
        dialogIsPlaying = false;

        // ���������� ���� ���������
        dialoguePanel.SetActive(false);

        // ����� ��� ������ ������
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogIsPlaying)
        {
            return;
        }

        // ���� ����������� �������� ������ -> ���������� �������
        if (currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }

    }

    // �������� ����������� ����
    public void EnterDialogMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogIsPlaying = true;
        dialoguePanel.SetActive(true);

        // ������ ������������� ��������� Ink ����������
        dialogVariables.StartListening(currentStory);

        
        currentStory.BindExternalFunction("startMiniGame", () => {
            MiniGameContext testContext = new MiniGameContext(TypesMiniGames.BreakingLock, 0f, 5);
            GameObject.Find("GameSystems").GetComponent<MiniGamesManager>().RunMiniGame(testContext);
        });
        

        currentStory.BindExternalFunction("setDoneTask", () => {
            ServiceLocator.Get<TaskObserver>().SetDoneFirstTask();
        });
        
        ContinueStory();
    }

    // �������� ����������� ����
    private IEnumerator ExitDialogMode()
    {
        yield return new WaitForSeconds(0.2f);

        // ��������� ������������� ��������� Ink ����������
        dialogVariables.StopListening(currentStory);

        dialogIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // ����������� �������
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // ���������� ������� �������
            dialogueText.text += "\n" + currentStory.Continue();

            // ��������� ������ ������ � ��������� ��������� ������
            DisplayChoices();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            // �������� ����������� ����
            StartCoroutine(ExitDialogMode());
        }
    }

    // ��������� ������ ������ ���������� ���������
    private void DisplayChoices()
    {
        // ���� ��������� ������ � InkJSON
        List<Choice> currentChoices = currentStory.currentChoices;


        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("���������� ������� � Ink ��������� ��������� ����� ������ ������ � UI");
        }

        int index = 0;

        // ��������� ������ ������ �� UI � ��������� �� ������
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        
    }

    // ��������� ������� ������� ������ �������������
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    // ��������� �������� Ink ���������� 
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable = null: " + variableName);
        }
        return variableValue;
    }
}

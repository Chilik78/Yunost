using Ink.Runtime;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniGames;
using Global;
using ProgressModul;

public class DialogManager : MonoBehaviour
{
    private string globalsInkFile;

    // ������
    [Header("Dialog UI")]
    // ���������� ������
    [SerializeField] private GameObject dialoguePanel;
    // ����� ������� �������
    [SerializeField] private TextMeshProUGUI dialogueText;
    // ������ ������ �������� ��������/������
    [SerializeField] private GameObject[] choices;

    //[SerializeField] private ScrollRect scrollRect;

    private CraftingManager craftManager;

    private InventoryManager inventoryManager;

    private TextMeshProUGUI[] choicesText;

    private static DialogManager instance;

    public bool dialogIsPlaying { get; private set; }

    private Story currentStory;

    private DialogVariables dialogVariables;

    private string tmpText;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("�� ����� ������ ������ �������");
        }

        instance = this;
   
        dialogVariables = ServiceLocator.Get<DialogVariables>();    
    }

    // ��������� ������� 
    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        var taskObserver = ServiceLocator.Get<TaskObserver>();
        dialogVariables.ChangeVariable("CurrentQuest", taskObserver.GetFirstInProgressTask.Id);
        dialogVariables.ChangeVariable("CurrentSubquest", taskObserver.GetFirstInProgressTask.GetFirstInProgressSubTask.Id);
        taskObserver.HaveNewTask += (Task task) => dialogVariables.ChangeVariable("CurrentQuest", task.Id);
        taskObserver.HaveNewSubTask += (Task task) => dialogVariables.ChangeVariable("CurrentSubquest", task.GetFirstInProgressSubTask.Id);
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

        if (isTyping && Input.GetKeyDown(KeyCode.Backspace))
        {
            CompleteTypingCurrentLine();
        }

        if (!dialogIsPlaying)
        {
            return;
        }


        // ���� ����������� �������� ������ -> ���������� �������
        if (currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }



        if (!isTyping && currentStory.currentChoices.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentStory.currentChoices.Count > 0)
            {
                MakeChoice(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentStory.currentChoices.Count > 1)
            {
                MakeChoice(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && currentStory.currentChoices.Count > 2)
            {
                MakeChoice(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && currentStory.currentChoices.Count > 3)
            {
                MakeChoice(3);
            }
        }


    }

    // �������� ����������� ����
    public void EnterDialogMode(string json)
    {
        currentStory = new Story(json);
        dialogIsPlaying = true;
        dialoguePanel.SetActive(true);

        // ������ ������������� ��������� Ink ����������
        dialogVariables.StartListening(currentStory);

        currentStory.BindExternalFunction("itemIsExist", (string item) => {
            bool isExist = ServiceLocator.Get<ListOfItems>().ItemExists(item);
            return isExist;
        });

        // ����� ���� ����   
        currentStory.BindExternalFunction("startMiniGame", () => {
            MiniGameContext testContext = new MiniGameContext(TypesMiniGames.BreakingLock, TypeDifficultMiniGames.Easy, 0f, 5);
            GameObject.Find("GameSystems").GetComponent<MiniGamesManager>().RunMiniGame(testContext);
        });

        // �������� �� ������� �������� � ���������
        currentStory.BindExternalFunction("pickupItem", (string item) => {
            craftManager = FindAnyObjectByType<CraftingManager>();
            inventoryManager = FindAnyObjectByType<InventoryManager>();
            inventoryManager.PickupNearbyItem();
        });

        // ����� ���������� �������
        currentStory.BindExternalFunction("setDoneTask", (string taskId) => {
            ServiceLocator.Get<TaskObserver>().SetDoneTaskById(taskId);
        });

        // ����� ���������� ����������
        currentStory.BindExternalFunction("setDoneSubTask", (string taskId, string subTaskId) => {
            ServiceLocator.Get<TaskObserver>().SetDoneSubTaskByIds(taskId, subTaskId);
        });

        // ���������� ��������
        currentStory.BindExternalFunction("hitHealth", (int value) => {
            ServiceLocator.Get<PlayerStats>().HitHealth(value);
        });

        // ����� �����
        currentStory.BindExternalFunction("changeScene", (string sceneName) => {
            StartCoroutine(ServiceLocator.Get<SceneControl>().LoadNewSceneAsync(sceneName));
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

    private void SetChoicesInteractable(bool interactable)
    {
        foreach (GameObject choice in choices)
        {
            choice.GetComponent<Button>().interactable = interactable;
        }
    }


    //������� ������� ��� ��������
    private Queue<string> dialogueQueue = new Queue<string>();
    //������� ���� ��� �������� ������ � ������ ������
    private bool isTyping = false;

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {

            string nextLine = currentStory.Continue();


            string[] lines = nextLine.Split('\n');

            foreach (string line in lines)
            {
                //Debug.Log($"��������� � �������: {line}");
                dialogueQueue.Enqueue(line.Trim());
                Debug.LogWarning("��������� ������" + line);
            }



            if (!isTyping && dialogueQueue.Count > 0)
            {
                StopAllCoroutines();
                
                StartCoroutine(TypeText(dialogueQueue.Dequeue()));
            }


            DisplayChoices();

        }
        else
        {
            SystemManager.GetInstance().UnfreezePlayer();
            StartCoroutine(ExitDialogMode());
        }
    }



    private IEnumerator TypeText(string newLine)
    {
        tmpText = newLine;
        isTyping = true;
        SetChoicesInteractable(false);

        dialogueText.text += "\n";

        foreach (char letter in newLine)
        {
            dialogueText.text += letter;


            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                dialogueText.text += newLine.Substring(dialogueText.text.Length - dialogueText.text.LastIndexOf('\n') - 1);
                //Debug.Log("������� ��� ���� � TypeText" + newLine.Substring(dialogueText.text.Length - dialogueText.text.LastIndexOf('\n') - 1));
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
        SetChoicesInteractable(true);


        if (dialogueQueue.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(TypeText(dialogueQueue.Dequeue()));
        }
    }


    private void CompleteTypingCurrentLine()
    {
        StopAllCoroutines();

        if (dialogueQueue.Count > 0)
        {
            string currentText = dialogueText.text;

            // ������� ������ ������� \n
            int newLineIndex = currentText.LastIndexOf('\n');
            if (newLineIndex >= 0)
            {
                // ��������� ����� �� ������� \n
                dialogueText.text = currentText.Substring(0, newLineIndex + 1);
            }
            else
            {
                // ���� \n �� ������, ��������� ����� ��� ���������
                Debug.LogWarning("������ ����� ������ \n �� ������.");
            }

            //dialogueText.text = "";

            string remainingText = tmpText;//dialogueQueue.Dequeue()
            Debug.LogWarning("���������� �����" + remainingText);

            dialogueText.text += remainingText;//+"\n"

            isTyping = false;
            SetChoicesInteractable(true);

            if (dialogueQueue.Count > 0)
            {
                StartCoroutine(TypeText(dialogueQueue.Dequeue()));
            }
        }
        else
        {
            Debug.LogWarning("������� ����� ��� ���������� ������.");
        }
    }




    // ��������� ������ ������ ���������� ���������
    private void DisplayChoices()
    {
        // ���� ��������� ������ � InkJSON
        List<Choice> currentChoices = currentStory.currentChoices;


        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("���������� ������� � Ink ��������� ��������� ����� ������ ������ � UI");
        }

        int index = 0;

        // ��������� ������ ������ �� UI � ��������� �� ������
        foreach (Choice choice in currentChoices)
        {

            choices[index].gameObject.SetActive(true);
            choicesText[index].text = $"{index + 1}. {choice.text}"; // choice.text
            index++;
        }

        for (int i = index; i < choices.Length; i++)
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


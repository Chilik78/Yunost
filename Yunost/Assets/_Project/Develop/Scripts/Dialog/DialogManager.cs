using Ink.Runtime;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniGames;
using Global;
using ProgressModul;
using System.IO;

public class DialogManager : MonoBehaviour
{
    private string globalsInkFile;

    // Диалог
    [Header("Dialog UI")]
    // Диалоговая панель
    [SerializeField] private GameObject dialoguePanel;
    // Текст истории диалога
    [SerializeField] private TextMeshProUGUI dialogueText;
    // Кнопки выбора варинтов действия/ответа
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
            Debug.LogWarning("На сцене больше одного диалога");
        }

        instance = this;

        // Инициализация Ink переменных
        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + "InkJSON/globals.ink"))
        {
            globalsInkFile = sr.ReadToEnd();
        }

        dialogVariables = new DialogVariables(globalsInkFile);

    }

    // Получение объекта 
    public static DialogManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        // Добавление прослушки на кнопки выбора
        foreach (GameObject choice in choices)
        {
            choice.GetComponent<Button>().onClick.AddListener(() => ContinueStory());
        }

        // Игрок вне диалога
        dialogIsPlaying = false;

        // Диалоговое окно отключено
        dialoguePanel.SetActive(false);

        // Текст для кнопок выбора
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

        // Если отсутствуют варианты выбора -> продолжать историю
        if (currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }

    }

    // Открытие диалогового окна
    public void EnterDialogMode(string json)
    {
        currentStory = new Story(json);
        dialogIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Начало прослушивания изменения Ink переменных
        dialogVariables.StartListening(currentStory);

        
        currentStory.BindExternalFunction("startMiniGame", () => {
            MiniGameContext testContext = new MiniGameContext(TypesMiniGames.BreakingLock, 0f, 5);
            GameObject.Find("GameSystems").GetComponent<MiniGamesManager>().RunMiniGame(testContext);
        });
        

        currentStory.BindExternalFunction("setDoneTask", () => {
            ServiceLocator.Get<TaskObserver>().SetDoneNextFirstTaskSubTask();
        });
        
        ContinueStory();
    }

    // Закрытие диалогового окна
    private IEnumerator ExitDialogMode()
    {
        yield return new WaitForSeconds(0.2f);

        // Окончание прослушивания изменения Ink переменных
        dialogVariables.StopListening(currentStory);

        dialogIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    // Продолжение истории
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // Обновление истории диалога
            dialogueText.text += "\n" + currentStory.Continue();

            // Включение кнопок выбора и получения вариантов выбора
            DisplayChoices();
        }
        else
        {
            SystemManager.GetInstance().UnfreezePlayer();
            SystemManager.GetInstance().MiniGamesManager.MiniGameEnd += (MiniGameResultInfo info) => ServiceLocator.Get<SceneControl>().GoToScene(2);
            // Закрытие диалогового окна
            StartCoroutine(ExitDialogMode());
        }
    }

    // Включение кнопок выбора диалоговых вариантов
    private void DisplayChoices()
    {
        // Лист вариантов выбора в InkJSON
        List<Choice> currentChoices = currentStory.currentChoices;


        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("Количество выборов в Ink превышает доступное число кнопок выбора в UI");
        }

        int index = 0;

        // Включение кнопок выбора на UI и изменение их текста
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

    // Получение индекса нажатой кнопки пользователем
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    // Получение значения Ink переменной 
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

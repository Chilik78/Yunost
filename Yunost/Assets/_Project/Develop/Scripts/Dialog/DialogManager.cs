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

    //[SerializeField] private ScrollRect scrollRect;

    private CraftingManager craftManager;

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
            StartCoroutine(ServiceLocator.Get<SceneControl>().LoadNewSceneAsync(Scenes.HubHome));
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

    // Открытие диалогового окна
    public void EnterDialogMode(string json)
    {
        currentStory = new Story(json);
        dialogIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Начало прослушивания изменения Ink переменных
        dialogVariables.StartListening(currentStory);

        
        // Старт Мини игры   
        currentStory.BindExternalFunction("startMiniGame", () => {
            MiniGameContext testContext = new MiniGameContext(TypesMiniGames.BreakingLock, 0f, 5);
            GameObject.Find("GameSystems").GetComponent<MiniGamesManager>().RunMiniGame(testContext);
        });

        // Проверка на наличие предмета в инвентаре
        currentStory.BindExternalFunction("itemInInventory", (string item) =>{
            craftManager = FindAnyObjectByType<CraftingManager>();
            bool inInventory = craftManager.IsExistInInventory(item);
            return inInventory;
        });

        // Смена выполнение задания
        currentStory.BindExternalFunction("setDoneTask", (string taskId) => {
            ServiceLocator.Get<TaskObserver>().SetDoneTaskById(taskId);
        });

        // Смена выполнение подзадания
        currentStory.BindExternalFunction("setDoneSubTask", (string taskId, string subTaskId) => {
            ServiceLocator.Get<TaskObserver>().SetDoneSubTaskByIds(taskId, subTaskId);
        });

        // Уменьшение здоровья
        currentStory.BindExternalFunction("hitHealth", (int value) => {
            ServiceLocator.Get<PlayerStats>().hitHealth(value);
        });

        // Смена сцены
        currentStory.BindExternalFunction("changeScene", (string sceneName) => {
            StartCoroutine(ServiceLocator.Get<SceneControl>().LoadNewSceneAsync(sceneName));
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

    /*    private IEnumerator TypeText(string text)
        {
            //dialogueText.text = ""; 
            foreach (char letter in text)
            {
                dialogueText.text += letter; 
                yield return new WaitForSeconds(0.05f); // Задержка между буквами
            }

            //SetChoicesInteractable(true);
        }*/




    // Продолжение истории
    /*    private void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                //SetChoicesInteractable(false);
                // Обновление истории диалога
                //dialogueText.text += "\n" + currentStory.Continue();

                string nextLine =  currentStory.Continue();
                StartCoroutine(TypeText(nextLine));




                // Включение кнопок выбора и получения вариантов выбора
                DisplayChoices();
            }
            else
            {
                SystemManager.GetInstance().UnfreezePlayer();
                StartCoroutine(ExitDialogMode());
            }
        }*/

    private void SetChoicesInteractable(bool interactable)
    {
        foreach (GameObject choice in choices)
        {
            choice.GetComponent<Button>().interactable = interactable;
        }
    }


    //создаем очередь для диалогов
    private Queue<string> dialogueQueue = new Queue<string>();
    //создаем флаг для проверки вывода в данный момент
    private bool isTyping = false;

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            
            string nextLine = currentStory.Continue();

            
            string[] lines = nextLine.Split('\n');
            foreach (string line in lines)
            {
                Debug.Log($"Добавлено в очередь: {line}");
                dialogueQueue.Enqueue(line.Trim()); 
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
        isTyping = true;
        SetChoicesInteractable(false);

        dialogueText.text += "\n"; 
        int startLength = dialogueText.text.Length;

        

        foreach (char letter in newLine)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); 
        }

        isTyping = false;
        SetChoicesInteractable(true);

        if (dialogueQueue.Count > 0)
        {
            yield return new WaitForSeconds(0.5f); 
            StartCoroutine(TypeText(dialogueQueue.Dequeue()));
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
            choicesText[index].text = $"{index + 1}. {choice.text}"; // choice.text
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

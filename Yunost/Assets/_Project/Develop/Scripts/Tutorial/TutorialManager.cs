using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; 
    public TMP_Text tutorialText;
    private UniversalTutorialManager universalTutorialManager;

    private bool isTutorialActive = false;

    private void Update()
    {
        if (isTutorialActive && Input.GetKeyDown(KeyCode.Return))
        {
            EndTutorial();

            if(!DialogManager.GetInstance().dialogIsPlaying)
            GameObject.Find("GameSystems").GetComponent<SystemManager>().UnfreezePlayer();
        }
    }

    public void StartTutorial(string message)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = message;
        Time.timeScale = 0; 
        isTutorialActive = true;
    }

    public void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1; 
        isTutorialActive = false;
    }

    private void Start()
    {
        universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();
        universalTutorialManager.AddMessage("Inventory", "Здесь вы можете хранить вещи и крафтить особые предметы. Кликнув на них, они переходят в области крафта");
        Debug.Log("Добавили текст к окну Inventory");
        universalTutorialManager.AddMessage("MiniGame1", "Чтобы пройти мини-игру, необходимо, используя МЫШЬ, повернуть отмычку в желаемое положение, затем нажать и удерживать кнопку D.");
        Debug.Log("Добавили текст к окну MiniGames");
        universalTutorialManager.AddMessage("StartDialog", "Здесь вы можете выбирать варианты диалога нажав на них ПКМ или нажав на клавиатуре 1, 2, 3, 4 ");
    }
}

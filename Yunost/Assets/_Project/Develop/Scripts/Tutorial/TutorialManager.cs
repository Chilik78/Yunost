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
        universalTutorialManager.AddMessage("Inventory", "����� �� ������ ������� ���� � �������� ������ ��������. ������� �� ���, ��� ��������� � ������� ������");
        Debug.Log("�������� ����� � ���� Inventory");
        universalTutorialManager.AddMessage("MiniGame1", "����� ������ ����-����, ����������, ��������� ����, ��������� ������� � �������� ���������, ����� ������ � ���������� ������ D.");
        Debug.Log("�������� ����� � ���� MiniGames");
        universalTutorialManager.AddMessage("StartDialog", "����� �� ������ �������� �������� ������� ����� �� ��� ��� ��� ����� �� ���������� 1, 2, 3, 4 ");
    }
}

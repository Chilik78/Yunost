using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MessageBoxManager : MonoBehaviour
{
    public GameObject questPanel; 
    public TMP_Text questText; 
    public float displayTime = 6f; // Время отображения 
    public float fadeDuration = 2f; // Длительность исчезновения

    private string currentQuest = "";
    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    void Start()
    {
        canvasGroup = questPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = questPanel.AddComponent<CanvasGroup>();
        }

        questPanel.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    // Метод для обновления задания
    public void UpdateQuest(string newQuest)
    {
        currentQuest = newQuest;
        questText.text = "Задание: " + currentQuest;

        // Показать панель
        ShowQuestPanel();

        // Запуск таймера для скрытия
        StartCoroutine(HideAfterDelay());
    }

    private void ShowQuestPanel()
    {

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        questPanel.SetActive(true);
        fadeCoroutine = StartCoroutine(FadePanel(0f, 1f)); 
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        fadeCoroutine = StartCoroutine(FadePanel(1f, 0f)); 

        yield return new WaitForSeconds(fadeDuration);
        questPanel.SetActive(false);
    }

    private IEnumerator FadePanel(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    // Для тестирования(можно вызвать из другого скрипта) 
    public void TestNewQuest(string testQuest)
    {
        UpdateQuest(testQuest);
    }


    public float testInterval = 5f; // Интервал между тестовыми сообщениями
    private float testTimer = 0f;

    void Update()
    {
/*        testTimer += Time.deltaTime;
        if (testTimer >= testInterval)
        {
            testTimer = 0f;
            TestNewQuest("Тестовое задание " + Random.Range(1, 100));
        }*/


        if (Input.GetKeyDown(KeyCode.Z))
        {
            TestNewQuest("Поговорить с Олегом о том, как выбить дверь!");
        }
    }



}
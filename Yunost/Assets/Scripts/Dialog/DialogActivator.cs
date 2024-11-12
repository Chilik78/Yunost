using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogActivator : MonoBehaviour
{
    public GameObject NPC;
    public GameObject DialogWindow;
    public GameObject DialogContainer;
    public GameObject DialogHistory;
    public GameObject ExitButton;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public Button button4;


    public TMP_Text btnText1;
    public TMP_Text btnText2;
    public TMP_Text btnText3;

    public TMP_Text historyText;
    bool dialogIsEnd = false;

    void Start()
    {

        DialogWindow = GameObject.Find("DialogWindow");
        DialogContainer = GameObject.Find("DialogContainer");
        DialogHistory = GameObject.Find("DialogHistory");
        ExitButton = GameObject.Find("ExitButton");

        historyText = DialogHistory.GetComponentInChildren<TMP_Text>(true);
        historyText.text = "Олег: Вот мы и у твоего дома! Но...видимо я обронил ключ...";

        // Добавление кнопки №1
        button1 = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/AnswerButton.prefab", typeof(Object))) as GameObject;
        button1.transform.SetParent(DialogContainer.transform);
        btnText1 = button1.GetComponentInChildren<TMP_Text>(true);
        btnText1.text = "Ты потерял ключ? Ну чтож, придётся подумать как попасть в дом...";
        button1.transform.localScale = new Vector3(0.2325581f, 0.1881533f, 1);
        button1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        button1.transform.localPosition = new Vector3(1.627869f, -25, 0);
        
        // Добавление кнопки №2
        button2 = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/AnswerButton.prefab", typeof(Object))) as GameObject;
        button2.transform.SetParent(DialogContainer.transform);
        btnText2 = button2.GetComponentInChildren<TMP_Text>(true);
        btnText2.text = "Как ты умудрился потерять ключ?! Ничего тебе доверить нельзя!";
        button2.transform.localScale = new Vector3(0.2325581f, 0.1881533f, 1);
        button2.transform.localRotation = Quaternion.Euler(Vector3.zero);
        button2.transform.localPosition = new Vector3(1.627869f, -40, 0);

        // Добавление кнопки №3
        /*button3 = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/AnswerButton.prefab", typeof(Object))) as GameObject;
        button3.transform.SetParent(DialogContainer.transform);
        btnText3 = button3.GetComponentInChildren<TMP_Text>(true);
        btnText3.text = "Давай я поищу ключ (- Время)";
        button3.transform.localScale = new Vector3(0.2325581f, 0.1881533f, 1);
        button3.transform.localRotation = Quaternion.Euler(Vector3.zero);
        button3.transform.localPosition = new Vector3(1.627869f, -45, 0);
        button3.SetActive(false);*/

        TMP_Text txtbut1 = button1.GetComponentInChildren<TMP_Text>(true);
        TMP_Text txtbut2 = button2.GetComponentInChildren<TMP_Text>(true);
        //TMP_Text txtbut3 = button3.GetComponentInChildren<TMP_Text>(true);

        button1.GetComponent<Button>().onClick.AddListener(() => dialogManager_btn1(txtbut1));
        button2.GetComponent<Button>().onClick.AddListener(() => dialogManager_btn2(txtbut2));
        //button3.GetComponent<Button>().onClick.AddListener(() => dialogManager_btn2(txtbut3));

        DialogWindow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        NPC = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        NPC = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && NPC != null && dialogIsEnd == false)
        {
            DialogWindow.SetActive(true);
            ExitButton.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && NPC != null && dialogIsEnd == true)
        {
            DialogWindow.SetActive(true);
            historyText.text = "Олег: Пошли уже разбираться с проникновением в дом!";
            ExitButton.SetActive(true);
            
        }

    }


    public void dialogManager_btn1(TMP_Text txtbut1)
    {

        if (txtbut1.text == "Ты потерял ключ? Ну чтож, придётся подумать как попасть в дом...")
        {
            historyText.text += "\nВы: " + txtbut1.text;
            historyText.text += "\n" + "Олег: Мы можем поискать ключ или ты можешь перелезть через окно. А на самый крайний случай - выломаем дверь!";
            btnText1.text = "Поможешь выломать дверь? (Лояльность)";
            btnText2.text = "Я попробую перелезть через окно (- Выносливость)";

        }
        else if (txtbut1.text == "Поможешь выломать дверь? (Лояльность)")
        {

            historyText.text += "\nВы: " + txtbut1.text;
            historyText.text += "\n" + "Олег: Да, конечно! Пойдём!";
            btnText1.text = "";
            btnText2.text = "";
            dialogIsEnd = true;
            ExitButton.SetActive(true);
        }

    }

    public void dialogManager_btn2(TMP_Text txtbut2)
    {
        if (txtbut2.text == "Как ты умудрился потерять ключ?! Ничего тебе доверить нельзя!")
        {
            historyText.text += "\nВы: " + txtbut2.text;
            historyText.text += "\n" + "Олег: Раз уж ты так заговорил, то помогать тебе я не стану. Ищи ключ сам, либо прыгай через окно в дом.";
            btnText1.text = "Поможешь выломать дверь? (Лояльность)";
            button1.GetComponent<Button>().enabled = false;
            btnText1.color = Color.black;
            btnText2.text = "Я попробую перелезть через окно (- Выносливость)";
        }
        else if(txtbut2.text == "Я попробую перелезть через окно (- Выносливость)")
        {
            historyText.text += "\nВы: " + txtbut2.text;
            historyText.text += "\n" + "Олег: Удачи. Я посмотрю со стороны.";
            btnText1.text = "";
            btnText2.text = "";
            dialogIsEnd = true;
            ExitButton.SetActive(true);
        }

    }

    /*public void dialogManager_btn3(TMP_Text txtbut3)
    {
        if (txtbut3.text == "А в чём проблема? Ключ от двери есть.")
        {

        }
    }*/

    public void ExitDialog()
    {
        DialogWindow.SetActive(false);
    }
}

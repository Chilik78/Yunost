using System.IO;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    /*    // Знак над NPC
        [Header("Visual Cue")]
        [SerializeField] private GameObject visualCue;*/

    private UniversalTutorialManager universalTutorialManager;

    [SerializeField]
    private float yStepVisualClue = 1;

    [SerializeField]
    private string targetName;

    private GameObject target;

    // Ink JSON файл с диалогами данного NPC
    [Header("Ink JSON")]
    [SerializeField] private string jsonPath;

    // Игрок в области NPC
    private bool playerInRange;


    private void Start()
    {
        universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();

        if(targetName != null)
        {
            target = GameObject.Find(targetName);
        }
        else
        {
            target = transform.GetChild(0).gameObject;
        }

        Debug.Log(target.name);
    }

    private void Awake()
    {
        playerInRange = false;
       /* var prefab = Resources.Load("VisualCue");
        _visualCue = Instantiate(prefab, this.transform) as GameObject;*/
    }

    private void Update()
    {
        // Отображение знака над NPC
        if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
        {
            //SetClue(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                string json = "";
                using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + "InkJSON/" + jsonPath + ".json"))
                {
                    json = sr.ReadToEnd();
                }
                DialogManager.GetInstance().EnterDialogMode(json);
                SystemManager.GetInstance().FreezePlayer();
                universalTutorialManager.TriggerTutorial("StartDialog");

            }
        }
        else
        {
            //SetClue(false);
        }
    }

    private Color _prevColor;

    private void SetClue(bool state)
    {
        if (state)
        {
            _prevColor = target.GetComponentInChildren<Renderer>().material.GetColor("_Color");
            target.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            target.GetComponentInChildren<Renderer>().material.SetColor("_Color", _prevColor);
        }
    }

    // Игрок вошёл в область NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    // Игрок вышел из области NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

}

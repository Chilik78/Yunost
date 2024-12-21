using System.IO;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    /*    // Знак над NPC
        [Header("Visual Cue")]
        [SerializeField] private GameObject visualCue;*/

    private GameObject _visualCue;

    private UniversalTutorialManager universalTutorialManager;

    // Ink JSON файл с диалогами данного NPC
    [Header("Ink JSON")]
    [SerializeField] private string jsonPath;

    // Игрок в области NPC
    private bool playerInRange;


    private void Start()
    {
        universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();
    }

    private void Awake()
    {
        playerInRange = false;
        var prefab = Resources.Load("VisualCue");
        _visualCue = Instantiate(prefab, this.transform) as GameObject;
    }

    private void Update()
    {
        // Отображение знака над NPC
        if (playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
        {
            _visualCue.SetActive(true);
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
            _visualCue.SetActive(false);
        }
    }

    // Игрок вошёл в область NPC
    private void OnTriggerEnter(Collider other)
    {
        var npcTransform = this.GetComponentInParent<Transform>();
        var newPos = new Vector3(npcTransform.position.x, npcTransform.position.y + 2f, npcTransform.position.z);
        _visualCue.transform.position = newPos;

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

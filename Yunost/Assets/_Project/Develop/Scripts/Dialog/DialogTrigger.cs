using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    // Знак над NPC
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // Ink JSON файл с диалогами данного NPC
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // Игрок в области NPC
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        // Отображение знака над NPC
        if(playerInRange && !DialogManager.GetInstance().dialogIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogManager.GetInstance().EnterDialogMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
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

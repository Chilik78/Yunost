using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    // ���� ��� NPC
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // Ink JSON ���� � ��������� ������� NPC
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // ����� � ������� NPC
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        // ����������� ����� ��� NPC
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

    // ����� ����� � ������� NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    // ����� ����� �� ������� NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

}

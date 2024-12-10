using Global;
using MiniGames;
using ProgressModul;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DialogTrigger : MonoBehaviour
{
    /*    // Знак над NPC
        [Header("Visual Cue")]
        [SerializeField] private GameObject visualCue;*/

    private GameObject _visualCue;

    // Ink JSON файл с диалогами данного NPC
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // Игрок в области NPC
    private bool playerInRange;

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
                DialogManager.GetInstance().EnterDialogMode(inkJSON);
                SystemManager.GetInstance().FreezePlayer();
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

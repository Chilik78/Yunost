using Global;
using Ink.Runtime;
using System.IO;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    /*    // Знак над NPC
        [Header("Visual Cue")]
        [SerializeField] private GameObject visualCue;*/

    private UniversalTutorialManager universalTutorialManager;

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

        if (!string.IsNullOrEmpty(targetName) && targetName != "self")
        {
            target = GameObject.Find(targetName);
        }
        else if (targetName == "self")
        {
            target = this.gameObject;
        }
        else
        {
            target = transform.GetChild(0).gameObject;
        }

        _visualClue = (GameObject) ServiceLocator.Get<UnityEngine.Object>();
        _prevMaterials = GetMaterials(target);
        _cueMaterial = Resources.Load<Material>("CueMaterial");
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
    }

    private Material[] GetMaterials(GameObject target)
    {
        var renderer = target.GetComponentInChildren<Renderer>();
        return renderer.materials;
    }

    private void SetMaterials(GameObject target, Material[] materials)
    {
        var renderer = target.GetComponentInChildren<Renderer>();
        var materials_r = renderer.materials;
        for (int i = 0; i < materials_r.Length; i++)
        {
            materials_r[i] = materials[i];
        }
        renderer.materials = materials_r;
    }

    private void SetMaterial(GameObject target, Material material)
    {
        var renderer = target.GetComponentInChildren<Renderer>();
        var materials_r = renderer.materials;
        for (int i = 0; i < materials_r.Length; i++)
        {
            materials_r[i] = material;
        }
        renderer.materials = materials_r;
    }

    private Material[] _prevMaterials;
    private GameObject _visualClue;
    private Material _cueMaterial;

    private void SetCueNPC(bool state)
    {
         _visualClue.SetActive(state);
    }

    private void SetCueIteract(bool state)
    {
        if (state)
        {
            _prevMaterials = GetMaterials(target);
            SetMaterial(target, _cueMaterial);
        }
        else
        {
            SetMaterials(target, _prevMaterials);
        }
    }

    private void SetCue(bool state)
    {
        if (target.tag == "NPC")
        {
            SetCueNPC(state);
        }
        else if(target.tag == "Iteract")
        {
            SetCueIteract(state);
        }
    }

    [SerializeField]
    private float _heightY = 7;

    private void SetCoordToCue()
    {
        _visualClue.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + _heightY, target.transform.position.z);
        _visualClue.SetActive(false);
    }

    // Игрок вошёл в область NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            SetCoordToCue();
            SetCue(true);
        }
    }

    // Игрок вышел из области NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            SetCue(false);
            DialogManager.GetInstance().ClearText();
        }
    }

}

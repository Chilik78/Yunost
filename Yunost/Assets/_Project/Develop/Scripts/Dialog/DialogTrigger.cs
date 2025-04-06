using Global;
using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class DialogTrigger : MonoBehaviour
{
    private bool _playerInRange; // Игрок в области NPC
    private UniversalTutorialManager _universalTutorialManager;
    private BoxCollider _boxCollider;

    private GameObject _targetCue;
    private Material[] _prevMaterials;
    private GameObject _visualCue;
    private Material _cueMaterial;
    [Header("Смещение по Y для Cue"), SerializeField] private float _shiftByY = 7;

    [Header("Ink JSON"), SerializeField, Tooltip("Ink JSON файл с диалогами данного NPC")] private string _jsonPath;

    #region Init
    private void Start()
    {
        _playerInRange = false;
        _universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();
        InitBoxCollider();
        InitClue();
    }

    private void InitBoxCollider()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        if (_boxCollider == null)
            _boxCollider = gameObject.AddComponent<BoxCollider>();

        _boxCollider.isTrigger = true;
    }

    private void InitClue()
    {
        _targetCue = transform.gameObject;
        _visualCue = (GameObject)ServiceLocator.Get<UnityEngine.Object>();
        _cueMaterial = Resources.Load<Material>("CueMaterial");

        if (_targetCue.tag != "NPC")
            _prevMaterials = GetMaterials(_targetCue);
    }

    #endregion

    private void Update()
    {
        if (_playerInRange && !DialogManager.GetInstance().dialogIsPlaying && Input.GetKeyDown(KeyCode.E))
        {
            StartDialog();
        }
    }

    private void StartDialog()
    {
        string json = "";
        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + "InkJSON/" + _jsonPath + ".json"))
        {
            json = sr.ReadToEnd();
        }
        DialogManager.GetInstance().EnterDialogMode(json);
        SystemManager.GetInstance().FreezePlayer();
        _universalTutorialManager.TriggerTutorial("StartDialog");
    }

    // Игрок вошёл в область NPC
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.gameObject.tag == "NPC")
                SetCoordToCue();
            _playerInRange = true;
            SetCue(true);
        }
    }

    // Игрок вышел из области NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerInRange = false;
            SetCue(false);
            DialogManager.GetInstance().ClearText();
        }
    }

    #region Clue_Methods
    private Material[] GetMaterials(GameObject target)
    {
        var renderer = target.GetComponent<Renderer>();
        if(renderer == null)
        {
            renderer = target.GetComponentInChildren<Renderer>();
        }
        return renderer.materials;
    }

    private void SetMaterials(GameObject target, Material[] materials)
    {
        Renderer renderer = target.GetComponentInChildren<Renderer>();
        Material[] materials_r = renderer.materials;
        for (int i = 0; i < materials_r.Length; i++)
        {
            materials_r[i] = materials[i];
        }
        renderer.materials = materials_r;
    }

    private void SetMaterial(GameObject target, Material material)
    {
        Renderer renderer = target.GetComponentInChildren<Renderer>();
        Material[] materials_r = renderer.materials;
        for (int i = 0; i < materials_r.Length; i++)
        {
            materials_r[i] = material;
        }
        renderer.materials = materials_r;
    }

    private void SetCueNPC(bool state)
    {
        _visualCue.SetActive(state);
    }

    private void SetCueIteract(bool state)
    {
        if (state)
        {
            SetMaterial(_targetCue, _cueMaterial);
        }
        else
        {
            SetMaterials(_targetCue, _prevMaterials);
        }
    }

    private void SetCue(bool state)
    {
        if (_targetCue.tag == "NPC")
        {
            SetCueNPC(state);
        }
        else if (_targetCue.tag == "Iteract" || _targetCue.tag == "Item")
        {
            SetCueIteract(state);
        }
    }

    private void SetCoordToCue()
    {
        Vector3 _targetPos = _targetCue.transform.position;
        _visualCue.transform.position = new Vector3(_targetPos.x, _targetPos.y + _shiftByY, _targetPos.z);
        _visualCue.SetActive(false);
    }

    #endregion
}

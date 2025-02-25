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

    private GameObject _targetClue;
    private Material[] _prevMaterials;
    private GameObject _visualClue;
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
        _boxCollider = transform.gameObject.GetComponent<BoxCollider>();
        if (_boxCollider == null)
            _boxCollider = gameObject.AddComponent<BoxCollider>();

        _boxCollider.isTrigger = true;
    }

    private void InitClue()
    {
        _targetClue = transform.gameObject;
        _visualClue = (GameObject)ServiceLocator.Get<UnityEngine.Object>();
        _cueMaterial = Resources.Load<Material>("CueMaterial");

        if(_targetClue.tag != "NPC")
            _prevMaterials = GetMaterials(_targetClue);
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
            if(transform.gameObject.tag == "NPC")
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
         _visualClue.SetActive(state);
    }

    private void SetCueIteract(bool state)
    {
        if (state)
        {
            SetMaterial(_targetClue, _cueMaterial);
        }
        else
        {
            SetMaterials(_targetClue, _prevMaterials);
        }
    }

    private void SetCue(bool state)
    {
        if (_targetClue.tag == "NPC")
        {
            SetCueNPC(state);
        }
        else if(_targetClue.tag == "Iteract")
        {
            SetCueIteract(state);
        }
    }

    private void SetCoordToCue()
    {
        Vector3 _targetPos = _targetClue.transform.position;
        _visualClue.transform.position = new Vector3(_targetPos.x, _targetPos.y + _shiftByY, _targetPos.z);
        _visualClue.SetActive(false);
    }

    #endregion
}

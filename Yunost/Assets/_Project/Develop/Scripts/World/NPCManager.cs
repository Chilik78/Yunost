using Global;
using ProgressModul;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private Dictionary<string, NPC> _npcsData = new Dictionary<string, NPC>();
    private Dictionary<string, GameObject> _npcsObjects = new Dictionary<string, GameObject>();

    [SerializeField] private string[] _npcsNames = new string[5] {
        "Lisa", "Sofia", "Makar", "Oleg", "Director"
    };

    private static NPCManager _instance;

    public static NPCManager GetInstance() => _instance;

    private void Awake()
    {
        var slsystem = ServiceLocator.Get<SaveLoadSystem>();
        foreach(var name in _npcsNames)
        {
            _npcsData.Add(name, new NPC(name));
            _npcsObjects.Add(name, GameObject.Find(name));
            slsystem.AddToSaveLoad(_npcsData[name]);
        }
        _instance = this;
        Debug.LogWarning("NPCM");
    }

    void Start()
    {
        
    }

    public void TPNPC(string name, string mark)
    {
        MarkController.GetInstance().ObjectToMark(_npcsObjects[name].transform, mark);
    }

    public void AddLoyality(string name, int value)
    {
        _npcsData[name].Loyalty += value;
    }

    public void SetLoyality(string name, int value)
    {
        _npcsData[name].Loyalty = value;
    }
}

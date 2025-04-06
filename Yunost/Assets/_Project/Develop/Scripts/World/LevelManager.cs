using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

class NPCPlacemnet
{
    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("mark")]
    public string Mark { get; private set; }

    public NPCPlacemnet(string name, string mark)
    {
        Name = name;
        Mark = mark;
    }
}

class LevelPlacement
{
    [JsonProperty("act")]
    public string Act { get; private set; }
    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("npcPlacemnets")]
    public NPCPlacemnet[] NPCPlacemnets { get; private set; }

    public LevelPlacement(string act, string name, NPCPlacemnet[] npcPlacemnets)
    {
        Act = act;
        Name = name;
        NPCPlacemnets = npcPlacemnets;
    }
}

public class LevelManager : MonoBehaviour
{
    private Dictionary<string, LevelPlacement> _levelPlacements = new();

    private static LevelManager _instance;

    public static LevelManager GetInstance() => _instance;

    private void _initLevelPlacements()
    {
        var json = Resources.Load<TextAsset>("InitLevelPlacements").text;
        var placements = JsonConvert.DeserializeObject<List<LevelPlacement>>(json);
        foreach(var placement in placements)
        {
            _levelPlacements.Add(placement.Act + '-' + placement.Name, placement);
        }
    }

    private void Awake()
    {
        _initLevelPlacements();
        _instance = this;
    }

    public void ApplyPlacement(string act, string name)
    {
        var placement = _levelPlacements[act + '-' + name];
        foreach(var npcPlacement in placement.NPCPlacemnets)
        {
            NPCManager.GetInstance().TPNPC(npcPlacement.Name, npcPlacement.Mark);
        }
    }
}

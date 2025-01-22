using Global;
using ProgressModul;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    SceneControl sceneControl;
    private Dictionary<string, GameObject> _tileDictionary = new Dictionary<string, GameObject>();
    private MarkController _markController;
    private Transform _playerTransform;
    private SystemManager _systemManager;

    private void Start()
    {
        sceneControl = ServiceLocator.Get<SceneControl>();
        var names = sceneControl.GetTileNames;
        
        foreach ( var name in names)
        {
            var obj = GameObject.Find(name);
            if (obj == null)
            {
                Debug.LogError($"Объекта тайла {name} не существует");
                continue;
            }
            obj.SetActive(false);
            _tileDictionary.Add(name, obj);
        }

        sceneControl.TileChanged += changeTile;

        Debug.Log($"Запуск {sceneControl.CurrentTile}");
        _tileDictionary[sceneControl.CurrentTile].SetActive(true);

        var gameSystems = GameObject.Find("GameSystems");
        _systemManager = gameSystems.GetComponent<SystemManager>();

        if (sceneControl.CurrentTile == "HubHome")
        {
            _systemManager.SetMainCamera(false);
        }
        else
        {
            _systemManager.SetHubCamera(false);
        }

        _markController = gameSystems.GetComponent<MarkController>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void changeTile(string newTile, string prevTile)
    {
        _tileDictionary[newTile].SetActive(true);
        _tileDictionary[prevTile].SetActive(false);

        if (newTile == "HubHome")
        {
            _systemManager.SetHubCamera(true);
            _systemManager.SetMainCamera(false);
        }
        else
        {
            _systemManager.SetMainCamera(true);
            _systemManager.SetHubCamera(false);
        }

        _markController.ObjectToMark(_playerTransform, newTile);
        
        
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<SceneControl>().TileChanged -= changeTile;
    }
}

using UnityEngine;
using Global;
using ProgressModul;
using Player;
using System;

public class InitScript : MonoBehaviour
{
    void Awake()
    {
        var visualCue = Instantiate(Resources.Load("VisualCue"));
        ServiceLocator.Register(visualCue);

        GlobalInitScript.InitServices();

        if (ServiceLocator.Get<SceneControl>().IsNewGame)
        {
            ServiceLocator.Get<SaveLoadSystem>().LoadDefault();
        }
        else
        {
            ServiceLocator.Get<SaveLoadSystem>().LoadGame(SaveType.File);
        }

        //DontDestroyOnLoad(GameObject.Find("GameSystems"));
    }

    private void Start()
    {
        PickupItem[] objects = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
        ListOfItems listOfItems = ServiceLocator.Get<ListOfItems>();

        foreach (PickupItem obj in objects)
        {
            if (listOfItems.ItemExists(obj.item.name))
            {
                Destroy(obj.gameObject);
            }
        }

        PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().OnMove += savePositions;

        var gameSystems = GameObject.Find("GameSystems");
        var systemManager = gameSystems.GetComponent<SystemManager>();

        systemManager.SetHubCamera(false);
        gameSystems.GetComponent<MarkController>().ObjectToMark(player.transform, "start_game");


        if (ServiceLocator.Get<SceneControl>().IsNewGame) return;

        player.transform.position = new Vector3(playerStats.X, player.transform.position.y, playerStats.Z);
        player.transform.rotation = Quaternion.Euler(0, playerStats.RotY, 0);
    }

    private void savePositions(Vector3 position, Quaternion rotation)
    {
        PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
        playerStats.X = position.x;
        playerStats.Z = position.z;
        playerStats.RotY = rotation.eulerAngles.y;
    }

    private void OnDestroy()
    {
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Movement>().OnMove -= savePositions;
            Destroy(player);
        }
        /*ServiceLocator.Unregister<PlayerStats>();
        ServiceLocator.Unregister<TimeControl>();
        ServiceLocator.Unregister<TaskObserver>();
        ServiceLocator.Unregister<ListOfItems>();*/
    }
}

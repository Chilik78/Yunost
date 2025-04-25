using Global;
using Player;
using ProgressModul;
using System;
using UnityEngine;

public class InitSystem : MonoBehaviour
{
    [SerializeField] public InitConfig config;
    private InitConfig _copyConfig;
    private bool _isLoaded;

    static private InitSystem _instance;

    void Awake()
    {
        //TODO: Для дебага
        _isLoaded = false;//PlayerPrefs.GetInt("is_loaded") == 1;
        _copyConfig = Instantiate(config);
        if (_instance != null)
        {
            Debug.LogWarning("Íà ñöåíå áîëüøå îäíîãî äèàëîãà");
        }
        _instance = this;
        Debug.Log(_isLoaded);
        if(_isLoaded )
        {
            _initServicesByLoad();
            ServiceLocator.Get<SaveLoadSystem>().LoadGame(SaveType.File);
        }
        else
        {
            _initServicesFromConfig();
        }
        

        var visualCue = Instantiate(Resources.Load("VisualCue"));
        ServiceLocator.Register(visualCue);
        Debug.LogWarning("InitS");
    }

    public static InitSystem GetInstance() => _instance;

    public void UnregisterServices()
    {
        ServiceLocator.Unregister<TaskObserver>();
        ServiceLocator.Unregister<ListOfItems>();
        ServiceLocator.Unregister<PlayerStats>();
        ServiceLocator.Unregister<TimeControl>();
        ServiceLocator.Unregister<DialogVariables>();
        ServiceLocator.Unregister<GameTimeControl>();
        ServiceLocator.Unregister<UnityEngine.Object>();

        ServiceLocator.Get<SaveLoadSystem>().Clear();
    }

    private void _initServicesByLoad()
    {
        TaskObserver taskObserver = new();
        ListOfItems listOfItems = new();
        PlayerStats playerStats = new();
        GameTimeControl gameTimeControl = new();
        TimeControl timeControl = new();
        DialogVariables dialogVariables = new DialogVariables();

        taskObserver.TaskStateChanged += (Task task) => {
            gameTimeControl.MainTime += 1;
        };

        var saveLoadSystem = ServiceLocator.Get<SaveLoadSystem>();

        saveLoadSystem.AddToSaveLoad(taskObserver);
        saveLoadSystem.AddToSaveLoad(listOfItems);
        saveLoadSystem.AddToSaveLoad(dialogVariables);
        saveLoadSystem.AddToSaveLoad(timeControl);
        saveLoadSystem.AddToSaveLoad(gameTimeControl);
        saveLoadSystem.AddToSaveLoad(playerStats);

        ServiceLocator.Register(taskObserver);
        ServiceLocator.Register(listOfItems);
        ServiceLocator.Register(playerStats);
        ServiceLocator.Register(gameTimeControl);
        ServiceLocator.Register(timeControl);
        ServiceLocator.Register(dialogVariables);
    }

    private void _initServicesFromConfig()
    {
        TaskObserver taskObserver = new(Resources.Load<TextAsset>("InitTasks").text);
        ListOfItems listOfItems = new();
        PlayerStats playerStats = new(config.HP, config.Stamina);
        GameTimeControl gameTimeControl = new(config.MainIGT, config.SideIGT);
        TimeControl timeControl = new(config.Hours, config.Minutes);
        DialogVariables dialogVariables = new DialogVariables();

        taskObserver.TaskStateChanged += (Task task) => {
            gameTimeControl.MainTime += 1;
        };

        var saveLoadSystem = ServiceLocator.Get<SaveLoadSystem>();
        saveLoadSystem.AddToSaveLoad(taskObserver);
        saveLoadSystem.AddToSaveLoad(listOfItems);
        saveLoadSystem.AddToSaveLoad(dialogVariables);
        saveLoadSystem.AddToSaveLoad(timeControl);
        saveLoadSystem.AddToSaveLoad(gameTimeControl);
        saveLoadSystem.AddToSaveLoad(playerStats);

        ServiceLocator.Register(taskObserver);
        ServiceLocator.Register(listOfItems);
        ServiceLocator.Register(playerStats);
        ServiceLocator.Register(gameTimeControl);
        ServiceLocator.Register(timeControl);
        ServiceLocator.Register(dialogVariables);
    }

    void Start()
    {
        var player = SystemManager.GetInstance().Player;
        PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
        player.GetComponent<Movement>().OnMove += _savePositions;

        if (_isLoaded)
        {
            player.transform.position = new Vector3(playerStats.X, 0, playerStats.Z);
            player.transform.rotation = Quaternion.Euler(new Vector3(0, playerStats.RotY, 0));
        }
        else
        {
            MarkController.GetInstance().ObjectToMark(player.transform, config.PositionMark);
        }
       
        GameObject gameSystems = GameObject.Find("GameSystems");
        var barControllers = gameSystems.GetComponents<BarController>();
        foreach (var barController in barControllers)
        {
            barController.Init();
        }

        PickupItem[] objects = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
        ListOfItems listOfItems = ServiceLocator.Get<ListOfItems>();

        foreach (PickupItem obj in objects)
        {
            if (listOfItems.ItemExists(obj.item.name))
            {
                Destroy(obj.gameObject);
            }
        }

        ServiceLocator.Get<SaveLoadSystem>().LoadDefault();
        _listenParamsToConfig();
    }

    private void OnDestroy()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Movement>().OnMove -= _savePositions;
            Destroy(player);
        }
        config.HP = _copyConfig.HP;
        config.LevelPositions = _copyConfig.LevelPositions;
        config.SideIGT = _copyConfig.SideIGT;
        config.Stamina = _copyConfig.Stamina;
        config.MainIGT = _copyConfig.MainIGT;
        config.Hours = _copyConfig.Hours;
        config.Minutes = _copyConfig.Minutes;
        config.NPCSLoyality = _copyConfig.NPCSLoyality;

        UnregisterServices();
    }

    private void _savePositions(Vector3 position, Quaternion rotation)
    {
        PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
        playerStats.X = position.x;
        playerStats.Z = position.z;
        playerStats.RotY = rotation.eulerAngles.y;
    }

    private void _listenParamsToConfig()
    {
        PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
        playerStats.HealthChanged += () => config.HP = playerStats.Health;
        playerStats.StaminaChanged += () => config.Stamina = playerStats.Stamina;

        GameTimeControl gameTimeControl = ServiceLocator.Get<GameTimeControl>();
        gameTimeControl.MainTimeChanged += (int time) => config.MainIGT = time;
        gameTimeControl.SideTimeChanged += (int time) => config.SideIGT = time;

        TimeControl timeControl = ServiceLocator.Get<TimeControl>();
        timeControl.TimeChanged += (int time, int h, int m) =>
        {
            config.Hours = h;
            config.Minutes = m;
        };
    }

    private void OnValidate()
    {
        try
        {
            PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
            playerStats.Health = config.HP;
            playerStats.Stamina = config.Stamina;

            GameTimeControl gameTimeControl = ServiceLocator.Get<GameTimeControl>();
            gameTimeControl.MainTime = config.MainIGT;
            gameTimeControl.SideTime = config.SideIGT;

            TimeControl timeControl = ServiceLocator.Get<TimeControl>();
            timeControl.SetTimeFormat(config.Hours, config.Minutes);

            var player = SystemManager.GetInstance().Player;
            MarkController.GetInstance().ObjectToMark(player.transform, config.PositionMark);

            LevelManager.GetInstance().ApplyPlacement(config.LevelPositions.act, config.LevelPositions.name);

            foreach(var npcData in config.NPCSLoyality)
            {
                NPCManager.GetInstance().SetLoyality(npcData.name, npcData.loyalty);
            }
        }
        catch (Exception)
        {
            Debug.Log("Происходит ввод параметров");
        }
    }
}   


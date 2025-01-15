using UnityEngine;
using Global;
using ProgressModul;
using Player;

public class InitScript : MonoBehaviour
{
    static public string isInitedKey => "isInited";
    void Awake()
    {
        if (PlayerPrefs.HasKey(isInitedKey) && PlayerPrefs.GetInt(isInitedKey) == 1) return;

        
        PlayerPrefs.SetInt(isInitedKey, 1);

        //DontDestroyOnLoad(GameObject.Find("GameSystems"));
    }

    private void Start()
    {
        PickupItem[] objects = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
        ListOfItems listOfItems = ServiceLocator.Get<ListOfItems>();
        foreach (PickupItem obj in objects)
        {
            if (listOfItems.AutoInventoryItems.Contains(obj.item))
            {
                Destroy(obj.gameObject);
            }

        }

        PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().OnMove += savePositions;

        if (PlayerPrefs.GetInt("IsFirstInScene") == 1)
        {
            PlayerPrefs.SetInt("IsFirstInScene", 0);
            return;
        }

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

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(isInitedKey, 0);
    }
}

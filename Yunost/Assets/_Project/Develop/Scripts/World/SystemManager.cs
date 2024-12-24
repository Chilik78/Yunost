using MiniGames;
using Player;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private static SystemManager _instance;
    private GameObject _gameSystems, _player, _mainCamera, _canvases;
    public MiniGamesManager MiniGamesManager {get ; set; }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("На сцене больше одного диалога");
        }
        _instance = this;
    }

    public static SystemManager GetInstance() => _instance;

    void Start()
    {
        _gameSystems = this.gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _mainCamera = GameObject.Find("Canvases");
        _canvases = GameObject.Find("Main Camera");
        MiniGamesManager = _gameSystems.GetComponent<MiniGamesManager>();
    }

    public void UnfreezePlayer()
    {
        if (_player == null) return;
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _player.GetComponent<Movement>().SetFreezed(false);
    }

    public void FreezePlayer()
    {
        if (_player == null) return;
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _player.GetComponent<Movement>().SetFreezed(true);
    }

    public void DisableCanvases()
    {
        if (_canvases == null) return;
        _canvases.SetActive(false);
    }

    public void EnableCanvases()
    {
        if (_canvases == null) return;
        _canvases.SetActive(true);
    }

    public void DisableMainCamera()
    {
        if (_mainCamera == null) return;
        _mainCamera.SetActive(false);
    }

    public void EnableMainCamera()
    {
        if (_mainCamera == null) return;
        _mainCamera.SetActive(true);
    }

    public void DisableSystemsToMiniGame()
    {
        FreezePlayer();
        DisableCanvases();
        DisableMainCamera();
    }

    public void EnableSystemsToMiniGame()
    {
        UnfreezePlayer();
        EnableCanvases();
        EnableMainCamera();
    }
}

using MiniGames;
using Player;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private static SystemManager _instance;
    private GameObject _gameSystems, _player, _mainCamera, _canvases, _sun, _hubCamera;
    public MiniGamesManager MiniGamesManager { get; set; }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Íà ñöåíå áîëüøå îäíîãî äèàëîãà");
        }
        _instance = this;
    }

    public static SystemManager GetInstance() => _instance;

    void Start()
    {
        _gameSystems = this.gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _mainCamera = GameObject.Find("Main Camera");
        _hubCamera = GameObject.Find("Hub Camera");
        _canvases = GameObject.Find("Canvases");
        _sun = GameObject.Find("Sun");
        MiniGamesManager = _gameSystems.GetComponent<MiniGamesManager>();
    }

    public void UnfreezePlayer()
    {
        if (_player == null) return;
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _player.GetComponent<Movement>().SetFreezed(false);
    }

    public void FreezePlayer()
    {
        if (_player == null) return;
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _player.GetComponent<Movement>().SetFreezed(true);
    }

    public void SetCanvases(bool state)
    {
        if (_canvases == null) return;
        _canvases.SetActive(state);
    }

    public void SetMainCamera(bool state)
    {
        if (_mainCamera == null) return;
        _mainCamera.SetActive(state);
    }

    public void SetHubCamera(bool state)
    {
        if (_hubCamera == null) return;
        _hubCamera.SetActive(state);
    }

    public void SetSun(bool state)
    {
        if (_sun == null) return;
        _sun.SetActive(state);
    }

    public void SetSystemsToMiniGame(bool state)
    {
        SetCanvases(state);
        SetMainCamera(state);
        SetSun(state);
    }
}
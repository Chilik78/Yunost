using Player;
using UnityEditor;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private static SystemManager _instance;
    private GameObject _gameSystems, _player, _mainCamera, _canvases;


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
    }

    public void UnfreezePlayer()
    {
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _player.GetComponent<Movement>().SetFreezed(false);
    }

    public void FreezePlayer()
    {
        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _player.GetComponent<Movement>().SetFreezed(true);
    }

    public void DisableCanvases()
    {
        _canvases.SetActive(false);
    }

    public void EnableCanvases()
    {
        _canvases.SetActive(true);
    }

    public void DisableMainCamera()
    {
        _mainCamera.SetActive(false);
    }

    public void EnableMainCamera()
    {
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

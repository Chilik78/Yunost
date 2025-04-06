using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class CameraChanger : MonoBehaviour
{
    private SystemManager _systemManager;
    void Start()
    {
        _systemManager = GameObject.Find("GameSystems").GetComponent<SystemManager>();
        _systemManager.SetHubCamera(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _systemManager.SetHubCamera(true);
            _systemManager.SetMainCamera(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _systemManager.SetHubCamera(false);
            _systemManager.SetMainCamera(true);
        }
    }
}

using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float smoothTime = 0.7f;
    public float offset = 3f;
    public int lowerLimitZoom = 2;
    public int upperLimitZoom = 6;

    private Vector3 _vel;
    private GameObject _player;

    private float _scrollSpeed = 10;
    private Camera _camera;


    void Start()
    {
        _camera = Camera.main;
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        FollowPlayer();
        Zoom();
    }

    private void FollowPlayer()
    {
        Vector3 targetCoord = new Vector3(_player.transform.position.x - offset, transform.position.y, _player.transform.position.z - offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetCoord, ref _vel, smoothTime); // Плавно перемещает камеру в точку координату персонажа
    }

    private void Zoom()
    {
        if (_camera.orthographic)
        {
            float currentOrthographicSize = _camera.orthographicSize;
            float newOrthographicSize = currentOrthographicSize - Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;

            if (newOrthographicSize <= upperLimitZoom && newOrthographicSize >= lowerLimitZoom)
            {
                _camera.orthographicSize = newOrthographicSize;
            }

        }
    }
}

using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float smoothTime = 0.7f;
    public float offset = 3f;

    private Vector3 vel;
    private GameObject player;

    private float ScrollSpeed = 10;
    private Camera ZoomCamera;


    void Start()
    {
        ZoomCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 targetCoord = new Vector3(player.transform.position.x - offset, transform.position.y, player.transform.position.z - offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetCoord, ref vel, smoothTime); //плавно перемещает камеру в точку координату персонажа

        if (ZoomCamera.orthographic)
        {
            ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        }
        else
        {
            ZoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        }
    }
}

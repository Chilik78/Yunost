using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject mapCanvas;
    public float zoomSpeed = 60f;
    public float minZoom = 5f;
    public float maxZoom = 50f;
    public float XYSpeed = 20f;
    public float initialOrthographicSize = 500f;
    //public Vector3 initialCameraPosition = Vector3.zero; // 0 700 0
    //public Vector3 initialCameraRotation = Vector3.zero; // 90 180 90

    private bool isMapOpen = false;
    private float initialTimeScale;
    private Camera mapCamera;

    private Vector3 dragStartPosition; 
    private Vector3 cameraStartPosition;
    private bool isDragging = false;

    void Start()
    {
        if (mapCanvas == null)
        {
            Debug.LogError("Map Canvas is not assigned!");
            enabled = false;
            return;
        }
        mapCanvas.SetActive(false);


        mapCamera = mapCanvas.GetComponentInChildren<Camera>();

        if (mapCamera == null)
        {
            Debug.LogError("No camera found inside the Map Canvas!");
            enabled = false;
            return;
        }
/*        else
        {
            mapCamera.transform.position = initialCameraPosition;
            mapCamera.transform.rotation = Quaternion.Euler(initialCameraRotation);
            mapCamera.orthographicSize = initialOrthographicSize;
           Debug.LogWarning(@$"
                           Позитация - {mapCamera.transform.position}, 
                           Ротация - {mapCamera.transform.rotation.eulerAngles}, 
                           Размера - {mapCamera.orthographicSize}");
        }*/

        initialTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }

        if (isMapOpen)
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            ZoomMap(scrollDelta);

            if (Input.GetMouseButtonDown(1)) // 1 - правая кнопка мыши
            {
                isDragging = true;
                dragStartPosition = GetMouseWorldPosition();//Input.mousePosition
                cameraStartPosition = mapCamera.transform.position;
            }

            if (Input.GetMouseButtonUp(1))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector3 currentMousePosition = GetMouseWorldPosition();
                Vector3 dragOffset = currentMousePosition - dragStartPosition;//(Input.mousePosition - dragStartPosition) * XYSpeed; 

                // Инвертируем оси, чтобы перемещение соответствовало желаемому поведению
                Vector3 newPosition = cameraStartPosition - dragOffset * XYSpeed;//- new Vector3(dragOffset.x, 0, dragOffset.y);

                mapCamera.transform.position = newPosition;
            }



        }
    }

    void ToggleMap()
    {
        isMapOpen = !isMapOpen;
        mapCanvas.SetActive(isMapOpen);

        if (isMapOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = initialTimeScale;
        }
    }

    void ZoomMap(float scrollDelta)
    {
        float newSize = mapCamera.orthographicSize - scrollDelta * zoomSpeed;
        mapCamera.orthographicSize = newSize;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Плоскость XZ
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }


}

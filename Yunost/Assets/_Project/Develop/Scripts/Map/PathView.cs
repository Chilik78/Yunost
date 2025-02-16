using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
[RequireComponent(typeof(Seeker))]
public class PathDisplay : MonoBehaviour
{
    public Transform target; // Цель, к которой нужно проложить путь
    public LineRenderer largeMapLineRenderer; // LineRenderer для отображения на карте
    public LineRenderer miniMapLineRenderer; // LineRenderer для отображения на мини-карте
    public float heightOffset = 0.1f; // Смещение линии по высоте (чтобы не сливалась с землей)
    public LayerMask unwalkableLayer;

    private Seeker seeker; // Компонент Seeker для поиска пути
    private List<Vector3> pathPoints; // Список точек пути

    void Start()
    {
        seeker = GetComponent<Seeker>();

        // Проверка на назначенные объекты
        if (largeMapLineRenderer == null)
        {
            Debug.LogError("LargeMapLineRenderer is not assigned!");
        }
        if (miniMapLineRenderer == null)
        {
            Debug.LogError("MiniMapLineRenderer is not assigned!");
        }
    }

    void Update()
    {
        // Если есть цель, то ищем путь и отображаем его
        if (target != null)
        {
            FindPath();
        }

        // Если нет цели, то очищаем отображение пути
        else
        {
            ClearPathDisplay();
        }
    }

    //Метод очистки пути
    public void ClearPathDisplay()
    {
        // If we have a path
        if (pathPoints != null)
        {
            //Clear the points for the path
            pathPoints.Clear();

            //Clear the large and mini map
            DrawPath(pathPoints, largeMapLineRenderer, false);
            DrawPath(pathPoints, miniMapLineRenderer, false);
        }
    }

    void FindPath()
    {
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (p.error)
        {
            Debug.LogError("Pathfinding error: " + p.errorLog);
            ClearPathDisplay();
            return;
        }
        //Get a list of Vector3 points that are not colliding with any unwalkable areas
        // Vector3[] path = p.vectorPath.Where(pos => Physics.CheckSphere(pos,0.5f,unwalkableLayer)).ToArray();

        // Copy path points to a list
        pathPoints = p.vectorPath;

        // Draw the path on the large map and mini-map
        DrawPath(pathPoints, largeMapLineRenderer);
        DrawPath(pathPoints, miniMapLineRenderer);
    }

    void DrawPath(List<Vector3> path, LineRenderer lineRenderer, bool shouldDraw = true)
    {
        if (shouldDraw)
        {
            lineRenderer.positionCount = path.Count;
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 point = path[i];
                point.y += heightOffset; // Поднимаем линию над землей
                lineRenderer.SetPosition(i, point);
            }
        }
        else
        {
            lineRenderer.positionCount = 0;
        }

    }
    void DrawPath(List<Vector3> path, LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 point = path[i];
            point.y += heightOffset; // Поднимаем линию над землей
            //lineRenderer.SetPosition(i, transform.InverseTransformPoint(point));
            lineRenderer.SetPosition(i, point);

        }

    }
    //Call clear path when the map should not be visible
}
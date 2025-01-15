using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class CircleDrawer : MonoBehaviour
{
    [Range(0, 6000)]
    public int segments = 50;
    [Range(0, 100)]
    public float xRadius = 5;
    [Range(0, 100)]
    public float yRadius = 5;
    
    [Range(0, 100)]
    public float width = 0.5f;

    public Color color;

    [Header("Smoothness для материала круга"), Range(0, 1)]
    public float smoothnessMaterial = 0f;

    private LineRenderer _line;

    public void DrawCirle()
    {
        Init();
        Draw();
    }
    private void Init()
    {
        _line = gameObject.GetComponent<LineRenderer>();

        _line.positionCount = segments + 1;
        _line.useWorldSpace = false;
        _line.startWidth = width;
        _line.endWidth = width;

        _line.material.color = color;
        _line.material.SetFloat("_Smoothness", smoothnessMaterial);
    }

    private void Draw()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;

            _line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}
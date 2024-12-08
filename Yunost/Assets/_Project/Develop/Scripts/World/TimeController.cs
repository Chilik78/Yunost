using Global;
using ProgressModul;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{

    public Text _gameTime; // ����� ������
    public Transform directionalLight; // �������� �������� �����
    public float fullDay = 120f; // ������� ������� ����, � ��������
    [Range(0, 1)] public float currentTime; // ������� ����� �����

    private float h, m;
    private string hour, min;

    void Start()
    {
        ServiceLocator.Get<TimeControl>().TimeChanged += UpdateTime;
        UpdateTime(ServiceLocator.Get<TimeControl>().CurrentTime);
    }


    void UpdateTime(int time)
    {
        currentTime = time / 1000f;
        if (currentTime >= 1) currentTime = 0; else if (currentTime < 0) currentTime = 0;
        directionalLight.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
        TimeCount();
    }

    void TimeCount()
    {
        h = 24 * currentTime;
        m = 60 * (h - Mathf.Floor(h));

        if (m < 10) min = "0" + (int)m; else min = ((int)m).ToString();
        if (h < 10) hour = "0" + (int)h; else hour = ((int)h).ToString();

        //Debug.Log(hour + ":" + min);
    }
}
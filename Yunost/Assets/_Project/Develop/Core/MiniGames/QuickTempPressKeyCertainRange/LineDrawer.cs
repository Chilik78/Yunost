using UnityEngine;

namespace MiniGames
{
    namespace QuickTempPressKeyCertainRange
    {
        [RequireComponent(typeof(LineRenderer))]
        public class LineDrawer : MonoBehaviour
        {
            [Range(0, 100)]
            public float length = 5;

            [Range(0, 100)]
            public float width = 0.5f;

            public Color color;

            [Header("Smoothness для материала линии"), Range(0, 1)]
            public float smoothnessMaterial = 0f;

            LineRenderer _line;
            public void DrawLine()
            {
                Init();
                Draw();
            }

            private void Init()
            {
                _line = gameObject.GetComponent<LineRenderer>();

                _line.positionCount = 2;
                _line.useWorldSpace = false;
                _line.startWidth = width;
                _line.endWidth = width;

                _line.material.color = color;
                _line.material.SetFloat("_Smoothness", smoothnessMaterial);
            }

            private void Draw()
            {
                _line.SetPosition(0, new Vector3(0, 0, 0));
                _line.SetPosition(1, new Vector3(0, length, 0));
            }
        }

    }
}
using UnityEngine;

namespace MiniGames
{ 
    namespace ReachEndPointWithObstacles
    {
        public class BorderGenerator : MonoBehaviour
        {
            [SerializeField, Header("Префаб границ")]
            GameObject _borderPrefab;

            private GameObject _canvas;
            private Vector2 _sizeCanvas;

            public void GenerateBorder()
            {
                _canvas = GameObject.Find("Canvas MiniGame");
                _sizeCanvas = _canvas.GetComponent<RectTransform>().sizeDelta;
                Generate(_sizeCanvas);
            }

            private void Generate(Vector2 sizeCanvas)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject border = Instantiate(_borderPrefab, _canvas.transform);
                    ChangePosition(border, i, sizeCanvas);
                    ChangeSize(border, i, sizeCanvas);
                }
            }

            private void ChangePosition(GameObject border, int index, Vector2 sizeCanvas)
            {
                if(index % 2 == 0)
                {
                    float positionY = index == 0 ? sizeCanvas.y / 2 : (sizeCanvas.y / 2) * -1;
                    border.transform.localPosition = new Vector3(0, positionY, _borderPrefab.transform.localPosition.z);
                }
                else
                {
                    float positionX = index == 1 ? sizeCanvas.x / 2 : (sizeCanvas.x / 2) * -1;
                    border.transform.localPosition = new Vector3(positionX, 0, _borderPrefab.transform.localPosition.z);
                }
            }

            private void ChangeSize(GameObject border, int index, Vector2 sizeCanvas)
            {
                if (index % 2 == 0)
                {
                    border.transform.localScale = new Vector3(sizeCanvas.x, _borderPrefab.transform.localScale.y, _borderPrefab.transform.localScale.z);
                }
                else
                {
                    border.transform.localScale = new Vector3(_borderPrefab.transform.localScale.x, sizeCanvas.y, _borderPrefab.transform.localScale.z);
                }
            }
        }
    }
}
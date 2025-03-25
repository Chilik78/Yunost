using UnityEngine;

namespace MiniGames
{ 
    namespace HoldingObjectInRange
    { 
        public class FishingLine
        {
            private GameObject _startPoint;
            private GameObject _endPoint;
            private LineRenderer _lineRenderer;

            public FishingLine()
            {
                _startPoint = GameObject.Find("Start Point");
                _endPoint = GameObject.Find("End Point");
                _lineRenderer = GameObject.Find("Fishing rod with hook").GetComponent<LineRenderer>();
            }

            public void Build()
            {
                Vector3 v1 = _startPoint.transform.position;
                Vector3 v2 = _endPoint.transform.position;

                _lineRenderer.positionCount = 2;
                _lineRenderer.SetWidth(0.005f, 0.007f);
                _lineRenderer.SetPosition(0, v1);
                _lineRenderer.SetPosition(1, v2);
            }

            public void Destroy()
            {
                _lineRenderer.positionCount = 0;
            }

            public void Update()
            {
                _lineRenderer.SetPosition(0, _startPoint.transform.position);
                _lineRenderer.SetPosition(1, _endPoint.transform.position);
            }
        }
    }
}
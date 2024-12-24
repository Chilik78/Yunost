using UnityEngine;
using System.Collections.Generic;

namespace CameraModule
{
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
        private Dictionary<GameObject, Color> _hiddenGameObjects;


        void Start()
        {
            _camera = Camera.main;
            _player = GameObject.FindWithTag("Player");
            _hiddenGameObjects = new Dictionary<GameObject, Color>();
        }

        void Update()
        {
            FollowPlayer();
            //ShowPlayerBehindObj();
            Zoom();
        }

        private void OnTriggerEnter(Collider collider)
        {
            //ShowPlayerBehindObj(collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            //ShowPlayerBehindObj(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            //ShowObjects();
        }

        private void FollowPlayer()
        {
            Vector3 targetCoord = new Vector3(_player.transform.position.x - offset, transform.position.y, _player.transform.position.z - offset);
            transform.position = Vector3.SmoothDamp(transform.position, targetCoord, ref _vel, smoothTime); // Плавно перемещает камеру в точку координату персонажа  
        }

        private void ShowPlayerBehindObj()
        {
            Ray ray = new Ray(_camera.transform.position, _player.transform.position - _camera.transform.position + new Vector3(0, 1, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            GameObject hitGameObj = hit.transform.gameObject;

            if (hitGameObj.tag != "Player" && hitGameObj.tag != "NPC")
            {
                ShowPlayerBehindObj(hitGameObj.GetComponent<Collider>());
            }
            else if (_hiddenGameObjects.Count != 0)
            {
                ShowObjects();
            }
        }

        private void ShowPlayerBehindObj(Collider collider)
        {
            GameObject gameObj = collider.gameObject;
            SwitchToTransparent(gameObj);
        }

        private void ShowObjects()
        {
            foreach (var hiddenGameObject in _hiddenGameObjects)
            {
                SwitchToOpaque(hiddenGameObject.Key, hiddenGameObject.Value);
            }
            _hiddenGameObjects.Clear();
        }

        void SwitchToTransparent(GameObject gameObject)
        {
            if (!_hiddenGameObjects.ContainsKey(gameObject))
            {
                try
                {
                    Color oldColor = gameObject.GetComponent<Renderer>().material.color;
                    _hiddenGameObjects[gameObject] = oldColor;
                    gameObject.GetComponent<Renderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
                }
                catch
                {
                    Debug.LogWarning($"Не удалось сделать прозрачным {gameObject.name}");
                }
            }

        }

        void SwitchToOpaque(GameObject gameObject, Color oldColor)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a);
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
}
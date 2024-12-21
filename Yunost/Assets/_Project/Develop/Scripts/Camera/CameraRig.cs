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
        private List<GameObject> _hiddenGameObjects;


        void Start()
        {
            _camera = Camera.main;
            _player = GameObject.FindWithTag("Player");
            _hiddenGameObjects = new List<GameObject>();
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
            /*foreach (var gameObj in _hiddenGameObjects)
            {
                SwitchToOpaque(gameObj.GetComponent<MeshRenderer>().material);
            }
            _hiddenGameObjects.Clear();*/
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
                Material mat = hitGameObj.GetComponent<MeshRenderer>().material;
                SwitchToTransparent(mat);

                if(!_hiddenGameObjects.Contains(hitGameObj))
                {
                    _hiddenGameObjects.Add(hitGameObj);
                }
            }
            else if(_hiddenGameObjects.Count != 0)
            {
                foreach (var gameObj in _hiddenGameObjects)
                {
                    SwitchToOpaque(gameObj.GetComponent<MeshRenderer>().material);
                }
                _hiddenGameObjects.Clear();
            }
        }

        private void ShowPlayerBehindObj(Collider collider)
        {
            GameObject gameObj = collider.gameObject;
            Material mat = gameObj.GetComponent<MeshRenderer>().material;
            SwitchToTransparent(mat);
            if (!_hiddenGameObjects.Contains(gameObj))
            {
                _hiddenGameObjects.Add(gameObj);
            }
        }

        void SwitchToTransparent(Material mat)
        {
            mat.SetOverrideTag("RenderType", "Transparent");
            mat.SetFloat("_Mode", 3);
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            mat.SetFloat("_SrcBlend", 1);
            mat.SetFloat("_DstBlend", 10);
        }

        void SwitchToOpaque(Material mat)
        {
            mat.SetOverrideTag("RenderType", "");
            mat.SetFloat("_Mode", 0);
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = -1;
            mat.SetFloat("_SrcBlend", 1);
            mat.SetFloat("_DstBlend", 0);
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


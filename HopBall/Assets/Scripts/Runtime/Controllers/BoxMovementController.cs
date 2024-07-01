using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers
{
    public class BoxMovementController : MonoBehaviour
    {
        private class SelectedObjectData
        {
            public GameObject GameObject { get; set; }
            public Vector3 Offset { get; set; }
        }

        private Camera _mainCamera;
        private readonly Dictionary<int, SelectedObjectData> _selectedObjects = new Dictionary<int, SelectedObjectData>();
        private int _selectedObjectCount = 0;
        private bool _isGameStarted = false;
        
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameRestart -= OnGameRestart;
        }

        private void Update()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Ray ray = _mainCamera.ScreenPointToRay(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        HandleTouchBegan(touch, ray);
                        if (_selectedObjectCount == 2 && !_isGameStarted)
                        {
                            CoreGameSignals.Instance.OnGameStart?.Invoke();
                            _isGameStarted = true;
                        }
                        break;
                    case TouchPhase.Moved:
                        HandleTouchMoved(touch, ray);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        HandleTouchEnded(touch);
                        break;
                }
            }
        }

        private void HandleTouchBegan(Touch touch, Ray ray)
        {
            if (UnityEngine.Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Box"))
                {
                    var selectedObject = new SelectedObjectData
                    {
                        GameObject = hit.collider.gameObject,
                        Offset = hit.collider.gameObject.transform.position - hit.point,
                    };
                    _selectedObjects[touch.fingerId] = selectedObject;
                    _selectedObjectCount = _selectedObjects.Count;
                }
            }
        }

        private void HandleTouchMoved(Touch touch, Ray ray)
        {
            if (_selectedObjects.TryGetValue(touch.fingerId, out SelectedObjectData selectedObject))
            {
                Vector3 newPoint = ray.GetPoint(Vector3.Distance(_mainCamera.transform.position, selectedObject.GameObject.transform.position));
                Vector3 newPosition = new Vector3(selectedObject.GameObject.transform.position.x, newPoint.y + selectedObject.Offset.y, selectedObject.GameObject.transform.position.z);
                selectedObject.GameObject.transform.position = newPosition;
            }
        }
        private void HandleTouchEnded(Touch touch)
        {
            _selectedObjects.Remove(touch.fingerId);
            _selectedObjectCount = _selectedObjects.Count;
        }
        
        private void OnGameRestart()
        {
            _isGameStarted = false;
        }
    }
}


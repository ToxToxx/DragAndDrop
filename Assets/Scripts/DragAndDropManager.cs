using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField] private Collider _currentCollider;
    [SerializeField] private float _limitY = 0.5f; // pivot

    private Camera _mainCamera;
    private Plane _dragPlane;
    private bool _inputStart;
    private Vector3 _offset;
    
    private float _maxDistance = 2000f;
    private string _paramsLayerNames = "DragAndDropable";
    

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        //для мыши
#if UNITY_EDITOR

        if(Input.GetMouseButtonDown(0))
            SelectPart();

        if (Input.GetMouseButtonUp(0))
            Drop();
#endif

        //для тача

        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began )
                SelectPart();

            if(touch.phase == TouchPhase.Ended)
                Drop();
        }

        DragAndDropObject();
    }

    private void SelectPart()
    {
        RaycastHit hit;

        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(camRay, out hit, _maxDistance, LayerMask.GetMask(_paramsLayerNames)))
        {
             _currentCollider = hit.collider;
            _dragPlane = new Plane(_mainCamera.transform.forward, _currentCollider.transform.position);
            float planeDist;
            _dragPlane.Raycast(camRay, out planeDist);
            _offset = _currentCollider.transform.position - camRay.GetPoint(planeDist);
        }
    }

    private void DragAndDropObject()
    {
        if(_currentCollider == null) return;

        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);

        _currentCollider.transform.position = camRay.GetPoint(planeDist) + _offset;

        //Лимит на Y
        if(_currentCollider.transform.position.y < _limitY)
        {
            _currentCollider.transform.position =
                new Vector3(_currentCollider.transform.position.x,
                _limitY,
                _currentCollider.transform.position.z);
        }
    }

    private void Drop()
    {
        if ( _currentCollider == null) return;

        _currentCollider.transform.position = 
            new Vector3(_currentCollider.transform.position.x,
            _limitY,
            _currentCollider.transform.position.z);
        _currentCollider = null;
    }
}

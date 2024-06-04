using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField] private Collider _currentCollider;

    private Camera _mainCamera;
    private Plane _dragPlane;
    private bool _inputStart;
    private Vector3 _offset;
    private float _maxDistance = 2000f;
    private string paramsLayerNames = "Robot";

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        
    }

    private void SelectPart()
    {
        RaycastHit hit;

        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(camRay, out hit, _maxDistance, LayerMask.GetMask(paramsLayerNames)))
        {
             _currentCollider = hit.collider;
            _dragPlane = new Plane(_mainCamera.transform.forward, _currentCollider.transform.position);
            float planeDist;
            _dragPlane.Raycast(camRay, out planeDist);
            _offset = _currentCollider.transform.position - camRay.GetPoint(planeDist);
        }
    }
}

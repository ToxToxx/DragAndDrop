using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDragAndDrop : MonoBehaviour
{
    private Plane _dragPlane;
    private Vector3 _offset;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    //из курсора выходит луч и мы выделяем объект
    private void OnMouseDown()
    {
        _dragPlane = new Plane(_mainCamera.transform.forward, transform.position);
        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        _offset =  transform.position - camRay.GetPoint(planeDist); //объект не прыгает с оффсетом при нажатии на грань
    }

    //передвижение объекта
    private void OnMouseDrag()
    {
        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist) + _offset;
    }

    private void OnMouseUp()
    {
        Debug.Log("End drag");
    }
}

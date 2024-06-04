using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI : EventTrigger
{
    private bool _dragging;
    private Vector2 _offset;

    public void Update()
    {
        if (_dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _offset;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
        _offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
    }


}

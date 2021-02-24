using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    Vector2 _touchStart;
    [SerializeField] float _maxLength;
    public EventVector2 onDragStart;
    public EventVector2 onDrag;
    public EventVector2 onDragEnd;


    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            _touchStart = mousePosition;
            onDragStart.Invoke(Vector2.zero);

        }
        else if (Input.GetMouseButton(0))
        {
            float length = Mathf.Clamp(Vector2.Distance(mousePosition, _touchStart), 0, _maxLength);
            onDrag.Invoke((mousePosition - _touchStart).normalized * length.Remap(0, _maxLength, 0, 1));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onDragEnd.Invoke(Vector2.zero);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] RectTransform _title = null;
    Camera _camera = null;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (GameManager.instance.gameOn)
        {
            if (_title != null && _camera != null)
                _title.transform.forward = _camera.transform.forward;
        }

    }

}

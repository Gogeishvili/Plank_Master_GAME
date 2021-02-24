using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerManager : Singleton<CamerManager>
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Transform lookTarget;
    Vector3 _offset;
    void Start()
    {
        _offset = transform.position - lookTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, lookTarget.position + _offset, _moveSpeed * Time.deltaTime);
        }
    }
}

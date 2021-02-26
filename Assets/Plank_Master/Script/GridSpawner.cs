using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{

    [SerializeField] Transform _groundPref;

    [SerializeField] int _countInLine = 4;
    [SerializeField] int _lineCount = 3;
    [SerializeField] float _xScale = 0;
    [SerializeField] float _zScale = 0;
    [SerializeField] float _lineLengthRightLeft = 0;
    [SerializeField] float _lineLengthForwardBack = 0;
    [SerializeField] Vector3 _position = default;


    private void Awake()
    {
        _xScale = _groundPref.localScale.x;
        _zScale = _groundPref.localScale.z;
        _lineLengthRightLeft = (_countInLine * _xScale) - _xScale;
        _lineLengthForwardBack = (_lineCount * _zScale) - _zScale;

        _position = new Vector3(-_lineLengthRightLeft / 2, 0, _lineLengthForwardBack / 2);
    }

    private void Start()
    {
        Spawn();
    }

   
    public void Spawn()
    {

        for (int i = 0; i < _lineCount; i++)
        {
            for (int y = 0; y < _countInLine; y++)
            {
                if (y == 0)
                {
                    var _g = Instantiate(_groundPref);
                    _g.position = _position;
                    _position = _g.position;
                    _g.SetParent(transform);
                }
                else
                {
                    var _g = Instantiate(_groundPref);
                    _g.position = new Vector3(_position.x + _xScale, 0, _position.z);
                    _position = _g.position;
                    _g.SetParent(transform);

                }
            }
            _position = new Vector3(-_lineLengthRightLeft / 2, 0, _position.z - _zScale);

        }

        NavMeshManager.instance.BakenavMesh();

    
    }
}

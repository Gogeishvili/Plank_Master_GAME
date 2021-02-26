using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : Singleton<GridSpawner>
{

    [SerializeField] int _indexX = 0;
    [SerializeField] int _indexZ = 0;
    [SerializeField] Plank _groundPref;
    [SerializeField] int _countInLine = 4;
    [SerializeField] int _lineCount = 3;
    float _xScale = 0;
    float _zScale = 0;
    float _lineLengthRightLeft = 0;
    float _lineLengthForwardBack = 0;
    Vector3 _position = default;
    List<Transform> _allPlanks = new List<Transform>();

    private void Awake()
    {
        _xScale = _groundPref.transform.localScale.x;
        _zScale = _groundPref.transform.localScale.z;
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
                    _g.transform.position = _position;
                    _position = _g.transform.position;
                    _g.transform.SetParent(transform);
                    _allPlanks.Add(_g.transform);

                }
                else
                {
                    var _g = Instantiate(_groundPref);
                    _g.transform.position = new Vector3(_position.x + _xScale, 0, _position.z);
                    _position = _g.transform.position;
                    _g.transform.SetParent(transform);
                    _allPlanks.Add(_g.transform);
                }
            }
            _position = new Vector3(-_lineLengthRightLeft / 2, 0, _position.z - _zScale);
        }

        NavMeshManager.instance.BakenavMesh();

        foreach (var item in _allPlanks)
        {
            item.transform.localScale = new Vector3(0.8f, item.localScale.y, 0.8f);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : Singleton<GridSpawner>
{
    [SerializeField] int _marArea = 5;
    [SerializeField] Transform _plankOnGroundPrototype;
    [SerializeField] Plank[] _groundPref;
    [SerializeField] int _countInLine = 4;
    [SerializeField] int _lineCount = 3;
    [SerializeField] float _gap = 0.2f;

    float _xScale = 0;
    float _zScale = 0;
    float _lineLengthRightLeft = 0;
    float _lineLengthForwardBack = 0;
    Plank[,] _map;
    Vector3 _position = default;
    List<Transform> _allPlanks = new List<Transform>();

    public Texture2D map;
    public ColorToPrefab[] colorMaping;

    private void Awake()
    {
        _map = new Plank[_countInLine, _lineCount];

        if (GameManager.instance.randomPlankeVersion)
        {
            _xScale = _groundPref[0].transform.localScale.x;
            _zScale = _groundPref[0].transform.localScale.z;
            _lineLengthRightLeft = (_countInLine * _xScale) - _xScale;
            _lineLengthForwardBack = (_lineCount * _zScale) - _zScale;
            _position = new Vector3(-_lineLengthRightLeft / 2, 0, _lineLengthForwardBack / 2);
        }
        else
        {
            _xScale = _groundPref[1].transform.localScale.x;
            _zScale = _groundPref[1].transform.localScale.z;
            _lineLengthRightLeft = (_countInLine * _xScale) - _xScale;
            _lineLengthForwardBack = (_lineCount * _zScale) - _zScale;
            _position = new Vector3(-_lineLengthRightLeft / 2, 0, _lineLengthForwardBack / 2);
        }

    }

    private void Start()
    {
        if(GameManager.instance.randomPlankeVersion){
        Spawn();
        }
        else{
        GenerateLevel();
        }
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }

        NavMeshManager.instance.BakenavMesh();

    }
    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            Debug.Log(1);
            return;
        }
        foreach (ColorToPrefab _colorMapping in colorMaping)
        {
            if (_colorMapping.color.Equals(pixelColor))
            {
                Vector3 _position = new Vector3(x * _colorMapping.prefab.transform.localScale.x, 0, y * _colorMapping.prefab.transform.localScale.z);
                Instantiate(_colorMapping.prefab, _position, Quaternion.identity, transform);
            }
        }
    }

    public void Spawn()
    {

        for (int i = 0; i < _lineCount; i++)
        {
            for (int y = 0; y < _countInLine; y++)
            {
                if (y == 0)
                {
                    if (GameManager.instance.randomPlankeVersion)
                    {
                        var _g = Instantiate(_groundPref[0]);
                        _g.transform.position = _position;
                        _position = _g.transform.position;
                        _g.transform.SetParent(transform);
                        _allPlanks.Add(_g.transform);
                        _map[i, y] = _g;
                    }
                    else
                    {
                        var _g = Instantiate(_groundPref[1]);
                        _g.transform.position = _position;
                        _position = _g.transform.position;
                        _g.transform.SetParent(transform);
                        _allPlanks.Add(_g.transform);
                        _map[i, y] = _g;
                    }

                }
                else
                {
                    if (GameManager.instance.randomPlankeVersion)
                    {
                        var _g = Instantiate(_groundPref[0]);
                        _g.transform.position = new Vector3(_position.x + _xScale, 0, _position.z);
                        _position = _g.transform.position;
                        _g.transform.SetParent(transform);
                        _allPlanks.Add(_g.transform);
                        _map[i, y] = _g;
                    }
                    else
                    {
                        var _g = Instantiate(_groundPref[1]);
                        _g.transform.position = new Vector3(_position.x + _xScale, 0, _position.z);
                        _position = _g.transform.position;
                        _g.transform.SetParent(transform);
                        _allPlanks.Add(_g.transform);
                        _map[i, y] = _g;
                    }

                }

                if (GameManager.instance.randomPlankeVersion)
                {
                    SpawnPlankOnGroung(_position);
                }

            }

            _position = new Vector3(-_lineLengthRightLeft / 2, 0, _position.z - _zScale);
        }

        NavMeshManager.instance.BakenavMesh();

        if (!GameManager.instance.randomPlankeVersion)
        {
            MapArea();
        }

    }

    public void MapArea()
    {
        for (int i = 0; i < _marArea; i++)
        {
            for (int y = 0; y < _countInLine; y++)
            {
                _map[i, y].meshRenderer.enabled = false;
                _map[i, y].isActive = false;
                _map[i, y].gameObject.tag = MyStatics.CAN_NOT_TAKE;
            }
        }

        for (int i = _lineCount - _marArea; i < _lineCount; i++)
        {
            for (int y = 0; y < _countInLine; y++)
            {
                _map[i, y].meshRenderer.enabled = false;
                _map[i, y].isActive = false;
                _map[i, y].gameObject.tag = MyStatics.CAN_NOT_TAKE;
            }
        }

        for (int i = 0; i < _lineCount; i++)
        {
            for (int y = 0; y < _marArea; y++)
            {
                _map[i, y].meshRenderer.enabled = false;
                _map[i, y].isActive = false;
                _map[i, y].gameObject.tag = MyStatics.CAN_NOT_TAKE;
            }
        }

        for (int i = 0; i < _lineCount; i++)
        {
            for (int y = _countInLine - _marArea; y < _countInLine; y++)
            {
                _map[i, y].meshRenderer.enabled = false;
                _map[i, y].isActive = false;
                _map[i, y].gameObject.tag = MyStatics.CAN_NOT_TAKE;
            }
        }

    }

    void SpawnPlankOnGroung(Vector3 pos)
    {

        for (int i = 0; i < 2; i++)
        {
            var _fp = Instantiate(_plankOnGroundPrototype);
            _fp.position = new Vector3(pos.x + Random.Range(-1, 1), pos.y + Random.Range(0.2f, 0.6f), pos.z);
            _fp.eulerAngles = new Vector3(Random.Range(-4, 4), Random.Range(-90, 90), Random.Range(-4, 4));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenmanager : MonoBehaviour
{
    [SerializeField] List<Transform> _players = new List<Transform>();
    [SerializeField] Transform _indicator;
    List<Transform> _indicators = new List<Transform>();
    Triangle3D[] _triangle3D = new Triangle3D[2];
    Camera _camera;
    BoxCollider2D _boxCollider2D;

    void Awake()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            Color _color=_players[i].GetComponent<Character>().myPlankColor;
            var _indexsator = Instantiate(_indicator, transform);
            _indexsator.GetChild(0).gameObject.GetComponent<Image>().color=new Color(_color.r,_color.g,_color.b);
            _indicators.Add(_indexsator);
        }

        _triangle3D[0] = new Triangle3D(Vector3.zero, new Vector3(Screen.width, Screen.height, 0), new Vector3(Screen.width, 0, 0));
        _triangle3D[1] = new Triangle3D(Vector3.zero, new Vector3(0, Screen.height, 0), new Vector3(Screen.width, Screen.height, 0));

        _camera = Camera.main;

        _boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        _boxCollider2D.size = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void Update()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            EdgePoint(i);
        }
    }

    void EdgePoint(int index)
    {
        Vector3 _screenPoint = _camera.WorldToScreenPoint(_players[index].position);
        if (_triangle3D[0].Contains(_screenPoint) || _triangle3D[1].Contains(_screenPoint))
        {
            _indicators[index].gameObject.SetActive(false);
        }
        else
        {
            Vector3 _center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            Ray2D ray = new Ray2D(_screenPoint,_center- _screenPoint);
            RaycastHit2D _hit2D=Physics2D.Raycast(ray.origin, ray.direction, Vector3.Distance(_center, _screenPoint) - 10);
            
            if (_hit2D)
            {
                _indicators[index].gameObject.SetActive(true);
                _indicators[index].position=_hit2D.point;
                _indicators[index].forward=-ray.direction;
            }


        }

    }
}

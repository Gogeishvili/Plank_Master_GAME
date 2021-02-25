using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 1;
    [SerializeField] Animator _targetAnimator;
    [SerializeField] List<GameObject> _myPlanks = new List<GameObject>();
    [SerializeField] GameObject _plankPrototypePref;
    NavMeshAgent _thisAgent = null;
    Transform _planksParent;
    Vector3 _lastPlankLocalPosition;
    int _countPlank = 0;

    private void Awake()
    {
        _planksParent = transform.GetChild(1);
        _thisAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void Move(Vector2 direction)
    {
        float speed = _maxSpeed * direction.magnitude;
        //_targetAnimator.SetFloat("speed", direction.magnitude);
        Vector3 forward = new Vector3(direction.x, 0, direction.y);
        if (forward.sqrMagnitude > 0)
            transform.forward = forward;
        _thisAgent.Move(forward * Time.deltaTime * speed);
    }

    public virtual void Take()
    {
        GameObject _plank = Instantiate(_plankPrototypePref, _planksParent);
        _myPlanks.Add(_plank);
        if (_myPlanks.Count == 1)
        {
            if (_countPlank % 2 == 0)
            {
                _plank.transform.localPosition = new Vector3(-_plank.transform.localScale.x / 2, 0, 0);
            }
            else
            {
                _plank.transform.localPosition = new Vector3(_plank.transform.localScale.x / 2, 0, 0);
            }
            _lastPlankLocalPosition = _plank.transform.localPosition;
        }
        else
        {
            if (_countPlank % 2 == 0)
            {
                _plank.transform.localPosition = new Vector3(-_plank.transform.localScale.x / 2, _lastPlankLocalPosition.y + _plank.transform.localScale.y / 2, 0);
            }
            else
            {
                _plank.transform.localPosition = new Vector3(_plank.transform.localScale.x / 2, _lastPlankLocalPosition.y + _plank.transform.localScale.y / 2, 0);

            }
            _lastPlankLocalPosition = _plank.transform.localPosition;
        }
        _countPlank += 1;

    }
    public virtual void PutItDown()
    {
        var _p=_myPlanks[_myPlanks.Count-1];
        _myPlanks.RemoveAt(_myPlanks.Count-1);
        _countPlank-=1;
        _lastPlankLocalPosition=_myPlanks[_myPlanks.Count-1].transform.localPosition;
        Destroy(_p);

    }
    private void OnTriggerExit(Collider other)
    {
        Plank _plank = other.GetComponent<Plank>();
        if (other.gameObject.tag == MyStatics.CANTAKE)
        {
            _plank.SetActiveAndDeactive(false);
            _plank.SetTag(MyStatics.CANNOTTAKE);
            Take();
        }
        else
        {
            _plank.SetActiveAndDeactive(true);
            _plank.SetTag(MyStatics.CANTAKE);
            PutItDown();
        }
    }

}

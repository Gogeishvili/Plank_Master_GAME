using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public bool isPlayer = false;
    public bool isLive = true;
    [SerializeField] protected float _maxSpeed = 1;
    [SerializeField] Animator _thisAnimator = null;
    [SerializeField] GameObject _plankPrototypePref;
    protected NavMeshAgent _thisAgent = null;
    List<GameObject> _myPlanks = new List<GameObject>();
    Transform _planksParent = null;
    Vector3 _lastPlankLocalPosition = default;
    Rigidbody _thisRB = null;
    int _countPlank = 0;

    private void Awake()
    {
        _planksParent=transform.Find("Planks");
        _thisAgent = GetComponent<NavMeshAgent>();
        _thisRB = GetComponent<Rigidbody>();
        _thisAnimator = GetComponent<Animator>();
    }

    public virtual void Move(Vector2 direction)
    {

        float speed = _maxSpeed * direction.magnitude;
        _thisAnimator.SetFloat("speed", direction.magnitude);

        if (_thisAgent.enabled)
        {
            Vector3 forward = new Vector3(direction.x, 0, direction.y);

            if (forward.sqrMagnitude > 0)
                transform.forward = forward;

            _thisAgent.Move(forward * Time.deltaTime * speed);

        }
    }
    public virtual void Move(Vector3 direction)
    {

        float speed = _maxSpeed * direction.magnitude;
        _thisAnimator.SetFloat("speed", direction.magnitude);

        if (_thisAgent.enabled)
        {
            _thisAgent.SetDestination(direction);
        }
    }


    public virtual void Take(int value)
    {
        if (_countPlank % 2 == 0)
        {
            GameObject _plank = Instantiate(_plankPrototypePref, _planksParent);
            _myPlanks.Add(_plank);

            if (_myPlanks.Count == 1)
            {
                _plank.transform.localPosition = Vector3.zero;
                _lastPlankLocalPosition = _plank.transform.localPosition;
            }
            else
            {
                _plank.transform.localPosition = new Vector3(0, _lastPlankLocalPosition.y + _plank.transform.localScale.y / 2, 0);
                _lastPlankLocalPosition = _plank.transform.localPosition;
            }
        }
        _countPlank += value;

    }
    public virtual void PutItDown(int value)
    {
        if (_countPlank <= 0)
        {
            Fail();
        }

        if (_myPlanks.Count > 0)
        {
            var _p = _myPlanks[_myPlanks.Count - 1];
            _myPlanks.RemoveAt(_myPlanks.Count - 1);
            _countPlank -= value;
            if (_myPlanks.Count > 0)
            {
                _lastPlankLocalPosition = _myPlanks[_myPlanks.Count - 1].transform.localPosition;
            }
            else
            {
                _lastPlankLocalPosition = Vector3.zero;
            }
            Destroy(_p);
        }
    }

    public virtual void Fail()
    {
        GameManager.instance.UpdatePlayerInfo(this);
        isLive = false;
        _thisAgent.enabled = false;
        _thisRB.isKinematic = false;
        _thisRB.AddForce(transform.forward * 300 * Time.deltaTime, ForceMode.VelocityChange);
    }



}

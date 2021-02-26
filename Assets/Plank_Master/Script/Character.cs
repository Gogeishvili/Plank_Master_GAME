using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] protected float _maxSpeed = 1;
    [SerializeField] protected bool _isPlayer = false;
    [SerializeField] Animator _thisAnimator = null;
    [SerializeField] List<GameObject> _myPlanks = new List<GameObject>();
    [SerializeField] GameObject _plankPrototypePref;
    protected NavMeshAgent _thisAgent = null;
    Transform _planksParent = null;
    Vector3 _lastPlankLocalPosition = default;
    Rigidbody _thisRB = null;
    int _countPlank = 0;
    bool _isLive = true;

    private void Awake()
    {
        _planksParent = transform.GetChild(1);
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

        if (_myPlanks.Count > 0)
        {
            var _p = _myPlanks[_myPlanks.Count - 1];
            _myPlanks.RemoveAt(_myPlanks.Count - 1);
            _countPlank -= 1;
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
        else
        {
            Fail();
        }

    }

    public virtual void Fail()
    {
        _isLive = false;
        _thisAgent.enabled = false;
        _thisRB.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isLive)
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
   
}

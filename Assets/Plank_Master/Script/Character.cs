using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public bool isPlayer = false;
    public bool isLive = true;
    public GameObject indicator { get; set; }
    public Color myColor;
    [SerializeField] protected float _maxSpeed = 1;
    [SerializeField] float _gap = 1f;
    [SerializeField] Animator _thisAnimator = null;
    [SerializeField] GameObject[] _plankPrototypePref;
    [SerializeField] Transform _characterBody;
    protected NavMeshAgent _thisAgent = null;
    public List<GameObject> _myPlanks = new List<GameObject>();
    Transform _planksParent = null;
    Vector3 _lastPlankLocalPosition = default;
    Rigidbody _thisRB = null;
    int _countPlank = 0;


    private void Awake()
    {
        _planksParent = transform.Find(MyStatics.PLANK_PARENT);
        _thisAgent = GetComponent<NavMeshAgent>();
        _thisRB = GetComponent<Rigidbody>();
        _thisAnimator = GetComponent<Animator>();

    }

    public virtual void Move(Vector2 direction)
    {

        float speed = _maxSpeed * direction.magnitude;

        if (_thisAgent.enabled)
        {
            Vector3 _forward = new Vector3(direction.x, 0, direction.y);

            if (_forward.sqrMagnitude > 0)
            {

                transform.forward = _forward;
                _thisAnimator.SetBool(MyStatics.RUN_ANIMATION, true);
            }
            else
            {
                _thisAnimator.SetBool(MyStatics.RUN_ANIMATION, false);
            }

            _thisAgent.Move(_forward * Time.deltaTime * speed);

        }

    }
    public virtual void Move(Vector3 direction)
    {

        if (_thisAgent.enabled)
        {
            _thisAgent.SetDestination(direction);
            if (direction.sqrMagnitude > 0)
            {
                _thisAnimator.SetBool(MyStatics.RUN_ANIMATION, true);
            }
            else
            {
                _thisAnimator.SetBool(MyStatics.RUN_ANIMATION, false);
            }
        }
    }


    public virtual void Take(int value)
    {
        #region  randomMode
        if (GameManager.instance.randomPlankeVersion)
        {
            if (_countPlank % 2 == 0)
            {
                GameObject _plank = Instantiate(_plankPrototypePref[0], _planksParent);
                _myPlanks.Add(_plank);

                _plank.GetComponent<MeshRenderer>().material.SetColor("_Color", myColor);

                if (_myPlanks.Count == 1)
                {
                    _plank.transform.localPosition = Vector3.zero;

                    _lastPlankLocalPosition = _plank.transform.localPosition;
                }
                else
                {
                    if (GameManager.instance.randomPlankeVersion)
                    {
                        _gap = 0.2f;
                    }
                    else
                    {
                        _gap = 1.2f;
                    }
                    _plank.transform.localPosition = new Vector3(0, _lastPlankLocalPosition.y + _plank.transform.localScale.y / 2 + _gap, 0);
                    _lastPlankLocalPosition = _plank.transform.localPosition;
                }

            }
            _countPlank += value;
        }

        #endregion

        else
        {
            GameObject _cube = Instantiate(_plankPrototypePref[1], transform);
            _myPlanks.Add(_cube);
            StartCoroutine(SetMyCubeColor(_cube));


            if (GameManager.instance.randomPlankeVersion)
            {
                if (_myPlanks.Count == 1)
                {

                    _cube.transform.localPosition = new Vector3(0, 1, 0);
                    _lastPlankLocalPosition = _cube.transform.localPosition;
                    _characterBody.localPosition = new Vector3(0, _cube.transform.localPosition.y + _cube.transform.localScale.y / 2, 0);
                }
                else
                {
                    _cube.transform.localPosition = new Vector3(0, _lastPlankLocalPosition.y + _cube.transform.localScale.y / 2 + _gap, 0);
                    _lastPlankLocalPosition = _cube.transform.localPosition;
                    _characterBody.localPosition = new Vector3(0, _cube.transform.localPosition.y + _cube.transform.localScale.y / 2, 0);
                }
            }
            else
            {
                _cube.transform.localPosition = new Vector3(0, 1, 0);

                if (_myPlanks.Count > 1)
                {
                    for (int i = 0; i < _myPlanks.Count - 1; i++)
                    {
                        _myPlanks[i].transform.localPosition = new Vector3(0, _myPlanks[i + 1].transform.position.y + _gap + _cube.transform.localScale.y / 2f, 0);
                    }
                }
                _characterBody.localPosition = new Vector3(0, _myPlanks[0].transform.localPosition.y + _cube.transform.localScale.y / 2, 0);

                _countPlank += value;

                if (isPlayer)
                    CamerManager.instance.OffestControl(1.2f, -0.8f);
            }

        }


    }
    public virtual void PutItDown(int value)
    {

        if (_countPlank <= 0)
        {
            Falling();
        }

        if (_myPlanks.Count > 0)
        {
            if (GameManager.instance.randomPlankeVersion)
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

            else
            {
                var _p = _myPlanks[0];
                _myPlanks.RemoveAt(0);
                _countPlank -= value;
                Destroy(_p);
            }


        }

        if (!GameManager.instance.randomPlankeVersion)
        {

            if (_myPlanks.Count > 0)
            {
                _characterBody.localPosition = new Vector3(0, _myPlanks[0].transform.localPosition.y + _myPlanks[0].transform.localScale.y / 2, 0);
            }
            else
            {
                _characterBody.localPosition = Vector3.zero;
            }

            if (isPlayer)
                CamerManager.instance.OffestControl(-1.2f, 0.8f);

        }

    }

    public virtual void Falling()
    {
        _thisAnimator.Play(MyStatics.FALLING_ANIMATION);
        Destroy(indicator);

        GameManager.instance.UpdatePlayerInfo(this);
        isLive = false;
        _thisAgent.enabled = false;
        _thisRB.isKinematic = false;

        if (GameManager.instance.randomPlankeVersion)
        {
            _thisRB.AddForce(transform.forward * 300 * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (_myPlanks.Count > 0)
        {
            for (int i = 0; i < _myPlanks.Count; i++)
            {
                var _p = _myPlanks[i];
                _p.AddComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5, 5),
                  Random.Range(1, 3), Random.Range(-5, 5)) * 100 * Time.deltaTime, ForceMode.VelocityChange);
                _p.transform.SetParent(null);
                _myPlanks.Remove(_p);

            }
        }
    }

    public void TakeHit(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var _p = this._myPlanks[_myPlanks.Count - 1];
            this._myPlanks.RemoveAt(_myPlanks.Count - 1);

            _p.AddComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5, 5),
              Random.Range(5, 6), Random.Range(-5, 5)) * 100 * Time.deltaTime, ForceMode.VelocityChange);
            _p.transform.SetParent(null);
            _countPlank -= 1;

            if (_myPlanks.Count > 1)
            {
                foreach (var item in _myPlanks)
                {
                    item.transform.localPosition = new Vector3(0, item.transform.localPosition.y - item.transform.localScale.y / 2, 0);
                }
            }
            _characterBody.localPosition = new Vector3(0, _myPlanks[0].transform.localPosition.y + _myPlanks[0].transform.localScale.y / 2, 0);


            if (isPlayer)
                CamerManager.instance.OffestControl(-1.2f, 0.8f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!GameManager.instance.randomPlankeVersion)
        {
            if (other.tag == MyStatics.CHARACTER_TAG)
            {
                Character _character = other.GetComponent<Character>();

                if (_character._myPlanks.Count < this._myPlanks.Count && _character.isLive && this.isLive)
                {
                    TakeHit(_character._myPlanks.Count);
                    _character.Falling();
                }
            }
        }
    }

    IEnumerator SetMyCubeColor(GameObject cube)
    {

        yield return new WaitForSeconds(0.4f);
        if (cube != null)
        {
            cube.GetComponent<MeshRenderer>().material.SetColor("_Color", myColor);
        }
    }

}

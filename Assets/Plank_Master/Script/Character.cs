using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    NavMeshAgent _thisAgent = null;
    [SerializeField] float _maxSpeed = 1;
    [SerializeField] Animator _targetAnimator;

    private void Awake()
    {
        _thisAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector2 direction)
    {
        float speed = _maxSpeed * direction.magnitude;
        //_targetAnimator.SetFloat("speed", direction.magnitude);
        Vector3 forward = new Vector3(direction.x, 0, direction.y);
        if (forward.sqrMagnitude > 0)
            transform.forward = forward;
        _thisAgent.Move(forward * Time.deltaTime * speed);
    }

    

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        other.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    [Range(0, 1000)]
    [SerializeField] float _moveX = 0;

    [Range(0, 1000)]
    [SerializeField] float _moveZ = 0;
    Vector3 _direction = Vector3.zero;



    private void Update()
    {
        if (GameManager.instance.gameOn)
        {
            Move(_direction);

            if (_thisAgent.velocity.magnitude < 0.5f)
            {
                _direction = new Vector3(Random.Range(-_moveX, _moveX), transform.position.y, Random.Range(-_moveZ, _moveZ));
            }
        }
    }

}

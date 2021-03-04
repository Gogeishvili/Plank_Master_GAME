using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour
{
    [SerializeField] Character _baseCharacter;
    void Start()
    {
        _baseCharacter = GetComponentInParent<Character>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_baseCharacter.isLive)
        {

            Plank _plank = other.GetComponent<Plank>();
            if (other.gameObject.tag == MyStatics.CAN_TAKE && _plank.isActive)
            {
                _plank.SetActiveAndDeactive(false,_baseCharacter.myPlankColor);
                _plank.isActive=false;
                _plank.SetTag(MyStatics.CAN_NOT_TAKE);
                _baseCharacter.Take(1);
            }

        }
    }
    
}

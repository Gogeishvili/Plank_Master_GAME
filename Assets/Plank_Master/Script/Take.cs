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
            if (other.gameObject.tag == MyStatics.CANTAKE && _plank.isActive)
            {
                _plank.SetActiveAndDeactive(false);
                _plank.isActive=false;
                _plank.SetTag(MyStatics.CANNOTTAKE);
                _baseCharacter.Take(1);
            }

        }
    }
    
}

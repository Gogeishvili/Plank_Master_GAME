using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagController : MonoBehaviour
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
            if (other.gameObject.tag == MyStatics.CANNOTTAKE && _plank.isActive)
            {
                _plank.SetTag(MyStatics.CANTAKE);
                _plank.isActive=true;

            }

        }
    }
   
}

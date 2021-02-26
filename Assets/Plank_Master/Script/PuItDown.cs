using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuItDown : MonoBehaviour
{
    [SerializeField] Character _baseCharacter;
    void Start()
    {
        _baseCharacter = GetComponentInParent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_baseCharacter.isLive)
        {
            Plank _plank = other.GetComponent<Plank>();
            if (other.gameObject.tag == MyStatics.CANNOTTAKE)
            {
                
                _plank.SetActiveAndDeactive(true);
                _plank.isActive=true;
                _baseCharacter.PutItDown(2);

            }

        }
    }
}

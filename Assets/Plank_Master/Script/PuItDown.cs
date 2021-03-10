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
        if (GameManager.instance.randomPlankeVersion)
        {
            if (_baseCharacter.isLive)
            {
                Plank _plank = other.GetComponent<Plank>();

                if (other.gameObject.tag == MyStatics.CAN_NOT_TAKE)
                {
                    _plank.SetActiveAndDeactive(true, _baseCharacter.myColor, transform);
                    _plank.isActive = true;
                    _baseCharacter.PutItDown(2);

                }
            }
        }
        else
        {
            if (_baseCharacter.isLive)
            {
                Plank _plank = other.GetComponent<Plank>();

                if (other.gameObject.tag == MyStatics.CAN_NOT_TAKE)
                {
                    if (_plank)
                        _plank.isActive = true;
                        
                    _baseCharacter.PutItDown(1);
                }

            }

        }
    }
}

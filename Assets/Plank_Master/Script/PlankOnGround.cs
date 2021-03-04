using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankOnGround : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == MyStatics.CHARACTER_LAYER)
        {
            Destroy(this.gameObject);
        }
    }
}
